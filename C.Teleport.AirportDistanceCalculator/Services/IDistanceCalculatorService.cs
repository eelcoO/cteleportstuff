using GeoCoordinatePortable;

using UnitsNet.Units;

namespace C.Teleport.AirportDistanceCalculator.Services
{
    public interface IDistanceCalculatorService
    {
        #region  Public Methods

        double CalculateDistance(GeoCoordinate @from, GeoCoordinate to, LengthUnit unitOfMeasure, int precision);

        #endregion
    }
}