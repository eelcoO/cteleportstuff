using System.ComponentModel.DataAnnotations;

using UnitsNet.Units;

namespace C.Teleport.AirportDistanceCalculator.Model
{
    /// <summary>
    /// Class representing a request to calculate the distance between two airports.
    /// </summary>
    public class DistanceCalculationRequest
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets IATA code of the airport one is travelling to.
        /// </summary>
        /// <value>
        /// An IATA code.
        /// </value>
        [Required, StringLength(3)]
        public string ToAirportIATACode { get; set; }

        /// <summary>
        /// Gets or sets IATA code of the airpLengthUnitOfMeasureort one is travelling from.
        /// </summary>
        /// <value>
        /// An IATA code.
        /// </value>
        [Required, StringLength(3)]
        public string FromAirportIATACode { get; set; }

        /// <summary>
        /// Gets or sets the unit of measure of the distance.
        /// </summary>
        /// <value>
        /// The unit of measure of the distance.
        /// </value>
        public LengthUnit? UnitOfMeasure { get; set; }

        /// <summary>
        /// Gets or sets the precision.
        /// </summary>
        /// <value>
        /// The precision.
        /// </value>
        public int? RoundingPrecision { get; set; }

        #endregion
    }
}
