using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using Nancy;

namespace C.Teleport.AirportDistanceCalculator.Model
{
    /// <summary>
    /// Class for cached responses.
    /// </summary>
    /// <seealso cref="Nancy.Response" />
    public class CachedResponse : Response
    {
        #region Private Fields

        private readonly Response _response;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CachedResponse"/> class.
        /// </summary>
        /// <param name="response">The response.</param>
        public CachedResponse(Response response)
        {
            _response = response;

            ContentType = response.ContentType;
            Headers = response.Headers;
            StatusCode = response.StatusCode;
            Contents = GetContents();
        }

        #endregion

        #region  Private Methods

        private Action<Stream> GetContents()
        {
            return stream =>
            {
                using(MemoryStream memoryStream = new MemoryStream())
                {
                    _response.Contents.Invoke(memoryStream);

                    string contents = Encoding.UTF8.GetString(memoryStream.ToArray());

                    StreamWriter writer = new StreamWriter(stream) {AutoFlush = true};

                    writer.Write(contents);
                }
            };
        }

        #endregion

        #region Overides of Response

        public override Task PreExecute(NancyContext context)
        {
            return _response.PreExecute(context);
        }

        #endregion
    }
}