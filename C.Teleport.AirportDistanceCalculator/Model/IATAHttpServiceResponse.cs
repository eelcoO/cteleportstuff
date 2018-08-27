using Newtonsoft.Json;

namespace C.Teleport.AirportDistanceCalculator.Model
{
    /// <summary>
    /// Response object of the IATAHttpService service call.
    /// </summary>
    public class IATAHttpServiceResponse
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        [JsonProperty("country")]
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        [JsonProperty("location")]
        public GeoLocation GeoLocation { get; set; }

        /// <summary>
        /// Gets or sets the city iata.
        /// </summary>
        /// <value>
        /// The city iata.
        /// </value>
        [JsonProperty("city_iata")]
        public string CityIATA { get; set; }

        /// <summary>
        /// Gets or sets the iata.
        /// </summary>
        /// <value>
        /// The iata.
        /// </value>
        [JsonProperty("iata")]
        public string IATA { get; set; }

        /// <summary>
        /// Gets or sets the country iata.
        /// </summary>
        /// <value>
        /// The country iata.
        /// </value>
        [JsonProperty("country_iata")]
        public string CountryIATA { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonProperty("name")]
        public string Name { get; set; }

        #endregion
    }
}