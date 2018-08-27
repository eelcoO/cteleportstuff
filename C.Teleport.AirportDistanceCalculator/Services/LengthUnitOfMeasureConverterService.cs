using UnitsNet;
using UnitsNet.Units;

namespace C.Teleport.AirportDistanceCalculator.Services
{
    public class LengthUnitOfMeasureConverterService : ILengthUnitOfMeasureConverterService
    {
        #region Implementation of ILengthUnitOfMeasureConverterService

        public double ConvertLength(double quantity, LengthUnit fromUnit, LengthUnit toUnit)
        {
            Length length = new Length(quantity, fromUnit);

            return length.As(toUnit);
        }

        #endregion
    }
}