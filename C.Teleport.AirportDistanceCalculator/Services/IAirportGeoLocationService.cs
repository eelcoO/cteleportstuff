using System.Threading.Tasks;

using C.Teleport.AirportDistanceCalculator.Model;

namespace C.Teleport.AirportDistanceCalculator.Services
{
    /// <summary>
    /// Interface for AirportGeoLocationService.
    /// </summary>
    public interface IAirportGeoLocationService
    {
        #region  Public Methods

        /// <summary>
        /// Gets the geo locations.
        /// </summary>
        /// <param name="fromAirportIATACode">From airport iata code.</param>
        /// <param name="toAirportIATACode">To airport iata code.</param>
        /// <returns></returns>
        Task<(GeoLocation FromGeoLocation, GeoLocation ToGeoLocation)> GetGeoLocations(string fromAirportIATACode, string toAirportIATACode);

        #endregion
    }
}