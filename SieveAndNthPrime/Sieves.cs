using System;

namespace SieveAndNthPrime
{
   internal class Sieves
   {
      /// <summary>
      /// 
      /// </summary>
      /// <param name="n">n é o n-ésimo número primo procurado</param>
      /// <returns></returns>
      public static long SieveAndNthPrime(long n)
      {
         // para os 4 primeiros números retorna direto
         if (n < 5)
            return new long[] { 2, 3, 5, 7 }[n - 1];

         // Posição da coluna onde o n-ésimo número primo deveria estar
         // Como há duas linhas (famílias) de números dividimos n por 2
         // n % 2 resulta se n está na primeira ou segunda linha
         // -2 ajuste para corrigir o posicionamento devido aos números 2 e 3
         long c = (n + n % 2) / 2 - 2;

         // Estimativa superior para o n-ésimo número (conforme TNP)
         // Aqui é calculado para a coluna
         double logn = Math.Log(c);

         // O limite superior de elementos da matriz
         // O acréscimo de 6 unidades é para que o limite superior
         // Seja suficiente quando usamos para valores pequenos de n
         // Valor empírico
         long upperbound = (long)Math.Round(logn * c) + 6;
         
         // Limite superior deve ser no mínimo igual a n
         // Devido a divisão de inteiros do C# resultar em inteiros
         if (upperbound / 2 <= n) upperbound = upperbound / 2 * 2 + 2;

         // Limite das colunas a serem checadas
         // A iteração irá checar as duas linha a cada passo
         long c0 = upperbound / 2;

         // Vetor para armazenas os números primos
         // Por padrão consideramos todos os números primos
         // E marcamos no vetor somente os compostos
         bool[] sieve = new bool[upperbound];

         long k = 0;    // coluna inicial
         long p = 5;    // número primo inicial da linha 1
         long q = 7;    // número primo inicial da linha 2

         // Laço realizado enquanto p é menor ou igual
         // Ao limite de checagagem das colunas
         // Ao final do passo k é incrementado (a coluna é movida à direita)
         // p e q são incrementados resultando na nova base inicial
         for (; p <= c0; k++, p += 6, q += 6)
         {
            // Marca os múltiplos do padrão 1 (linha x linha 2)
            long k0 = k + p;           // p fixado
            long pos0 = k0 + k0;       // próximo múltiplo da coluna (no vetor)
            long t0 = (p - 5) / 6 * 2; // posição no vetor do próximo múltiplo

            // Marca os múltiplos de p somente se a próxima posição
            // Estiver dentro do limite superior e não for composta
            // Exemplo: 35 é multiplo de 5 e já foi marcado anterior
            // Evitar o próximo laço desnecessário
            if (t0 < upperbound && !sieve[t0])
            {
               // Marca as colunas que são múltiplos de p x q
               for (; pos0 < upperbound; pos0 += p + p)
               {
                  sieve[pos0] = true;
               }
            }

            // Marca os múltiplos do padrão 2 (line 1 x linha 1)
            long k1 = p - 2 - k;       // coluna base 
                                       // a posição é 2 colunas antes da coluna múltipla de p
                                       // subtraindo o deslocamento inicial
            long pos1 = k1 + k1 + 1;   // próxima posição no vetor deve ser iniciado após p
                                       // Exemplo: p = 5 -> pos1 = 7

            // Marca as colunas que são múltiplos de p x p
            for (; pos1 < upperbound; pos1 += p + p)
            {
               sieve[pos1] = true;
            }

            // Marca os múltiplos do padrão 2 (line 2 x linha 2)
            long k2 = k + q;                 // q fixado
            long pos2 = k2 + k2 + 1;         // próximo múltiplo da coluna (no vetor)
            long t2 = (q - 7) / 6 * 2 + 1;   // posição no vetor do próximo múltiplo

            // +1 pois cada 2 elementos no vetor corresponde a uma coluna
            // posição par: linha 1; posição ímpar: linha 2

            // Marca os múltiplos de q somente se a próxima posição
            // Estiver dentro do limite superior e não for composta
            // Exemplo: 49 é multiplo de 7 e já foi marcado anterior
            // Evitar o próximo laço desnecessário
            if (t2 < upperbound && !sieve[t2])
            {
               // Marca as colunas que são múltiplos de q x q
               for (; pos2 < upperbound; pos2 += q + q)
               {
                  sieve[pos2] = true;
               }
            }
         }

         // Conta 2 e 3 que são primos e não estão na lista
         long count = 2;

         // Laço para determinar em que posição da tabela encontra-se n
         for (long i = 0; i < upperbound; i++)
         {
            // Não é composto: PRIMO
            if (!sieve[i])
            {
               count++;          // incrementa a contagem
               if (count == n)   // encontrou n
               {
                  i++;           // incrementa i para ajusta a posição
                  long col = (i + i % 2) / 2 - 1;     // determina a coluna da tabela
                  long row = 2 - (i % 2);             // determina a linha
                  return 3 + row * 2 + 6 * col;       // retorna o n-ésimo primo

                  // Onde:
                  // row = 1 ou 2 -> resulta em 5 ou 7 
                  // col = k -> da fórmula 5 ou 7 + 6k
               }
            }
         }

         // Retorna 0 se não encontra
         return 0;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="n">n é o n-ésimo número primo procurado</param>
      /// <returns></returns>
      /// <exception cref="ArgumentException"></exception>
      /// <exception cref="Exception"></exception>
      public static long SieveOfEratosthenes(long n)
      {
         if (n < 1)
         {
            throw new ArgumentException("n deve ser maior que 0");
         }

         if (n == 1) return 2;
         if (n == 2) return 3;

         // Upperbound estimate for the n-th prime number
         double logn = Math.Log(n);
         double loglogn = Math.Log(logn);

         long upperBound = (int)Math.Round(n * (logn + loglogn)) + 2;

         bool[] sieve = new bool[upperBound];

         for (long i = 0; i < upperBound; i++)
         {
            sieve[i] = true;
         }

         sieve[0] = sieve[1] = false;
         int count = 0;

         for (long p = 2; p < upperBound; p++)
         {
            if (sieve[p])
            {
               count++;
               if (count == n)
               {
                  return p;
               }
               for (long multiple = p * p; multiple < upperBound; multiple += p)
               {
                  sieve[multiple] = false;
               }
            }
         }

         throw new Exception("Could not find n-th prime within upper bound estimate.");
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="k">k é o k-ésimo número primo procurado</param>
      /// <returns></returns>
      public static long SieveOfAtkin(long k)
      {
         // Upperbound estimate for the n-th prime number
         double logn = Math.Log(k);

         // For k = 1, log n = 0. Prevents error.
         double loglogn = logn == 0 ? 1 : Math.Log(logn);

         long limit = (int)Math.Ceiling(k * logn + k * loglogn) + 1;
         
         // Allow a minimum space for processing smaller values: k < 5.
         if (limit < 10) limit = 10;

         // Initialise the sieve array with false values
         bool[] sieve = new bool[limit];

         //for (long i = 0; i < limit; i++)
         //   sieve[i] = false;

         /* Mark sieve[n] is true if one of the
         following is true:
         a) n = (4*x*x)+(y*y) has odd number
            of solutions, i.e., there exist
            odd number of distinct pairs
            (x, y) that satisfy the equation
            and    n % 12 = 1 or n % 12 = 5.
         b) n = (3*x*x)+(y*y) has odd number
            of solutions and n % 12 = 7
         c) n = (3*x*x)-(y*y) has odd number
            of solutions, x > y and n % 12 = 11 */

         for (long x = 1; x * x < limit; x++)
         {
            for (long y = 1; y * y < limit; y++)
            {
               // Main part of Sieve of Atkin
               long n = (4 * x * x) + (y * y);
               if (n < limit && (n % 12 == 1 || n % 12 == 5))
                  sieve[n] ^= true;

               n = (3 * x * x) + (y * y);
               if (n < limit && n % 12 == 7)
                  sieve[n] ^= true;

               n = (3 * x * x) - (y * y);
               if (x > y && n < limit && n % 12 == 11)
                  sieve[n] ^= true;
            }
         }

         // Mark all multiples of squares as
         // non-prime
         for (long r = 5; r * r < limit; r++)
         {
            if (sieve[r])
            {
               for (long i = r * r; i < limit; i += r * r)
                  sieve[i] = false;
            }
         }

         // 2 e 3 are primes
         sieve[2] = sieve[3] = true;
         long count = 0;

         // return nth-prime
         for (long a = 2; a < limit; a++)
         {
            if (sieve[a])
            {
               count++;
               if (count == k) return a;
            }
         }

         return 0;
      }
   }
}
