using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using Nancy;

namespace C.Teleport.AirportDistanceCalculator.Model
{
    public class CachedResponse : Response
    {
        private readonly Response _response;

        public CachedResponse(Response response)
        {
            _response = response;

            ContentType = response.ContentType;
            Headers = response.Headers;
            StatusCode = response.StatusCode;
            Contents = GetContents();
        }

        public override Task PreExecute(NancyContext context)
        {
            return _response.PreExecute(context);
        }

        private Action<Stream> GetContents()
        {
            return stream =>
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    _response.Contents.Invoke(memoryStream);

                    string contents = Encoding.UTF8.GetString(memoryStream.ToArray());

                    StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };

                    writer.Write(contents);
                }
            };
        }
    }
}
