using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace SieveAndNthPrime
{
   static class Program
   {
      /* TECHNICAL NOTE
       * 
       * As the maximum number of elements in a vector is limited to the size 
       * of an int (2GB), values ​​greater than 10^8 cannot be executed directly, 
       * requiring the use of big arrays to get around the limitation imposed 
       * by the language.
       * 
       * For this reason, this benchmark test is limited up to 10^8th prime number.
      */

      static void Main()
      {
         bool isValidOption = true;

         while (isValidOption)
         {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\tWhich benchmark test would you like to run?");
            Console.WriteLine();
            Console.WriteLine("\t\t1. Listing up to 100th prime number");
            Console.WriteLine("\t\t2. Benchmark up to 10^8th prime number");
            Console.WriteLine();
            Console.WriteLine("\t'Esc' to exit.");
            Console.WriteLine();
            Console.Write("Choose the test: ");

            ConsoleKeyInfo keyInfo = Console.ReadKey();

            switch (keyInfo.Key)
            {
               case ConsoleKey.Escape:
                  return;

               case ConsoleKey.D1:
               case ConsoleKey.NumPad1:
                  Console.WriteLine();
                  isValidOption = true;
                  List100Primes.GenerateLists();
                  break;

               case ConsoleKey.D2:
               case ConsoleKey.NumPad2:
                  Console.WriteLine();
                  isValidOption = true;
                  BenchmarkRunner.Run<PrimeBenchmarks>();
                  break;

               default:
                  isValidOption = false;
                  break;
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            _ = Console.ReadKey();
         }
      }
   }

   public  class List100Primes()
   {
      private static readonly List<long> primesSieve = [];
      private static readonly List<long> primesEratosthenes = [];
      private static readonly List<long> primesAtkin = [];

      public static void GenerateLists()
      {
         for (int i = 1; i <= 100; i++)
         {
            primesSieve.Add(Sieves.SieveAndNthPrime(i));
            primesEratosthenes.Add(Sieves.SieveOfEratosthenes(i));
            primesAtkin.Add(Sieves.SieveOfAtkin(i));
         }

         Console.WriteLine("Primes list for Sieve6k±1:");
         Console.WriteLine(string.Join(" ", primesSieve));
         Console.WriteLine();

         Console.WriteLine("Primes list for Eratosthenes:");
         Console.WriteLine(string.Join(" ", primesEratosthenes));
         Console.WriteLine();

         Console.WriteLine("Primes list for Atkin:");
         Console.WriteLine(string.Join(" ", primesAtkin));
         Console.WriteLine();

         bool sieveEqualEratosthenes = primesSieve.SequenceEqual(primesEratosthenes);
         bool eratosthenesEqualsAtkin = primesEratosthenes.SequenceEqual(primesAtkin);

         if (sieveEqualEratosthenes && eratosthenesEqualsAtkin)
            Console.WriteLine("Sequences are equals!");

         if (!sieveEqualEratosthenes && eratosthenesEqualsAtkin)
            Console.WriteLine("Sieve6k±1 sequence is different!");

         if (sieveEqualEratosthenes && !eratosthenesEqualsAtkin)
            Console.WriteLine("Atkin's sequence is different!");

         if (!sieveEqualEratosthenes && !eratosthenesEqualsAtkin)
            Console.WriteLine("Sequences are not equals!");
      }
   }

   [MemoryDiagnoser]
   public class PrimeBenchmarks
   {
      // Values ​​for nth prime numbers
      private static readonly List<long> NthPrimes = [
         2L,                // 10^0  =              1
         29L,               // 10^1  =             10
         541L,              // 10^2  =            100
         7919L,             // 10^3  =          1.000
         104_729L,          // 10^4  =         10.000
         1_299_709L,        // 10^5  =        100.000
         15_485_863L,       // 10^6  =      1.000.000
         179_424_673L,      // 10^7  =     10.000.000
         2_038_074_743L,    // 10^8  =    100.000.000
         22_801_763_489L,   // 10^9  =  1.000.000.000
         252_097_800_623L   // 10^10 = 10.000.000.000
      ];

      [Params(10, 100, 1_000, 10_000, 100_000, 1_000_000, 10_000_000, 100_000_000)]
      public long N;

      [Benchmark(Baseline = true)]
      public bool Crivo6k1()
      {
         int pos = (int)Math.Log10(N);
         return Sieves.SieveAndNthPrime(N) == NthPrimes[pos];
      }

      [Benchmark]
      public bool CrivoEratostenes()
      {
         int pos = (int)Math.Log10(N);
         return Sieves.SieveOfEratosthenes(N) == NthPrimes[pos];
      }

      [Benchmark]
      public bool CrivoAtkin()
      {
         int pos = (int)Math.Log10(N);
         return Sieves.SieveOfAtkin(N) == NthPrimes[pos];
      }
   }
}
