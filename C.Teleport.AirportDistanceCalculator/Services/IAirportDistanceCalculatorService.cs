using System.Threading.Tasks;

using UnitsNet.Units;

namespace C.Teleport.AirportDistanceCalculator.Services
{
    public interface IAirportDistanceCalculatorService
    {
        #region  Public Methods

        Task<double> CalculateDistance(string fromIATACode, string toIATACode, LengthUnit unitOfMeasure = LengthUnit.Mile, int precision = 0);

        #endregion
    }
}