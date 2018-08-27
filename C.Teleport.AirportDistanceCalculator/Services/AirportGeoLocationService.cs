using System;
using System.Threading.Tasks;

using C.Teleport.AirportDistanceCalculator.Model;

namespace C.Teleport.AirportDistanceCalculator.Services
{
    /// <summary>
    /// Service to get geolocations.
    /// </summary>
    /// <seealso cref="C.Teleport.AirportDistanceCalculator.Services.IAirportGeoLocationService" />
    public class AirportGeoLocationService : IAirportGeoLocationService
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AirportGeoLocationService"/> class.
        /// </summary>
        /// <param name="iataHttpService">The iata HTTP service.</param>
        /// <exception cref="ArgumentNullException">iataHttpService</exception>
        public AirportGeoLocationService(IIATAHttpService iataHttpService)
        {
            IATAHttpService = iataHttpService ?? throw new ArgumentNullException(nameof(iataHttpService));
        }

        #endregion

        #region Protected Properties

        /// <summary>
        /// Gets the iata HTTP service.
        /// </summary>
        /// <value>
        /// The iata HTTP service.
        /// </value>
        protected IIATAHttpService IATAHttpService { get; }

        #endregion

        #region Implementation of IAirportGeoLocationService

        /// <summary>
        /// Gets the geolocations.
        /// </summary>
        /// <param name="fromAirportIATACode">From airport iata code.</param>
        /// <param name="toAirportIATACode">To airport iata code.</param>
        /// <returns></returns>
        public async Task<(GeoLocation FromGeoLocation, GeoLocation ToGeoLocation)> GetGeoLocations(string fromAirportIATACode, string toAirportIATACode)
        {
            IATAHttpServiceResponse fromResponse = await IATAHttpService.MakeHttpRequest(fromAirportIATACode);
            GeoLocation from = new GeoLocation(fromResponse.GeoLocation.Latitude, fromResponse.GeoLocation.Longitude);
            
            IATAHttpServiceResponse toResponse = await IATAHttpService.MakeHttpRequest(toAirportIATACode);
            GeoLocation to = new GeoLocation(toResponse.GeoLocation.Latitude, toResponse.GeoLocation.Longitude);

            return (from, to);
        }

        #endregion
    }
}