using UnitsNet.Units;

namespace C.Teleport.AirportDistanceCalculator.Services
{
    /// <summary>
    /// Interface for LengthUnitOfMeasureConverterService
    /// </summary>
    public interface ILengthUnitOfMeasureConverterService
    {
        #region  Public Methods

        /// <summary>
        /// Converts the length.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <param name="fromUnit">From unit.</param>
        /// <param name="toUnit">To unit.</param>
        /// <returns></returns>
        double ConvertLength(double length, LengthUnit fromUnit, LengthUnit toUnit);

        #endregion
    }
}