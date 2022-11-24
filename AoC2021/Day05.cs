using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021
{
    public class Coordinate
    {
        public Coordinate()
        {
            
        }
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
    }

    public class HydroThermalVentLine
    {
        public HydroThermalVentLine()
        {

        }
        public HydroThermalVentLine(Coordinate start, Coordinate stop)
        {
            Start = start;
            Stop = stop;
        }

        public Coordinate Start { get; set; }
        public Coordinate Stop { get; set; }
    }

    public class Day05
    {
        private List<HydroThermalVentLine> Lines = new List<HydroThermalVentLine>();

        public void ParseInput(string path)
        {
            if (!File.Exists(path))
            {
                throw new ArgumentException($"The input file {path} does not exist");
            }

            int lineCount = 0;
            foreach (string line in System.IO.File.ReadLines(path))
            {
                lineCount++;
                var hydroline = new HydroThermalVentLine();

                var coords = line.Split(" -> ");
                if (coords.Length < 2 || coords.Length > 2)
                    throw new ArgumentException($"Could not parse a x and y coordinate pair on line {lineCount} in input file: amount of coordinates per line ot as expected");

                var coord = coords[0].Split(',');
                if (coord.Length < 2 || coord.Length > 2)
                    throw new ArgumentException($"Could not parse a x and y coordinate pair on line {lineCount} in input file; x and y in coordinate not as expected");
                int x = Int32.Parse(coord[0]);
                int y = Int32.Parse(coord[1]);
                hydroline.Start = new Coordinate(x, y);

                coord = coords[1].Split(',');
                if (coord.Length < 2 || coord.Length > 2)
                    throw new ArgumentException($"Could not parse a x and y coordinate pair on line {lineCount} in input file; x and y in coordinate not as expected");
                x = 0;
                y = 0;
                x = Int32.Parse(coord[0]);
                y = Int32.Parse(coord[1]);
                hydroline.Stop = new Coordinate(x, y);

                Lines.Add(hydroline);
                
            }
            Console.WriteLine(Lines.Count() + " lines");
        }

        public List<HydroThermalVentLine> GetHorizontalVertical45DegreesLines()
        {
            var result = new List<HydroThermalVentLine>();
            foreach (var line in Lines)
            {
                if (line.Start.X == line.Stop.X || line.Start.Y == line.Stop.Y)
                {
                    result.Add(line);
                }
                else if(Math.Abs(line.Stop.X - line.Start.X) == Math.Abs(line.Stop.Y - line.Start.Y))
                {
                    result.Add(line);

                }
            }

            return result;
        }

        public List<HydroThermalVentLine> GetHorizontalVerticalLines()
        {
            var result = new List<HydroThermalVentLine>();
            foreach (var line in Lines)
            {
                if (line.Start.X == line.Stop.X || line.Start.Y == line.Stop.Y)
                {
                    result.Add(line);
                }
            }

            return result;
        }

        public List<Coordinate> GetLappedCoordinates(List<HydroThermalVentLine> lines)
        {
            var result = new List<Coordinate>();

            foreach (var line in lines)
            {
                Console.Write($".");

                int compare(int i1, int i2)
                {
                    if (i1 < i2)
                    {
                        return 1;
                    }
                    else if (i1 > i2)
                    {
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                bool stop = false;
                int x = line.Start.X;
                int y = line.Start.Y;

                
                while (!stop)
                {
                    result.Add(new Coordinate(x, y));

                    if (line.Start.X != line.Stop.X)
                    {
                        int diffx = compare(x, line.Stop.X);
                        if (diffx == 0)
                        {
                            stop = true;
                        }

                        x += diffx;
                    }

                    if (line.Start.Y != line.Stop.Y)
                    {
                        int diffy = compare(y, line.Stop.Y);
                        if (diffy == 0)
                        {
                            stop = true;
                        }

                        y += diffy;
                    }
                    
                }
            }

            Console.WriteLine($"Totally {result.Count} coordinates.");

            
            var distinctList = result.GroupBy(x => new{ x.X, x.Y })
                .Where(x => x.Count() > 1)
                .Select(y => new Coordinate() { X = y.Key.X, Y = y.Key.Y })
                .ToList();

            Console.WriteLine($"Totally {distinctList.Count} duplicate coordinates.");
            return distinctList;
        }

        public Day05()
        {
            ParseInput("Input\\Day05.txt");
            //var horizontalVerticalLines = GetHorizontalVerticalLines();
            var moreLines = GetHorizontalVertical45DegreesLines();
            var lappingCoords = GetLappedCoordinates(moreLines);

            Console.WriteLine($"Found {lappingCoords.Count} overlapping coordinates");
        }
    }
}
