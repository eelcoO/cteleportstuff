using System;

using GeoCoordinatePortable;

using UnitsNet.Units;

namespace C.Teleport.AirportDistanceCalculator.Services
{
    public class DistanceCalculatorService : IDistanceCalculatorService
    {
        #region Constructors

        public DistanceCalculatorService(ILengthUnitOfMeasureConverterService lengthUnitOfMeasureConverter)
        {
            LengthUnitOfMeasureConverterService = lengthUnitOfMeasureConverter ?? throw new ArgumentNullException(nameof(lengthUnitOfMeasureConverter));
        }

        #endregion

        #region Protected Properties

        protected ILengthUnitOfMeasureConverterService LengthUnitOfMeasureConverterService { get; }

        #endregion

        #region Implementation of IDistanceCalculatorService

        public double CalculateDistance(GeoCoordinate @from, GeoCoordinate to, LengthUnit unitOfMeasure = LengthUnit.Mile, int precision = 0)
        {
            return Math.Round(LengthUnitOfMeasureConverterService.ConvertLength(from.GetDistanceTo(to), LengthUnit.Meter, unitOfMeasure), precision);
        }

        #endregion
    }
}