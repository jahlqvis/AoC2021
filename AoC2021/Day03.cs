﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021
{
    public static class Day03
    {
        public class BitCount
        {
            public int zeros;
            public int ones;


        };

        private static int GetNumberOfUsedBits(uint value)
        {
            int count = 0;
            while (value != 0)
            {
                value >>= 1;    
                count++;
            }
            return count;
        }

        public static int CalculateDiagnosticA(ushort[] inputArr)
        {
            Dictionary<int, BitCount> bytesCount = new Dictionary<int, BitCount>();

            // get amount of bits in input
            ushort max = inputArr.Max();
            int numBits = GetNumberOfUsedBits(max);

            // traverse through all binary numbers in input array
            foreach (ushort i in inputArr)
            {
                byte[] by = BitConverter.GetBytes(i);
                BitArray bits = new BitArray(by);

                // traverse through all bits in binary number
                for (int ii = 0; ii < numBits; ii++)
                {
                    
                    BitCount b;
                    if(!bytesCount.TryGetValue(ii, out b))
                    {
                        b = new BitCount();
                        b.zeros = 0;
                        b.ones = 0;
                        bytesCount.Add(ii, b);
                    }
                    
                    if (bits.Get(ii))
                    {
                        b.ones++;
                        
                    }
                    else
                    {
                        b.zeros++;
                    }
                    
                }
            }

            // most common bit, gamma
            // least common bit, epsilon
            BitArray gamma = new BitArray(new byte[2]);
            BitArray epsilon = new BitArray(new byte[2]);

            for (int i = 0; i < numBits; i++)
            {
                if (bytesCount[i].ones > bytesCount[i].zeros)
                {
                    gamma.Set(i, true);
                    epsilon.Set(i, false);
                }
                else
                {
                    gamma.Set(i, false);
                    epsilon.Set(i, true);
                }
            }


            int[] intArray = new int[1];
            short gammarate;
            short epsilonrate;
            gamma.CopyTo(intArray, 0);
            gammarate = (short)intArray[0];
            epsilon.CopyTo(intArray, 0);
            epsilonrate = (short)intArray[0];

            return gammarate * epsilonrate;
        }

        public static int CalculateDiagnosticB(ushort[] inputArr)
        {
            Dictionary<int, BitCount> bytesCount = new Dictionary<int, BitCount>();

            // get amount of bits in input
            ushort max = inputArr.Max();
            int numBits = GetNumberOfUsedBits(max);

            List<ushort> liNumbers = new List<ushort>();
            foreach (ushort n in inputArr) { liNumbers.Add(n); }

            // this dictionary is used to temporarily store bit information for each digit and number, most common or least common
            Dictionary<ushort, bool> dictNumbers = new Dictionary<ushort, bool>();

            // Oxygen generator rating
            // traverse through all bits in binary number, from most significant to least significant bit 
            for (int i = numBits-1; i >= 0; i--)
            {
                int ones = 0;
                int zeros = 0;

                // traverse through numbers in input list until only one number left which is the desired number
                foreach (ushort num in liNumbers)
                {
                    byte[] by = BitConverter.GetBytes(num);
                    BitArray bits = new BitArray(by);

                    dictNumbers.Add(num, bits.Get(i));
                    
                    if (bits.Get(i))
                        ones++;
                    else
                        zeros++;
                }

                // determine the most common value (0 or 1) in the current bit position, and keep only numbers with that bit in that position. If 0 and 1 are equally common, keep values with a 1 in the position being considered.
                if (ones >= zeros)
                    dictNumbers = dictNumbers.Where(x => x.Value == true).ToDictionary(x => x.Key, p => p.Value);
                else
                    dictNumbers = dictNumbers.Where(x => x.Value == false).ToDictionary(x => x.Key, p => p.Value);

                liNumbers = dictNumbers.Keys.ToList();
                dictNumbers.Clear();

                if (liNumbers.Count() == 1)
                    break;
                if (liNumbers.Count() == 0)
                    throw new Exception("No numbers left in list");
            }
            
            if(liNumbers.Count() > 1)
                throw new Exception("More than one number left in list");

            int OxygenGeneratorRating = liNumbers[0];

            // CO2 scrubber rating
            // traverse through all bits in binary number for 

            liNumbers.Clear();
            foreach (ushort n in inputArr) { liNumbers.Add(n); }

            // traverse through all bits in binary number, from most significant to least significant bit 
            for (int i = numBits - 1; i >= 0; i--)
            { 
                int ones = 0;
                int zeros = 0;

                // traverse through numbers in input list until only one number left which is the desired number 
                foreach (ushort num in liNumbers)
                {
                    byte[] by = BitConverter.GetBytes(num);
                    BitArray bits = new BitArray(by);

                    dictNumbers.Add(num, bits.Get(i));

                    if (bits.Get(i))
                        ones++;
                    else
                        zeros++;
                }

                // determine the least common value (0 or 1) in the current bit position, and keep only numbers with that bit in that position. If 0 and 1 are equally common, keep values with a 0 in the position being considered.
                if (ones >= zeros)
                    dictNumbers = dictNumbers.Where(x => x.Value == false).ToDictionary(x => x.Key, p => p.Value);
                else
                    dictNumbers = dictNumbers.Where(x => x.Value == true).ToDictionary(x => x.Key, p => p.Value);

                liNumbers = dictNumbers.Keys.ToList();
                dictNumbers.Clear();

                if (liNumbers.Count() == 1)
                    break;
                if (liNumbers.Count() == 0)
                    throw new Exception("No numbers left in list");
            }

            if (liNumbers.Count() > 1)
                throw new Exception("More than one number left in list");

            int CO2ScrubberRating = liNumbers[0];

            return OxygenGeneratorRating * CO2ScrubberRating;
        }

        public static ushort[] Report = new ushort[]
        {
            0b110011101111,
            0b011110010111,
            0b101010111001,
            0b110011100110,
            0b110010000101,
            0b000111001111,
            0b001111110011,
            0b100000111010,
            0b101010000110,
            0b001000100011,
            0b110000000100,
            0b000100110000,
            0b010010101110,
            0b101110111101,
            0b100000100111,
            0b111011010111,
            0b001101010010,
            0b010010001111,
            0b010101100001,
            0b111001101101,
            0b110110000011,
            0b000110111111,
            0b111111100010,
            0b110010101011,
            0b010011100100,
            0b110011010010,
            0b100001111010,
            0b000101000110,
            0b110001000111,
            0b110110000001,
            0b101011010110,
            0b101001011010,
            0b100101000110,
            0b101010110001,
            0b111000110110,
            0b111000001111,
            0b010111101111,
            0b000001001011,
            0b000001111101,
            0b000111010101,
            0b110101111110,
            0b110000100101,
            0b011101110101,
            0b011101000000,
            0b111011100101,
            0b011100001101,
            0b000010100001,
            0b010010010100,
            0b001010100110,
            0b001111110110,
            0b111110101001,
            0b000010101101,
            0b100111000100,
            0b000001110000,
            0b011010110011,
            0b111111110111,
            0b011100010011,
            0b010001100101,
            0b000100110101,
            0b101101001111,
            0b001000011101,
            0b101100000000,
            0b000100101000,
            0b100100010010,
            0b111010011011,
            0b101010001010,
            0b110111111100,
            0b010111000001,
            0b010110111010,
            0b001011110110,
            0b100010001100,
            0b010000000011,
            0b101101101001,
            0b011110101101,
            0b101101010101,
            0b011011101110,
            0b001110110100,
            0b110100101000,
            0b101000000101,
            0b000101100010,
            0b100000111111,
            0b110100101011,
            0b010011110110,
            0b011001100110,
            0b011000100010,
            0b100110001101,
            0b010011110010,
            0b010011010010,
            0b111001110011,
            0b001010101100,
            0b001011100111,
            0b001101110011,
            0b110011101011,
            0b100001100110,
            0b011011101000,
            0b001001011100,
            0b110000110110,
            0b111100010010,
            0b011110110100,
            0b110000111000,
            0b110000001100,
            0b101110111011,
            0b110010111001,
            0b100001010110,
            0b011111110110,
            0b001101010000,
            0b101110110010,
            0b011101001101,
            0b011100001001,
            0b011110101111,
            0b010000101011,
            0b101110111111,
            0b000010100011,
            0b001010110010,
            0b111101111011,
            0b000101101010,
            0b111011101011,
            0b101001000001,
            0b100010100101,
            0b100110100010,
            0b101001111011,
            0b010110001111,
            0b010001000100,
            0b100101001111,
            0b100100110101,
            0b011010101110,
            0b111100001000,
            0b010000110001,
            0b000101000100,
            0b100011011011,
            0b101101010111,
            0b011110101000,
            0b011011100110,
            0b010111101001,
            0b001110000101,
            0b100100100011,
            0b010001110011,
            0b000111101111,
            0b001100011000,
            0b010100010011,
            0b110000100011,
            0b000010010001,
            0b000101110111,
            0b101011011101,
            0b101001111000,
            0b011101111010,
            0b111011000011,
            0b110111100101,
            0b000110111010,
            0b100101011110,
            0b000010000001,
            0b010110011111,
            0b011011000100,
            0b101001111100,
            0b101101101010,
            0b100011100111,
            0b001100100101,
            0b111010101101,
            0b010010010101,
            0b110001101100,
            0b011000100101,
            0b011010100010,
            0b010110010001,
            0b100101011010,
            0b001011100001,
            0b000100010001,
            0b110001100000,
            0b001011001100,
            0b110111111001,
            0b100110110000,
            0b001110101001,
            0b100001001101,
            0b101111101101,
            0b101010001110,
            0b000010011000,
            0b010011101100,
            0b101000110100,
            0b010001001110,
            0b000111100110,
            0b010010011100,
            0b011011111100,
            0b011000110110,
            0b101000011110,
            0b110111011001,
            0b111011000000,
            0b101000101111,
            0b011110100111,
            0b101000011010,
            0b011100000110,
            0b110100001000,
            0b010101100011,
            0b100110001010,
            0b001100111101,
            0b101100111111,
            0b000000100101,
            0b000110010001,
            0b000010101011,
            0b111100000001,
            0b101011001000,
            0b010100111111,
            0b010001010011,
            0b010001010100,
            0b010000100001,
            0b110101001011,
            0b000010001101,
            0b101110011001,
            0b100010101100,
            0b110000011111,
            0b100111010111,
            0b010100000110,
            0b000000100010,
            0b111001000100,
            0b000010110111,
            0b111010000100,
            0b101001010110,
            0b110000000010,
            0b001110000001,
            0b111001100001,
            0b001001001011,
            0b000111111000,
            0b000001101000,
            0b001010100011,
            0b011110010011,
            0b011110110101,
            0b101010001011,
            0b110110110011,
            0b001100010011,
            0b001011000011,
            0b100011011010,
            0b101001110011,
            0b111011111111,
            0b011100101110,
            0b101101101111,
            0b011001000000,
            0b100110011001,
            0b111011000100,
            0b001010110001,
            0b010000111011,
            0b101101111010,
            0b010001111111,
            0b001010101110,
            0b000111011111,
            0b100010100011,
            0b011001100101,
            0b010010000000,
            0b110001101011,
            0b000010010000,
            0b011001110010,
            0b011111111111,
            0b000100010101,
            0b000001010001,
            0b101111110010,
            0b010100001010,
            0b001100101110,
            0b010010101001,
            0b100010000110,
            0b100110010001,
            0b011001111100,
            0b000000111011,
            0b100011101000,
            0b100110101101,
            0b110111001110,
            0b110101001100,
            0b101101011110,
            0b100100111101,
            0b101000100101,
            0b011010011101,
            0b010011110011,
            0b101110111000,
            0b110000010110,
            0b101110000110,
            0b001001100110,
            0b000001001010,
            0b001111011001,
            0b110100000011,
            0b101011010011,
            0b111010101001,
            0b000100111000,
            0b011001010100,
            0b001101101011,
            0b011110011001,
            0b000100101001,
            0b000010001000,
            0b011101100100,
            0b110000001000,
            0b010111000100,
            0b101101111100,
            0b110100011110,
            0b001111000100,
            0b001011000110,
            0b011110011010,
            0b010101100111,
            0b110111010110,
            0b001001011101,
            0b011111100100,
            0b110001000101,
            0b001101011000,
            0b001001110001,
            0b000001100101,
            0b111100010001,
            0b111100100110,
            0b001000001000,
            0b010111110010,
            0b011011111011,
            0b101101111011,
            0b100100000010,
            0b001010001000,
            0b011100110011,
            0b010101000101,
            0b101110001010,
            0b001001001101,
            0b110011010101,
            0b000101010101,
            0b100101010100,
            0b101001101000,
            0b000111011001,
            0b001110110101,
            0b000011100101,
            0b001100101111,
            0b001110010101,
            0b000111010100,
            0b110001110111,
            0b101110010100,
            0b010110111111,
            0b010110111001,
            0b101011111001,
            0b101101100010,
            0b100010110000,
            0b111110000011,
            0b011101011010,
            0b010101101110,
            0b100000001101,
            0b111001010001,
            0b101010101101,
            0b111110001110,
            0b010101010000,
            0b011100001010,
            0b011011110100,
            0b101001001111,
            0b011110110010,
            0b101101000000,
            0b111000101101,
            0b001001111000,
            0b011000010111,
            0b001000000011,
            0b010111110111,
            0b111011100001,
            0b010110011000,
            0b110010000100,
            0b010001101011,
            0b000110000010,
            0b100011100100,
            0b010100101111,
            0b110011001111,
            0b100001111110,
            0b100000011110,
            0b000001010010,
            0b001001010100,
            0b000110100110,
            0b010010011101,
            0b101000000100,
            0b010010111000,
            0b000001000010,
            0b110010100101,
            0b101010000100,
            0b111110101101,
            0b100000110010,
            0b100110110001,
            0b001011001101,
            0b110111011111,
            0b101111011000,
            0b111010110001,
            0b101111100111,
            0b110100111100,
            0b110000000001,
            0b000010100110,
            0b110000010101,
            0b010101111000,
            0b001001110101,
            0b010101001000,
            0b010011011101,
            0b100111100010,
            0b111010011000,
            0b011001000110,
            0b010101110111,
            0b101111100001,
            0b101110101100,
            0b101010010110,
            0b101100001001,
            0b110111101101,
            0b100101001100,
            0b111111010010,
            0b010010100010,
            0b111100010111,
            0b000101011100,
            0b100100101100,
            0b100111011100,
            0b100000000010,
            0b110110001100,
            0b011000110010,
            0b011000010101,
            0b101110011110,
            0b101001110000,
            0b001000001010,
            0b001101101001,
            0b011001000111,
            0b010010100001,
            0b011100100100,
            0b001000111001,
            0b110111111110,
            0b011100101000,
            0b110101100010,
            0b111110110010,
            0b100001110111,
            0b101011110101,
            0b001101010011,
            0b001000110100,
            0b001000110010,
            0b110101111010,
            0b101010010011,
            0b110111010111,
            0b001001011110,
            0b010110101011,
            0b100100101101,
            0b000000001100,
            0b001111101010,
            0b000000000000,
            0b110011010011,
            0b101010111101,
            0b011010000000,
            0b110111000101,
            0b111000010111,
            0b101100000110,
            0b011001101110,
            0b100001100011,
            0b000110000110,
            0b110010000010,
            0b100011101111,
            0b011010110001,
            0b101111100110,
            0b111110000101,
            0b000100110111,
            0b101111000010,
            0b011111001010,
            0b000110111000,
            0b101011100101,
            0b111100011011,
            0b111000011011,
            0b011001100001,
            0b000110001110,
            0b101000111010,
            0b011000110011,
            0b001101000011,
            0b010011100000,
            0b011101001110,
            0b001010100000,
            0b101111110011,
            0b100111111010,
            0b001110010010,
            0b111000110001,
            0b101110001000,
            0b011010110000,
            0b100011110110,
            0b010100101001,
            0b000011111101,
            0b111000000001,
            0b001001011000,
            0b000110000000,
            0b011001010110,
            0b111001101010,
            0b101001011000,
            0b011000010011,
            0b001010101101,
            0b010100010000,
            0b111111000010,
            0b101001001011,
            0b101000111001,
            0b010110110010,
            0b111010110100,
            0b110100100100,
            0b001000011000,
            0b011011011000,
            0b011000111110,
            0b010010010001,
            0b001010010000,
            0b100100001110,
            0b110101010000,
            0b011011010000,
            0b011011100000,
            0b100010010010,
            0b001000001001,
            0b111011011101,
            0b010110010011,
            0b111111101100,
            0b100111011011,
            0b010010111010,
            0b001101101110,
            0b010000011111,
            0b111011000110,
            0b111000011100,
            0b111100110000,
            0b111100101011,
            0b010100000100,
            0b001111111100,
            0b000111100111,
            0b100110001111,
            0b001111001100,
            0b000010001001,
            0b101011100111,
            0b110010010100,
            0b011010001011,
            0b011100111100,
            0b010101011111,
            0b110010110000,
            0b110110100010,
            0b011001110000,
            0b010011111000,
            0b001001001001,
            0b101110010011,
            0b000111110100,
            0b100110000100,
            0b011100101011,
            0b000110001000,
            0b001111100101,
            0b000111000000,
            0b101110100011,
            0b000100000011,
            0b111111100011,
            0b101011101001,
            0b010000111101,
            0b011110111100,
            0b110100111011,
            0b110110011001,
            0b101111110110,
            0b011000100001,
            0b101100111011,
            0b110010111011,
            0b001101011101,
            0b110010110001,
            0b000000101011,
            0b111011110101,
            0b011000010110,
            0b110011110010,
            0b011111011000,
            0b010111100101,
            0b110111111101,
            0b000001101001,
            0b011111110000,
            0b101000111101,
            0b011010000100,
            0b110000010100,
            0b110101101111,
            0b111000110011,
            0b001100001111,
            0b111101010110,
            0b001000111111,
            0b000101000101,
            0b000000101100,
            0b000111011011,
            0b010110000011,
            0b001111000101,
            0b111000001000,
            0b001001101100,
            0b011011110111,
            0b001000101100,
            0b111011001001,
            0b010001100100,
            0b011011100010,
            0b101111010011,
            0b101111010101,
            0b100100101110,
            0b111011011111,
            0b010100011011,
            0b000000000110,
            0b010011101001,
            0b011001110101,
            0b110010101110,
            0b011100011001,
            0b110001101110,
            0b001111010100,
            0b000011001100,
            0b100010010001,
            0b101011110100,
            0b000111010111,
            0b000101101001,
            0b111001101000,
            0b000110110001,
            0b010110111110,
            0b101011000101,
            0b110000000111,
            0b010110011010,
            0b001101000000,
            0b101111001100,
            0b001000100000,
            0b000101110010,
            0b111010111001,
            0b000110011110,
            0b011011100101,
            0b001101111110,
            0b000010010100,
            0b101001001100,
            0b000010010110,
            0b000010010101,
            0b011000000110,
            0b000111000011,
            0b101010011100,
            0b111100001110,
            0b011000011010,
            0b001110001001,
            0b101010111100,
            0b101111001110,
            0b011100010010,
            0b011110001000,
            0b111011110001,
            0b101100110011,
            0b010000000100,
            0b010001011010,
            0b101111101010,
            0b100101101011,
            0b110011011010,
            0b101011010001,
            0b101100110010,
            0b000111101000,
            0b101011100011,
            0b100010010110,
            0b111001110101,
            0b100011111011,
            0b101011010100,
            0b111100110001,
            0b001011111111,
            0b010000001111,
            0b011111101111,
            0b001011001011,
            0b000111001110,
            0b101111000111,
            0b110001111010,
            0b100001100111,
            0b110110101110,
            0b110100010000,
            0b110101010110,
            0b101101000100,
            0b000101100111,
            0b001110111101,
            0b110100100000,
            0b011000101000,
            0b010000001001,
            0b001101001010,
            0b101111001000,
            0b100001110110,
            0b111110100011,
            0b110011111010,
            0b101100011100,
            0b011000100000,
            0b011111111101,
            0b101101101100,
            0b111100110011,
            0b010011101010,
            0b011010111101,
            0b000100010111,
            0b101001011101,
            0b110111000111,
            0b011101100001,
            0b111101000010,
            0b000100011010,
            0b010111010010,
            0b000011110010,
            0b010001110100,
            0b011100110101,
            0b011001100010,
            0b010100001100,
            0b000001110101,
            0b001111100001,
            0b001110000110,
            0b100101110000,
            0b000100100000,
            0b101100111101,
            0b001100011010,
            0b011011011010,
            0b011101100110,
            0b001101001000,
            0b001001010111,
            0b000001100000,
            0b100110001000,
            0b010010111111,
            0b101110100010,
            0b001101110101,
            0b011101010100,
            0b101001010111,
            0b111100111101,
            0b101110100101,
            0b110110110110,
            0b011011100011,
            0b000111010001,
            0b100000011100,
            0b000111111101,
            0b111111011101,
            0b111000100110,
            0b001000011011,
            0b000101001111,
            0b011101010101,
            0b100100010111,
            0b111101100100,
            0b011100001100,
            0b101001001101,
            0b001101001001,
            0b001111000111,
            0b001100010101,
            0b000010111111,
            0b011001100100,
            0b111100100010,
            0b010100100011,
            0b001011111000,
            0b110100101111,
            0b011010010000,
            0b101011000011,
            0b100100110010,
            0b010111001101,
            0b101011010111,
            0b010000110010,
            0b011111101000,
            0b001000000111,
            0b101011000110,
            0b001111101011,
            0b000001101110,
            0b000100011111,
            0b110100010010,
            0b010011101110,
            0b001111011101,
            0b010100011100,
            0b101101001000,
            0b101010000101,
            0b010111010000,
            0b001100001101,
            0b011111011101,
            0b101101100101,
            0b010111010111,
            0b001000010000,
            0b111100100111,
            0b011010001000,
            0b111111001000,
            0b100010100001,
            0b010010010111,
            0b001110011100,
            0b010111011011,
            0b111000111000,
            0b111100001010,
            0b100100011111,
            0b110010100100,
            0b101010001001,
            0b110011100111,
            0b110101000101,
            0b000011100001,
            0b100111011111,
            0b110011011011,
            0b101000010110,
            0b110010111111,
            0b111000001011,
            0b111001000111,
            0b111101101111,
            0b110000100100,
            0b011011110000,
            0b010000100010,
            0b000011010010,
            0b000000011100,
            0b100110000010,
            0b110110111101,
            0b010010001011,
            0b000010111100,
            0b100000010110,
            0b110010101010,
            0b110001000110,
            0b000110101110,
            0b000010100000,
            0b011111000001,
            0b100010011001,
            0b111111101010,
            0b001001111011,
            0b101111010110,
            0b010010110000,
            0b111000010010,
            0b111011010110,
            0b010001111000,
            0b011100000101,
            0b001001101000,
            0b110101101001,
            0b110010101000,
            0b111111000101,
            0b101000010111,
            0b111101010011,
            0b101110101010,
            0b001000101000,
            0b001101111111,
            0b111100111111,
            0b110011000001,
            0b011000111001,
            0b111000000011,
            0b111110011110,
            0b011101000001,
            0b001111110000,
            0b000000000010,
            0b011011001101,
            0b110000001101,
            0b000001110111,
            0b011000111111,
            0b001101100110,
            0b100000011000,
            0b010100001110,
            0b011100010100,
            0b011111011100,
            0b100100011000,
            0b110011001011,
            0b111111011010,
            0b101111010100,
            0b110101010101,
            0b011110011011,
            0b111100101101,
            0b010001010000,
            0b101010101010,
            0b101101101110,
            0b010101101001,
            0b010101011110,
            0b110001000000,
            0b101101111001,
            0b100011101110,
            0b001101010110,
            0b100111101010,
            0b011010111010,
            0b110010100010,
            0b010100111000,
            0b110010110100,
            0b010011000110,
            0b110010111000,
            0b111010001001,
            0b000000111110,
            0b110101010100,
            0b110010001001,
            0b110101011000,
            0b110111000001,
            0b000110110100,
            0b110010010110,
            0b011000011100,
            0b100011001001,
            0b111000111011,
            0b001100100111,
            0b001111101000,
            0b100100000001,
            0b000111001101,
            0b000000101111,
            0b000111110111,
            0b100000000101,
            0b010111011100,
            0b011101100111,
            0b011111000101,
            0b010010011110,
            0b000001110110,
            0b111001110010,
            0b111000011101,
            0b111101110010,
            0b110110010011,
            0b000010111110,
            0b010011101000,
            0b111010011100,
            0b111011001010,
            0b101011001011,
            0b110000000011,
            0b101000010010,
            0b110100110011,
            0b100101010111,
            0b101101110100,
            0b010001000001,
            0b111001001011,
            0b011011010110,
            0b110100010111,
            0b111000011010,
            0b000100011011,
            0b101000011011,
            0b001010110011,
            0b100110111000,
            0b010001011111,
            0b111101111110,
            0b111100001100,
            0b010011011011,
            0b101101110000,
            0b111001010101,
            0b010101011100,
            0b011001011110,
            0b001000000110,
            0b010011011000,
            0b000000001001,
            0b010110100101,
            0b110101101010,
            0b011100011010,
            0b101101011100,
            0b100000111100,
            0b100111100000,
            0b111001111000,
            0b000110011111,
            0b010110100000,
            0b000010011100,
            0b110101001110,
            0b000001001001,
            0b011010010001,
            0b010101000010,
            0b001011101011,
            0b110001000011,
            0b110111100001,
            0b110101011010,
            0b101001111110,
            0b000001111001,
            0b001100110010,
            0b110000101001,
            0b000111000001,
            0b011000000111,
            0b100010101110,
            0b011111001110,
            0b001111111111,
            0b011111010001,
            0b000111110101,
            0b100010010100,
            0b111100000010,
            0b001011111001,
            0b100101111111,
            0b101111110100,
            0b111101010100,
            0b011100011101,
            0b111011001100,
            0b010111000011,
            0b010011010100,
            0b110011000100,
            0b000011101111,
            0b100111111111,
            0b100010001000,
            0b111101010001,
            0b111101001011,
            0b101100000010,
            0b011010111001,
            0b101101001101,
            0b100011110100,
            0b111101111111,
            0b011110010001,
            0b001011001001,
            0b100000011001,
            0b101011011010,
            0b011100101100,
            0b111100100000,
            0b100000000001,
            0b010000011011,
            0b101010011000,
            0b111101101000,
            0b101011001101,
            0b011111000111,
            0b100010110011,
            0b001100110100,
            0b000111010000,
            0b100101101100,
            0b010011100001,
            0b010101001111,
            0b011101010001,
            0b100011000011,
            0b010101100100,
            0b111111010001,
            0b000011111100,
            0b111100101000,
            0b100100010011,
            0b101000001000,
            0b110010000110,
            0b010101010011,
            0b000110011010,
            0b110010010101,
            0b001101011010,
            0b101100010111,
            0b100010011100,
            0b101001101010,
            0b100100000110,
            0b100101111101,
            0b011001001101,
            0b001101001111,
            0b011110001111,
            0b000101011010,
            0b111101001100,
            0b001100010001,
            0b101100001100,
            0b011001000001,
            0b011101110010,
            0b001011001110,
            0b100101110001,
            0b111111110001,
            0b001100010110,
            0b101001010100,
            0b001011111010,
            0b000011001001,
            0b000101011000,
            0b000111011000,
            0b010000010101,
            0b101010110101,
            0b010101000000,
            0b100010110101,
            0b111110010001,
            0b110010100001,
            0b000111010110
        };
    }
}