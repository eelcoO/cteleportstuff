using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using C.Teleport.AirportDistanceCalculator.Model;

using Microsoft.Extensions.Caching.Memory;

using Newtonsoft.Json;

using Polly;
using Polly.Caching;
using Polly.Caching.MemoryCache;
using Polly.Retry;

using Serilog;

using Url = Flurl.Url;

namespace C.Teleport.AirportDistanceCalculator.Services
{
    /// <summary>
    /// Service to call the airport information service at "https://places-dev.cteleport.com/airports/".
    /// </summary>
    /// <seealso cref="C.Teleport.AirportDistanceCalculator.Services.IIATAHttpService" />
    public class IATAHttpService : IIATAHttpService
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="IATAHttpService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="memoryCache">The memory cache.</param>
        /// <exception cref="ArgumentNullException">
        /// logger
        /// or
        /// memoryCache
        /// </exception>
        public IATAHttpService(ILogger logger, IMemoryCache memoryCache)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            MemoryCacheProvider = new MemoryCacheProvider(memoryCache ?? throw new ArgumentNullException(nameof(memoryCache)));
        }

        #endregion

        #region Protected Properties

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        protected ILogger Logger { get; set; }

        /// <summary>
        /// Gets the memory cache provider.
        /// </summary>
        /// <value>
        /// The memory cache provider.
        /// </value>
        protected MemoryCacheProvider MemoryCacheProvider { get; }

        #endregion

        #region Implementation of IIATAHttpService

        /// <summary>
        /// Makes the HTTP request to the IATAHttpService.
        /// </summary>
        /// <param name="airportIATACode">The airport iata code.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">airportIATACode</exception>
        /// <exception cref="HttpRequestException"></exception>
        public async Task<IATAHttpServiceResponse> MakeHttpRequest(string airportIATACode)
        {
            if(string.IsNullOrWhiteSpace(airportIATACode)) throw new ArgumentNullException(nameof(airportIATACode));
            
            //Todo: Move url, timeout and number of retries in settings object.
            string url = Url.Combine("https://places-dev.cteleport.com/airports/", airportIATACode);
            int retries = 5;
            double timeout = Math.Pow(2, 5);

            CachePolicy<HttpResponseMessage> cachePolicy = CreateCachePolicy(MemoryCacheProvider);
            RetryPolicy<HttpResponseMessage> waitAndRetryPolicy = CreateWaitAndRetryPolicy(retries);

            HttpClientHandler handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip
            };

            using (HttpClient client = new HttpClient(handler))
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                client.Timeout = TimeSpan.FromSeconds(timeout);

                Context context = new Context(url);

                HttpResponseMessage response = await cachePolicy.ExecuteAsync(
                    async () =>
                    {
                        return await waitAndRetryPolicy.ExecuteAsync(
                            async () =>
                            {
                                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, url);

                                return await client.SendAsync(requestMessage, HttpCompletionOption.ResponseContentRead);
                            }
                        );
                    },
                    context
                );

                await response.Content.LoadIntoBufferAsync();
                string responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode) throw new HttpRequestException($"An unexpected errpr occured while trying to call \"{url}\". Status: {response.StatusCode}. Content: {responseContent}.");

                return ExtractResponse(responseContent);
            }
        }

        #endregion

        #region Private Methods

        private CachePolicy<HttpResponseMessage> CreateCachePolicy(MemoryCacheProvider provider)
        {
            return Policy
            .CacheAsync(
            provider.AsyncFor<HttpResponseMessage>(),
            new ResultTtl<HttpResponseMessage>(message => new Ttl(message.Headers.CacheControl.MaxAge ?? TimeSpan.FromHours(24)))
            );
        }

        private RetryPolicy<HttpResponseMessage> CreateWaitAndRetryPolicy(int retries)
        {
            return Policy
            .HandleResult<HttpResponseMessage>(
            message => message.StatusCode == HttpStatusCode.InternalServerError || message.StatusCode == HttpStatusCode.RequestTimeout
            )
            .WaitAndRetryAsync(
            retries,
            attempt => TimeSpan.FromSeconds(1 * Math.Pow(2, attempt)),
            (result, timeSpan, retryCount, context) =>
            {
                Logger.Warning($"Request failed with {result.Result.StatusCode}. Waiting {timeSpan} before retry. Retry attempt {retryCount}.");
            }
            );
        }

        private IATAHttpServiceResponse ExtractResponse(string responseContent)
        {
            try
            {
                IATAHttpServiceResponse serviceResponse = JsonConvert.DeserializeObject<IATAHttpServiceResponse>(responseContent);

                return serviceResponse;
            }
            catch (Exception ex)
            {
                throw new HttpRequestException($"An error occurred while trying to deserialize the response. Raw response: {responseContent}", ex);
            }
        }

        #endregion
    }
}