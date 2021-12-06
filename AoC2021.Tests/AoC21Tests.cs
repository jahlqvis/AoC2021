using Microsoft.VisualStudio.TestTools.UnitTesting;
using AoC2021;

namespace AoC2021.Tests
{
    [TestClass]
    public class AoC21Tests
    {
        [TestMethod]
        public void Day01ATest()
        {
            int[] depths = new int[]
            {
                199,
                200,
                208,
                210,
                200,
                207,
                240,
                269,
                260,
                263
            };

            int result = Day01.CountA(depths);
            Assert.AreEqual(7, result); 
        }

        [TestMethod]
        public void Day01BTest()
        {
            int[] depths = new int[]
            {
                199,
                200,
                208,
                210,
                200,
                207,
                240,
                269,
                260,
                263
            };

            int result = Day01.CountB(depths);
            Assert.AreEqual(5, result);
        }

        [TestMethod]
        public void Day02ATest()
        {
            string[] steps = new string[]
            {
                "forward 5",
                "down 5",
                "forward 8",
                "up 3",
                "down 8",
                "forward 2"
            };

            int res = Day02.CalculatePositionA(steps);

            Assert.AreEqual(150, res);
        }

        [TestMethod]
        public void Day02BTest()
        {
            string[] steps = new string[]
            {
                "forward 5",
                "down 5",
                "forward 8",
                "up 3",
                "down 8",
                "forward 2"
            };

            int res = Day02.CalculatePositionB(steps);

            Assert.AreEqual(900, res);
        }

        [TestMethod]
        public void Day03ATest()
        {
            ushort[] report = new ushort[]
            {
                0b00100,
                0b11110,
                0b10110,
                0b10111,
                0b10101,
                0b01111,
                0b00111,
                0b11100,
                0b10000,
                0b11001,
                0b00010,
                0b01010
            };

            int result = Day03.CalculateDiagnosticA(report);

            Assert.AreEqual(198, result);
        }

        [TestMethod]
        public void Day03BTest()
        {
            ushort[] report = new ushort[]
            {
                0b00100,
                0b11110,
                0b10110,
                0b10111,
                0b10101,
                0b01111,
                0b00111,
                0b11100,
                0b10000,
                0b11001,
                0b00010,
                0b01010
            };

            int result = Day03.CalculateDiagnosticB(report);

            Assert.AreEqual(230, result);
        }
    }
}