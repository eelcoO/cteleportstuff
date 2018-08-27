using GeoCoordinatePortable;

using UnitsNet.Units;

namespace C.Teleport.AirportDistanceCalculator.Services
{
    /// <summary>
    /// Inteface for DistanceCalculatorService
    /// </summary>
    public interface IDistanceCalculatorService
    {
        #region  Public Methods

        /// <summary>
        /// Calculates the distance.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <param name="unitOfMeasure">The unit of measure.</param>
        /// <param name="precision">The precision.</param>
        /// <returns></returns>
        double CalculateDistance(GeoCoordinate @from, GeoCoordinate to, LengthUnit unitOfMeasure, int precision);

        #endregion
    }
}