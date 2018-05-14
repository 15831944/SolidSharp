﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace SolidSharp
{
	internal static class MathUtil
	{
		private static readonly uint[] SmallestPrimes = ComputeSmallestPrimes();

		private static uint[] ComputeSmallestPrimes()
		{
			var primes = new List<uint> { 2, };

			// Allocate ~8KB of data for determining the first few primes
			var array = new BitArray(65536, true);

			for (int i = 1; i < 256; i++)
			{
				if (array[i])
				{
					int p = (i << 1) + 1;

					primes.Add(unchecked((uint)p));
					
					for (int j = i + p; j < 6556; j += p)
					{
						array[j] = false;
					}
				}
			}

			return primes.ToArray();
		}

		public static ulong Gcd(ulong a, ulong b)
		{
			int d = 0;

			while ((a & 1) == 0 && (b & 1) == 0)
			{
				++d;
				a >>= 1;
				b >>= 1;
			}

			while (a != b)
			{
				if ((a & 1) == 0) a >>= 1;
				else if ((b & 1) == 0) b >>= 1;
				else if (a > b) a = (a - b) >> 1;
				else b = (b - a) >> 1;
			}

			return a * (1u << d);
		}

		public static ulong Gcd(long a, long b)
		{
			if (a < 0) a = unchecked(-a);
			if (b < 0) b = unchecked(-b);

			return Gcd(unchecked((ulong)a), unchecked((ulong)b));
		}

		public static long Pow(long x, long n)
		{
			if (x == 0 && n == 0) throw new InvalidOperationException("0⁰ is undefined.");
			if (x == 0) return 0;
			if (x == 0) return 1;

			long r = 1;

			while (n > 1)
			{
				if ((n & 1) != 0)
				{
					r = checked(x * r);
					--n;
				}
				x = checked(x * x);
				n >>= 1;
			}

			return x * r;
		}

		public static void Swap<T>(ref T a, ref T b)
		{
			var c = a;
			a = b;
			b = c;
		}

		public static long IntSqrt(long x)
			=> unchecked((long)IntSqrt(checked((ulong)x)));

		public static ulong IntSqrt(ulong x)
		{
			if (x < 2) return x;

			int shiftCount = 2;
			while ((x >> shiftCount) != 0)
			{
				shiftCount += 2;
			}

			ulong result = 0;
			while (shiftCount >= 0)
			{
				ulong next = (result <<= 1) + 1;

				if (next * next <= (x >> shiftCount))
				{
					result = next;
				}

				shiftCount -= 2;
			}

			return result;
		}

		public static (long factor, long square) SimplifySqrt(long x)
		{
			if (x < 0) throw new ArgumentOutOfRangeException(nameof(x));

			var (factor, square) = SimplifySqrt(unchecked((ulong)x));

			return unchecked(((long)factor, (long)square));
		}

		public static (ulong Factor, ulong Square) SimplifySqrt(ulong x)
		{
			ulong factor = 1; // The factor applied to the square root. For perfect squares, this will end up being sqrt(x).
			ulong square = x; // The current value of the square whose root shall be found. For perfect squares, this will end up being 1.
			ulong squareFactors = 1; // Accumulate the non-squared prime numbers there. For perfect squares, this stay 1.

			// Very easily simplify powers of 4.
			while ((square & 3) == 0)
			{
				square >>= 2;
				factor <<= 1;
			}

			// Remove the remaining power of two, if any.
			if ((square & 1) == 0)
			{
				squareFactors <<= 1;
				square >>= 1;
			}

			// Now square is *not* a multiple of four…
			// Any number up to 8 would have been simplified by the code above.
			if (square <= 8) goto Completed;

			// Find an estimate of sqrt(x) to determine the maximum possible factor.
			uint sqrt = checked((uint)IntSqrt(square));

			// If we precisely found sqrt(x), we can stop there.
			if (sqrt * sqrt == square)
			{
				factor *= sqrt;
				square = 1;
			}
			// Naive iterative algorithm for factoring the value of square in prime numbers and squares of prime numbers.
			else if (square <= long.MaxValue)
			{
				// Try dividing by a few prime's squares.
				for (int i = 1; i < SmallestPrimes.Length; i++)
				{
					uint p = SmallestPrimes[i];
					ulong sp = p * p;

					if (sp > square) break; // NB: We know that sp ≠ square because of the integer square root computation above.

					while (square % sp == 0)
					{
						factor *= p;
						square = square / sp;
					}

					if (square % p == 0)
					{
						// Still reduce the size of square, trying to reduce the required number of iterations.
						square /= p;
						squareFactors *= p;

						if (square == 1) break;
					}
				}
			}

		Completed:;
			return (factor, square * squareFactors);
		}
	}
}
