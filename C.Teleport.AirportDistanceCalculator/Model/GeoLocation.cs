using System;

using Newtonsoft.Json;

namespace C.Teleport.AirportDistanceCalculator.Model
{
    /// <summary>
    /// GeoLocation with latitude and longitude
    /// </summary>
    public class GeoLocation
    {
        #region Private Fields

        private double _longitude;
        private double _latitude;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GeoLocation"/> class.
        /// </summary>
        /// <param name="longitude">The longitude.</param>
        /// <param name="latitude">The latitude.</param>
        public GeoLocation(double latitude, double longitude)
        {
            _longitude = longitude;
            _latitude = latitude;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeoLocation"/> class.
        /// </summary>
        public GeoLocation() { }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        [JsonProperty("lon")]
        public double Longitude
        {
            get => _longitude;
            set
            {
                if (value > 180.0 || value < -180.0)
                    throw new ArgumentOutOfRangeException(nameof(Longitude), "Argument must be in range of -180 to 180");
                _longitude = value;
            }
        }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        [JsonProperty("lat")]
        public double Latitude
        {
            get => _latitude;
            set
            {
                if (value > 90.0 || value < -90.0)
                    throw new ArgumentOutOfRangeException(nameof(Latitude), "Argument must be in range of -90 to 90");
                _latitude = value;
            }
        }

        #endregion
    }
}