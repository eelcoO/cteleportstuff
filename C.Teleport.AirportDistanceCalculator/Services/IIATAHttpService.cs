using System.Threading.Tasks;

using C.Teleport.AirportDistanceCalculator.Model;

namespace C.Teleport.AirportDistanceCalculator.Services
{
    /// <summary>
    /// Interface for the IATAHttpService.
    /// </summary>
    public interface IIATAHttpService
    {
        #region  Public Methods

        /// <summary>
        /// Makes the HTTP request to the IATAHttpService.
        /// </summary>
        /// <param name="airportIATACode">The airport iata code.</param>
        /// <returns></returns>
        Task<IATAHttpServiceResponse> MakeHttpRequest(string airportIATACode);

        #endregion
    }
}