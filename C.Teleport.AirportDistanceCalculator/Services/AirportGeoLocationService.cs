using System;
using System.Threading.Tasks;

using C.Teleport.AirportDistanceCalculator.Model;

namespace C.Teleport.AirportDistanceCalculator.Services
{
    public class AirportGeoLocationService : IAirportGeoLocationService
    {
        #region Constructors

        public AirportGeoLocationService(IIATAHttpService iataHttpService)
        {
            IATAHttpService = iataHttpService ?? throw new ArgumentNullException(nameof(iataHttpService));
        }

        #endregion

        #region Protected Properties

        protected IIATAHttpService IATAHttpService { get; }

        #endregion

        #region Implementation of IAirportGeoLocationService

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