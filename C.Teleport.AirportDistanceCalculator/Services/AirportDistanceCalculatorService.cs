using System;
using System.Threading.Tasks;

using C.Teleport.AirportDistanceCalculator.Model;

using GeoCoordinatePortable;

using UnitsNet.Units;

namespace C.Teleport.AirportDistanceCalculator.Services
{
    public class AirportDistanceCalculatorService : IAirportDistanceCalculatorService
    {
        #region Constructors

        public AirportDistanceCalculatorService(IAirportGeoLocationService airportGeoLocationService, IDistanceCalculatorService distanceCalculatorService)
        {
            AirportGeoLocationService = airportGeoLocationService ?? throw new ArgumentNullException(nameof(airportGeoLocationService));
            DistanceCalculatorService = distanceCalculatorService ?? throw new ArgumentNullException(nameof(distanceCalculatorService));
        }

        #endregion

        #region Protected Properties

        protected IAirportGeoLocationService AirportGeoLocationService { get; }

        protected IDistanceCalculatorService DistanceCalculatorService { get; }

        #endregion

        #region Implementation of IAirportDistanceCalculatorService

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