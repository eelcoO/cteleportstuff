using UnitsNet;
using UnitsNet.Units;

namespace C.Teleport.AirportDistanceCalculator.Services
{
    /// <summary>
    /// Service to convert length units.
    /// </summary>
    /// <seealso cref="C.Teleport.AirportDistanceCalculator.Services.ILengthUnitOfMeasureConverterService" />
    public class LengthUnitOfMeasureConverterService : ILengthUnitOfMeasureConverterService
    {
        #region Implementation of ILengthUnitOfMeasureConverterService

        /// <summary>
        /// Converts the length.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <param name="fromUnit">From unit.</param>
        /// <param name="toUnit">To unit.</param>
        /// <returns></returns>
        public double ConvertLength(double length, LengthUnit fromUnit, LengthUnit toUnit)
        {
            Length lengthObject = new Length(length, fromUnit);

            return lengthObject.As(toUnit);
        }

        #endregion
    }
}