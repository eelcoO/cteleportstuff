using C.Teleport.AirportDistanceCalculator.Services;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using UnitsNet.Units;

namespace C.Teleport.AirportDistanceCalculator.Tests
{
    [TestClass]
    public class LengthUnitOfMeasureConverterServiceTest
    {
        [TestMethod]
        public void Miles_Equals_Mile()
        {
            LengthUnitOfMeasureConverterService service = new LengthUnitOfMeasureConverterService();
            double length = service.ConvertLength(1, LengthUnit.Mile, LengthUnit.Mile);

            Assert.AreEqual(1, length);
        }

        [TestMethod]
        public void Miles_Equals_1_60934Kilometer()
        {
            LengthUnitOfMeasureConverterService service = new LengthUnitOfMeasureConverterService();
            double length = service.ConvertLength(1, LengthUnit.Mile, LengthUnit.Kilometer);

            Assert.AreEqual(1.60934, length);
        }
    }
}
