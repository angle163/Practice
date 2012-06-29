
using System;
using NUnit.Framework;
using Practice.Extension;

namespace Practice.Web.Sample
{
    [TestFixture]
    public class DecimalToBinary
    {
        static char[] Digits = {
            '0' , '1' , '2' , '3' , '4' , '5' ,
            '6' , '7' , '8' , '9' , 'a' , 'b' ,
            'c' , 'd' , 'e' , 'f' , 'g' , 'h' ,
            'i' , 'j' , 'k' , 'l' , 'm' , 'n' ,
            'o' , 'p' , 'q' , 'r' , 's' , 't' ,
            'u' , 'v' , 'w' , 'x' , 'y' , 'z'
        };

        static char[] DigitTens = {
            '0', '0', '0', '0', '0', '0', '0', '0', '0', '0',
            '1', '1', '1', '1', '1', '1', '1', '1', '1', '1',
            '2', '2', '2', '2', '2', '2', '2', '2', '2', '2',
            '3', '3', '3', '3', '3', '3', '3', '3', '3', '3',
            '4', '4', '4', '4', '4', '4', '4', '4', '4', '4',
            '5', '5', '5', '5', '5', '5', '5', '5', '5', '5',
            '6', '6', '6', '6', '6', '6', '6', '6', '6', '6',
            '7', '7', '7', '7', '7', '7', '7', '7', '7', '7',
            '8', '8', '8', '8', '8', '8', '8', '8', '8', '8',
            '9', '9', '9', '9', '9', '9', '9', '9', '9', '9',
        };

        static char[] DigitOnes = {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
        };

        [Test]
        public void TestMethod()
        {
            long[] someNumbers = new long[31];
            for (int i = 0; i < someNumbers.Length; i++)
            {
                someNumbers[i] = 1 << i;
            }
            string output = string.Empty;
            Console.WriteLine("Right median -- Decimal    ->   Binary: ");
            for (int i = 0; i < someNumbers.Length; i++)
            {
                output = "{0}         -- {1} -> {2}"
                    .FormatWith(
                        i.ToString().PadLeft(4),//从1向右移位的数
                        someNumbers[i].ToString().PadLeft(10),//右移后结果10进制
                        Convert.ToString(someNumbers[i], 2).PadLeft(32)//右移后结果2进制
                    );
                Console.WriteLine(output);
            }
            long orResult = someNumbers[0];
            for (int i = 1; i < someNumbers.Length; i++)
            {
                orResult = orResult | someNumbers[i];
            }
            Console.Write("All numbers do or operation, compute result: {0}", Convert.ToString(orResult, 2).PadLeft(32));
        }

        [Test]
        public void TestMethod2()
        {
            Console.WriteLine("0 ~ 10");
            for (int i = 0; i < 11; i++)
            {
                Console.WriteLine("Custom: {0} -> {1}".FormatWith(i.ToString().PadLeft(4), ToBinaryString(i)));
                Console.WriteLine("System: {0} -> {1}".FormatWith(i.ToString().PadLeft(4), Convert.ToString(i, 2)));
                Console.WriteLine();
            }

            Random rnd = new Random();
            int num = -1;
            Console.WriteLine("ten random numbers");
            for (int i = 0; i < 10; i++)
            {
                num = rnd.Next();
                Console.WriteLine("Custom: {0} -> {1}".FormatWith(num.ToString().PadLeft(4), ToBinaryString(num)));
                Console.WriteLine("System: {0} -> {1}".FormatWith(num.ToString().PadLeft(4), Convert.ToString(num, 2)));
                Console.WriteLine();
            }
        }
        // C# Method
        public string ToBinaryString(int i)
        {
            char[] buf = new char[32];
            int charPos = 32;
            int shift = 1;
            int radix = 1 << shift;
            int mask = radix - 1;
            do
            {
                buf[--charPos] = (i & mask) == 1 ? '1' : '0';
                i >>= shift;
            } while (i != 0);
            return new string(buf, charPos, (32 - charPos));
        }

        [Test]
        public void TestMethod3()
        {
            //Console.WriteLine(6 & 1);
            /*
            Console.WriteLine(Convert.ToString(-3423, 2));
            Console.WriteLine(Convert.ToString(3423, 2));
            Console.WriteLine(ToBinaryString(3423));
             * */
            int n = new Random().Next();
            Console.WriteLine(Convert.ToString(n, 2));
            Console.WriteLine(ToBinaryString(n));
        }


        // -------------------------------------------------------------
        // Java JDK source code
        // file: Integer.java
        // package java.lang
        // --------------------------------------------------------------
        public string ToUnsingedString(int i, int shift)
        {
            char[] buf = new char[32];
            int charPos = 32;
            int radix = 1 << shift;
            int mask = radix - 1;
            do
            {
                buf[--charPos] = Digits[i & mask];
                i >>= shift;
            } while (i != 0);
            return new String(buf, charPos, (32 - charPos));
        }

        [Test]
        public void TestMethod4()
        {
            Random rnd = new Random();
            int n;
            for (int i = 0; i < 10; i++)
            {
                n = rnd.Next();
                Console.Write("Number:{0}, ".FormatWith(n.ToString().PadLeft(12)));
                Console.WriteLine("size: {0}".FormatWith(DigitSize(n).ToString().PadLeft(3)));
            }
        }

        public int DigitSize(int x)
        {
            int[] sizeTable = {
                9, 99, 999, 9999, 99999, 999999, 9999999,
                99999999, 999999999, int.MaxValue
            };
            for (int i = 0; ; )
            {
                if (x <= sizeTable[i++])
                {
                    return i;
                }
            }
        }

        [Test]
        public void TestToString()
        {
            Random rnd = new Random();
            int n = rnd.Next();
            for (int i = 0; i < 1000; i++)
            {
                n = rnd.Next();
                if (!n.ToString().Equals(ToString(n)))
                {
                    Console.WriteLine(n);
                    Console.WriteLine("{1}\n".FormatWith(n, ToString(n)));
                }
            }
        }

        public string ToString(int i)
        {
            if (i == int.MinValue)
            {
                return "-2147483648";
            }

            int size = (i < 0) ? DigitSize(-i) + 1 : DigitSize(i);
            char[] buf = new char[size];
            GetChars(i, size, buf);
            return new string(buf, 0, size);
        }

        public void GetChars(int i, int index, char[] buf)
        {
            int q, r;
            int charPos = index;
            char sign = '0';
            if (i < 0)
            {
                sign = '-';
                i = -i;
            }

            // Generate two Digits per iteration.
            while (i >= 65536)
            {
                q = i / 100;
                //really: r = i - (q * 100);
                r = i - ((q << 6) + (q << 5) + (q << 2));
                i = q;
                buf[--charPos] = Digits[r % 10];
                buf[--charPos] = Digits[r / 10];
            }
            // Fall thru to fast mode for smaller numbers
            // assert(i <= 65536, i);
            for (; ; )
            {
                q = (i * 52429) >> (16 + 3);
                // r = i - (q*10) ...
                r = i - ((q << 3) + (q << 1));
                buf[--charPos] = Digits[r];
                i = q;
                if (i == 0) break;
            }
            if (sign != '0')
            {
                buf[--charPos] = sign;
            }
        }
    }
}