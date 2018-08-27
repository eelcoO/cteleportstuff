using UnitsNet.Units;

namespace C.Teleport.AirportDistanceCalculator.Model
{
    /// <summary>
    /// Class representing the response to a distance calculation request.
    /// </summary>
    public class DistanceCalculationResponse
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets IATA code of the airport one is travelling to.
        /// </summary>
        /// <value>
        /// An IATA code.
        /// </value>
        public string ToAirportIATACode { get; set; }

        /// <summary>
        /// Gets or sets IATA code of the airport one is travelling from.
        /// </summary>
        /// <value>
        /// An IATA code.
        /// </value>
        public string FromAirportIATACode { get; set; }

        /// <summary>
        /// Gets or sets the distance.
        /// </summary>
        /// <value>
        /// The distance.
        /// </value>
        public double Distance { get; set; }

        /// <summary>
        /// Gets or sets the unit of measure of the distance.
        /// </summary>
        /// <value>
        /// The unit of measure of the distance.
        /// </value>
        public LengthUnit UnitOfMeasure { get; set; }

        /// <summary>
        /// Gets or sets the precision.
        /// </summary>
        /// <value>
        /// The precision.
        /// </value>
        public int RoundingPrecision { get; set; }

        #endregion
    }
}