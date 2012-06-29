using System;
using NUnit.Framework;
using Practice.Extension;

namespace Practice.Web.Sample
{
    [TestFixture]
    public class ExamForZhuMoran
    {
        [Test]
        public void MonkeyAndPeachViaLoop()
        {
            /**
             * 海滩上有一堆桃子，五只猴子来分。第一只猴子把这堆桃子凭据分为五份，
             * 多了一个，这只猴子把多的一个扔入海中，拿走了一份。第二只猴子把剩
             * 下的桃子又平均分成五份，又多了一个，它同样把多的一个扔入海中，拿
             * 走了一份，第三、第四、第五只猴子都是这样做的，问海滩上原来最少有
             * 多少个桃子？
             **/
            int last, guess, loop, total;
            guess = 3;
            while (true)
            {
                last = guess;
                for (loop = 0; loop < 5; loop++)
                {
                    // last 为假设猴子均分后剩下的桃子数；
                    // 那么，last * 5 + 1为猴子分成5份后，扔掉多余的一个，
                    // 拿走一份后剩下的桃子数量，所剩的这份一定为4的倍数
                    if ((5 * last + 1) % 4 > 0)
                        break;
                    last = (5 * last + 1) / 4;
                }

                if (loop == 4)
                {
                    total = last * 5 + 1;
                    break;
                }

                guess += 4;
            }
            Console.WriteLine("桃子至少有{0}个。".FormatWith(total));
        }

        [Test]
        public void MonkeyAndPeachViaRecursion()
        {
            int total;
            for (total = 1; total < int.MaxValue; total++)
            {
                if (LeaveByMod5(total, 5))
                {
                    break;
                }
            }
            Console.WriteLine("桃子至少有{0}个。".FormatWith(total));
        }

        /*
        bool LeaveByMod5(int total, int copies)
        {
            if (total % 5 == 1)
            {
                if (copies == 1)
                {
                    return true;
                }

                return LeaveByMod5((total - 1) * 4 / 5, --copies);
            }

            return false;
        }
        */

        bool LeaveByMod5(int total, int copies)
        {
            if (total % 5 != 1)
                return false;

            if (copies == 1)
                return true;

            return LeaveByMod5((total - 1) / 5 * 4, --copies);
        }
    }
}