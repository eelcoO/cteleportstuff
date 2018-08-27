using System.Threading.Tasks;

using C.Teleport.AirportDistanceCalculator.Model;

using GeoCoordinatePortable;

namespace C.Teleport.AirportDistanceCalculator.Services
{
    public interface IAirportGeoLocationService
    {
        #region  Public Methods

        Task<(GeoLocation FromGeoLocation, GeoLocation ToGeoLocation)> GetGeoLocations(string fromAirportIATACode, string toAirportIATACode);

        #endregion
    }
}