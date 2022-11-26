using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021
{
    public class Day06
    {
        public Day06()
        {
            var fishes = ParseInput("Input\\Day06.txt");
            Console.WriteLine(GetFishesCount(fishes) + " fishes");
            int days = 256;
            var fishesNew = Wait(fishes, days);
            Console.WriteLine($"After {days}");
            Console.WriteLine(GetFishesCount(fishesNew) + " fishes");
        }

        public Dictionary<int, ulong> ParseInput(string inputPath)
        {
            if (!File.Exists(inputPath))
            {
                throw new ArgumentException($"The input file {inputPath} does not exist");
            }

            var list = System.IO.File.ReadAllLines(inputPath)[0].Split(",").Select(i => Convert.ToUInt64(i)).ToList();
            var dict = new Dictionary<int, ulong>();
            dict.Add(0, (ulong)list.Where(x => x.Equals(0)).Count());
            dict.Add(1, (ulong)list.Where(x => x.Equals(1)).Count());
            dict.Add(2, (ulong)list.Where(x => x.Equals(2)).Count());
            dict.Add(3, (ulong)list.Where(x => x.Equals(3)).Count());
            dict.Add(4, (ulong)list.Where(x => x.Equals(4)).Count());
            dict.Add(5, (ulong)list.Where(x => x.Equals(5)).Count());
            dict.Add(6, (ulong)list.Where(x => x.Equals(6)).Count());
            dict.Add(7, (ulong)list.Where(x => x.Equals(7)).Count());
            dict.Add(8, (ulong)list.Where(x => x.Equals(8)).Count());
            
            return dict;
        }

        Dictionary<int, ulong> Rotate(Dictionary<int, ulong> fishesCurrentState)
        {
            var fishesNextState = new Dictionary<int, ulong>();
            fishesNextState[0] = fishesCurrentState[1];
            fishesNextState[1] = fishesCurrentState[2];
            fishesNextState[2] = fishesCurrentState[3];
            fishesNextState[3] = fishesCurrentState[4];
            fishesNextState[4] = fishesCurrentState[5];
            fishesNextState[5] = fishesCurrentState[6];
            fishesNextState[6] = fishesCurrentState[7];
            fishesNextState[7] = fishesCurrentState[8];
            fishesNextState[8] = fishesCurrentState[0];

            if (fishesCurrentState[0] > 0)
            {
                fishesNextState[6] += fishesCurrentState[0];
            }
            return fishesNextState;
        }

        public Dictionary<int, ulong> Wait(Dictionary<int, ulong> fishes, int days)
        {
            // start from 1 as we already rotated once
            for (int i = 0; i < days; i++)
            {
                fishes = Rotate(fishes);
            }

            return fishes;
        }

        public ulong GetFishesCount(Dictionary<int, ulong> fishes)
        {
            ulong total = 0;
            foreach (var fish in fishes)
            {
                total += (ulong)fish.Value;
            }
            
            return total;
        }
    }
}
