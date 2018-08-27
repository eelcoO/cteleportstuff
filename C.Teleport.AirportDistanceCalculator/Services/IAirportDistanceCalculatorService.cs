using System.Threading.Tasks;

using UnitsNet.Units;

namespace C.Teleport.AirportDistanceCalculator.Services
{
    /// <summary>
    /// Interface for AirportDistanceCalculatorService.
    /// </summary>
    public interface IAirportDistanceCalculatorService
    {
        #region  Public Methods

        /// <summary>
        /// Calculates the distance.
        /// </summary>
        /// <param name="fromIATACode">From iata code.</param>
        /// <param name="toIATACode">To iata code.</param>
        /// <param name="unitOfMeasure">The unit of measure.</param>
        /// <param name="precision">The precision.</param>
        /// <returns></returns>
        Task<double> CalculateDistance(string fromIATACode, string toIATACode, LengthUnit unitOfMeasure = LengthUnit.Mile, int precision = 0);

        #endregion
    }
}