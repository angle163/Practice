using System;
using NUnit.Framework;

namespace Practice.Web.Sample
{
    [TestFixture]
    public class ArrayModify
    {
        [Test]
        public void TestMethod()
        {
            int[] arr = new int[10];
            int i;
            for (i = 0; i < arr.Length; arr[i] = i++) ;
            PrintArray(arr);
            Console.WriteLine();
            ModifyArray(arr);
            PrintArray(arr);
        }

        private void PrintArray(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine(arr[i]);
            }
        }

        private void ModifyArray(int[] arr)
        {
            for (int i = 0; i < arr.Length; arr[i] = 1 << arr[i++]) ;
        }
    }
}