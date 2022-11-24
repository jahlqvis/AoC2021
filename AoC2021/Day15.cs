using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021
{
    public class Node
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Weight { get; set; }
        public bool Visited { get; set; }
        public int Distance { get; set; }
        public bool InQueue { get; set; }

        public Node(int x, int y, int weight)
        {
            X = x; 
            Y = y;
            Weight = weight;
            Distance = 0;
            InQueue = false;
            Visited = false;
        }
    }

    public class Day15
    {
        private static Node[,]? _grid;
        private readonly string[] _input;

        public Day15(bool test)
        {
            _input = test switch
            {
                true => System.IO.File.ReadAllLines(
                    @"C:\Users\johahl\source\repos\AoC2021\AoC2021\Day15testinput.txt"),
                false => System.IO.File.ReadAllLines(
                    @"C:\Users\johahl\source\repos\AoC2021\AoC2021\Day15input.txt")
            };
        }

        public int RunA()
        {
            _grid = new Node[_input.Length, _input[0].Length];

            for (var y = 0; y < _input.Length; y++)
            {
                for (var x = 0; x < _input[y].Length; x++)
                {
                    var weight = int.Parse(_input[y].ToCharArray()[x].ToString());
                    _grid[y, x] = new Node(x, y, weight);
                }
            }

            return Tasks();
        }

        public int RunB()
        {
            // 5 times bigger, original grid was just upperleft part of a 5x5 
            var expandedGrid = new Node[_input.Length * 5, _input[0].Length * 5];
            
            for (var y = 0; y < _input.Length; y++)
            {
                for (var x = 0; x < _input[y].Length; x++)
                {
                    var gridSizeX = _input[y].Length;
                    var gridSizeY = _input.Length;

                    for (var n = 0; n < 5; n++)
                    {
                        var newX = x + n * gridSizeX;

                        for (var m = 0; m < 5; m++)
                        {
                            var newY = y + m * gridSizeY;

                            var weight = int.Parse(_input[y][x].ToString());
                            weight += n;
                            weight += m;
                            if (weight > 9)
                            {
                                weight -= 9;
                            }
                            
                            expandedGrid[newY, newX] = new Node(newX, newY, weight);
                        }
                    }
                }
            }

            _grid = expandedGrid;

            return Tasks();
        }

        private int Tasks()
        {
            var queue = new Queue<(int, int)>();    // queue with coordinates x, y

            if (_grid == null)
                throw new Exception("Grid can't be null");

            _grid[0, 0].InQueue = true; // start position upper left corner
            queue.Enqueue((0, 0));  // enqueue start position

            while (queue.TryDequeue(out var coords))
            {
                // gets node from coordinate pair
                var currNode = _grid[coords.Item1, coords.Item2];
                currNode.Visited = true;   // marks visited
                currNode.InQueue = false;  // marks dequeued

                FindNeighbours(currNode).ForEach(x =>
                {
                    if (x.Visited || x.InQueue)
                    {
                        if (x.Distance > x.Weight + currNode.Distance)
                        {
                            x.Distance = x.Weight + currNode.Distance;
                        }
                        
                        if (currNode.Distance <= currNode.Weight + x.Distance) 
                            return;
                        
                        currNode.Distance = currNode.Weight + x.Distance;
                    }
                    else
                    {
                        x.Distance = x.Weight + currNode.Distance;

                        x.InQueue = true;   // mark as in queue
                        queue.Enqueue((x.Y, x.X));  // enqueue 
                    }
                });
            }

            var finalNode = _grid[_grid.GetUpperBound(0), _grid.GetUpperBound(1)];
            return finalNode.Distance;
        }

        /// <summary>
        /// Returns list of neighbouring nodes (x+1, y and x, y+1)
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private List<Node> FindNeighbours(Node node)
        {
            var neighbours = new List<Node>();

            // if x is not at the right edge of grid
            if (node.X < _grid.GetLength(1) - 1)
            {
                // add neighbour x+1
                neighbours.Add(_grid[node.Y, node.X + 1]);
            }

            // if x is not at the left edge of grid
            if (node.X > 0)
            {
                // add neighbour x-1
                neighbours.Add(_grid[node.Y, node.X - 1]);
            }

            // if y is not at the bottom of grid
            if (node.Y < _grid.GetLength(0) - 1)
            {
                // add neighbour y+1
                neighbours.Add(_grid[node.Y + 1, node.X]);
            }

            // if y is not at the top of grid
            if (node.Y > 0)
            {
                // add neighbour y-1
                neighbours.Add(_grid[node.Y - 1, node.X]);
            }
            return neighbours;
        }

       
    }
}
