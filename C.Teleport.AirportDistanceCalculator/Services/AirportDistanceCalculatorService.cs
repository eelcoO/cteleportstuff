using System;
using System.Threading.Tasks;

using C.Teleport.AirportDistanceCalculator.Model;

using GeoCoordinatePortable;

using UnitsNet.Units;

namespace C.Teleport.AirportDistanceCalculator.Services
{
    /// <summary>
    /// Service to calculate the distance between two airports based on IATA codes.
    /// </summary>
    /// <seealso cref="C.Teleport.AirportDistanceCalculator.Services.IAirportDistanceCalculatorService" />
    public class AirportDistanceCalculatorService : IAirportDistanceCalculatorService
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AirportDistanceCalculatorService"/> class.
        /// </summary>
        /// <param name="airportGeoLocationService">The airport geo location service.</param>
        /// <param name="distanceCalculatorService">The distance calculator service.</param>
        /// <exception cref="ArgumentNullException">
        /// airportGeoLocationService
        /// or
        /// distanceCalculatorService
        /// </exception>
        public AirportDistanceCalculatorService(IAirportGeoLocationService airportGeoLocationService, IDistanceCalculatorService distanceCalculatorService)
        {
            AirportGeoLocationService = airportGeoLocationService ?? throw new ArgumentNullException(nameof(airportGeoLocationService));
            DistanceCalculatorService = distanceCalculatorService ?? throw new ArgumentNullException(nameof(distanceCalculatorService));
        }

        #endregion

        #region Protected Properties

        /// <summary>
        /// Gets the airport geo location service.
        /// </summary>
        /// <value>
        /// The airport geo location service.
        /// </value>
        protected IAirportGeoLocationService AirportGeoLocationService { get; }

        /// <summary>
        /// Gets the distance calculator service.
        /// </summary>
        /// <value>
        /// The distance calculator service.
        /// </value>
        protected IDistanceCalculatorService DistanceCalculatorService { get; }

        #endregion

        #region Implementation of IAirportDistanceCalculatorService

        /// <summary>
        /// Calculates the distance.
        /// </summary>
        /// <param name="fromIATACode">From iata code.</param>
        /// <param name="toIATACode">To iata code.</param>
        /// <param name="unitOfMeasure">The unit of measure.</param>
        /// <param name="precision">The rounding precision.</param>
        /// <returns></returns>
        public async Task<double> CalculateDistance(string fromIATACode, string toIATACode, LengthUnit unitOfMeasure = LengthUnit.Mile, int precision = 0)
        {
            (GeoLocation FromGeoCoordinate, GeoLocation ToGeoCoordinate) coordinates = await AirportGeoLocationService.GetGeoLocations(fromIATACode, toIATACode);

            GeoCoordinate fromCoordinate = new GeoCoordinate(coordinates.FromGeoCoordinate.Latitude, coordinates.FromGeoCoordinate.Longitude);
            GeoCoordinate toCoordinate = new GeoCoordinate(coordinates.ToGeoCoordinate.Latitude, coordinates.ToGeoCoordinate.Longitude);

            return DistanceCalculatorService.CalculateDistance(fromCoordinate, toCoordinate, unitOfMeasure, precision);
        }

        #endregion
    }
}