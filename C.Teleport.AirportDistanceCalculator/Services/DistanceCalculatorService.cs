using System;

using GeoCoordinatePortable;

using UnitsNet.Units;

namespace C.Teleport.AirportDistanceCalculator.Services
{
    /// <summary>
    /// Service to calculate the distance between GeoPoints.
    /// </summary>
    /// <seealso cref="C.Teleport.AirportDistanceCalculator.Services.IDistanceCalculatorService" />
    public class DistanceCalculatorService : IDistanceCalculatorService
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DistanceCalculatorService"/> class.
        /// </summary>
        /// <param name="lengthUnitOfMeasureConverter">The length unit of measure converter.</param>
        /// <exception cref="ArgumentNullException">lengthUnitOfMeasureConverter</exception>
        public DistanceCalculatorService(ILengthUnitOfMeasureConverterService lengthUnitOfMeasureConverter)
        {
            LengthUnitOfMeasureConverterService = lengthUnitOfMeasureConverter ?? throw new ArgumentNullException(nameof(lengthUnitOfMeasureConverter));
        }

        #endregion

        #region Protected Properties

        /// <summary>
        /// Gets the length unit of measure converter service.
        /// </summary>
        /// <value>
        /// The length unit of measure converter service.
        /// </value>
        protected ILengthUnitOfMeasureConverterService LengthUnitOfMeasureConverterService { get; }

        #endregion

        #region Implementation of IDistanceCalculatorService

        /// <summary>
        /// Calculates the distance.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <param name="unitOfMeasure">The unit of measure.</param>
        /// <param name="precision">The rounding precision.</param>
        /// <returns></returns>
        public double CalculateDistance(GeoCoordinate @from, GeoCoordinate to, LengthUnit unitOfMeasure = LengthUnit.Mile, int precision = 0)
        {
            return Math.Round(LengthUnitOfMeasureConverterService.ConvertLength(from.GetDistanceTo(to), LengthUnit.Meter, unitOfMeasure), precision);
        }

        #endregion
    }
}