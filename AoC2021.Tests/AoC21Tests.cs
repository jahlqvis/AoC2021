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

        [TestMethod]
        public void Day04ATest()
        {
            string[] input = new string[]
            {
                "7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1",
                "              ",
                "22 13 17 11  0",
                "8  2 23  4 24",
                "21  9 14 16  7",
                "6 10  3 18  5",
                "1 12 20 15 19",
                "              ",
                "3 15  0  2 22",
                "9 18 13 17  5",
                "19 8 7 25 23",
                "20 11 10 24  4",
                "14 21 16 12  6",
                "              ",
                "14 21 17 24  4",
                "10 16 15  9 19",
                "18  8 23 26 20",
                "22 11 13  6  5",
                "2  0 12  3  7"
            };

            int num = Day04.Bingo(input);

            Assert.AreEqual(4512, num);
        }

        [TestMethod]
        public void Day04BTest()
        {
            string[] input = new string[]
            {
                "7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1",
                "              ",
                "22 13 17 11  0",
                "8  2 23  4 24",
                "21  9 14 16  7",
                "6 10  3 18  5",
                "1 12 20 15 19",
                "              ",
                "3 15  0  2 22",
                "9 18 13 17  5",
                "19 8 7 25 23",
                "20 11 10 24  4",
                "14 21 16 12  6",
                "              ",
                "14 21 17 24  4",
                "10 16 15  9 19",
                "18  8 23 26 20",
                "22 11 13  6  5",
                "2  0 12  3  7"
            };

            int num = Day04.BingoReversed(input);

            Assert.AreEqual(1924, num);
        }

        [TestMethod]
        public void Day15ATest()
        {
            Day15 day = new Day15(true);
            int risk = day.RunA();
            Assert.AreEqual(40, risk);
        }

        [TestMethod]
        public void Day15BTest()
        {
            Day15 day = new Day15(true);
            int risk = day.RunB();
            Assert.AreEqual(315, risk);
        }
    }
}