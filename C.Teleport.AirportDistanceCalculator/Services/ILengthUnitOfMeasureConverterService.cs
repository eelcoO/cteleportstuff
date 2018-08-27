using UnitsNet.Units;

namespace C.Teleport.AirportDistanceCalculator.Services
{
    public interface ILengthUnitOfMeasureConverterService
    {
        #region  Public Methods

        double ConvertLength(double quantity, LengthUnit fromUnit, LengthUnit toUnit);

        #endregion
    }
}