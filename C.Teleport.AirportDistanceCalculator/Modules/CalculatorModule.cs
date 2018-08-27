using System;
using System.Linq;
using System.Threading.Tasks;

using C.Teleport.AirportDistanceCalculator.Model;
using C.Teleport.AirportDistanceCalculator.Services;

using Microsoft.Extensions.Caching.Memory;

using Nancy;
using Nancy.Extensions;
using Nancy.IO;
using Nancy.ModelBinding;

using Serilog;

using UnitsNet.Units;

using Response = Nancy.Response;

namespace C.Teleport.AirportDistanceCalculator.Modules
{
    /// <summary>
    /// Module that calculates the distance between two airports.
    /// </summary>
    /// <seealso cref="Nancy.NancyModule" />
    public class CalculatorModule : NancyModule
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CalculatorModule" /> class.
        /// </summary> {
        /// <param name="logger">The logger.</param>
        /// <param name="airportDistanceCalculatorService">The airport distance calculator service.</param>
        /// <param name="memoryCache">The memory cache</param>
        /// <exception cref="ArgumentNullException">logger</exception>
        /// <exception cref="ArgumentNullException">airportDistanceCalculatorService</exception>
        /// <exception cref="ArgumentNullException">memoryCache</exception>
        public CalculatorModule(ILogger logger, IAirportDistanceCalculatorService airportDistanceCalculatorService, IMemoryCache memoryCache) : base("distance")
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            AirportDistanceCalculatorService = airportDistanceCalculatorService ?? throw new ArgumentNullException(nameof(airportDistanceCalculatorService));
            MemoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));

            Post("calculate", Post);

            Before += CheckCache;

            After += SetCache;
        }

        #endregion

        #region Protected Properties

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        protected ILogger Logger { get; }

        /// <summary>
        /// Gets the airport distance calculator service.
        /// </summary>
        /// <value>
        /// The airport distance calculator service.
        /// </value>
        public IAirportDistanceCalculatorService AirportDistanceCalculatorService { get; }

        /// <summary>
        /// Gets the memory cache.
        /// </summary>
        /// <value>
        /// The memory cache.
        /// </value>
        protected IMemoryCache MemoryCache { get; }

        #endregion

        #region  Private Methods

        #region Actions

        private async Task<object> Post(dynamic argument)
        {
            DistanceCalculationRequest request = this.Bind<DistanceCalculationRequest>(); //Todo: Validation

            double distance = await AirportDistanceCalculatorService.CalculateDistance(
                request.FromAirportIATACode, 
                request.ToAirportIATACode, 
                request.UnitOfMeasure ?? LengthUnit.Mile, 
                request.RoundingPrecision ?? 0
            );

            DistanceCalculationResponse distanceCalculationResponse = new DistanceCalculationResponse
            {
                FromAirportIATACode = request.FromAirportIATACode,
                ToAirportIATACode = request.ToAirportIATACode,
                UnitOfMeasure = request.UnitOfMeasure ?? LengthUnit.Mile,
                Distance = distance,
                RoundingPrecision = request.RoundingPrecision ?? 0
            };

            return distanceCalculationResponse;
        }

        #endregion

        #region Cache

        private string GetCacheKey()
        {
            string headers = string.Join(
            Environment.NewLine,
            Request.Headers
            .Where(
            h => h.Key.Equals("Content-Type", StringComparison.OrdinalIgnoreCase) || h.Key.Equals("Accept", StringComparison.OrdinalIgnoreCase) || h.Key.Equals("Accept-Encoding", StringComparison.OrdinalIgnoreCase)
            )
            .Select(h => $"{h.Key}:{string.Join(",", h.Value)}")
            );

            string body = RequestStream.FromStream(Request.Body).AsString();

            return $"{headers}{Environment.NewLine}{body}";
        }

        private void SetCache(NancyContext context)
        {
            if (context.Response.StatusCode != HttpStatusCode.OK)
            {
                return;
            }

            string key = GetCacheKey();
            if (MemoryCache.Get(key) == null)
            {
                CachedResponse cachedResponse = new CachedResponse(context.Response);

                MemoryCache.Set(key, cachedResponse, DateTimeOffset.Now.AddHours(1));

                context.Response = cachedResponse;
            }
        }

        private Response CheckCache(NancyContext context)
        {
            string key = GetCacheKey();
            if (MemoryCache.TryGetValue(key, out CachedResponse response))
            {
                return response;
            }

            return null;
        }

        #endregion

        #endregion
    }
}