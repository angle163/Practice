using System;
using NUnit.Framework;

namespace Practice.Web.Sample
{
    [TestFixture]
    public class NumberSort
    {
        private void BubbleSort(int[] arr)
        {
            int i, j;
            for (i = 0; i < arr.Length; i++)
            {
                for (j = arr.Length - 1; j > i; j--)
                {
                    if (arr[j] < arr[j - 1])
                    {
                        arr[j] ^= arr[j - 1] ^= arr[j];
                        arr[j - 1] ^= arr[j];
                    }
                }
            }
        }

        private void SelectionSort(int[] arr)
        {
            int i, j, min;
            for (i = 0; i < arr.Length; i++)
            {
                min = i;
                for (j = i; j < arr.Length; j++)
                {
                    if (arr[min] > arr[j])
                    {
                        min = j;
                    }
                }
                if (min != i)
                {
                    arr[min] ^= arr[i] ^= arr[min];
                    arr[i] ^= arr[min];
                }
            }
        }

        private void InsertionSort(int[] arr)
        {
            int i, temp, pos;
            for (i = 1; i < arr.Length; i++)
            {
                temp = arr[pos = i];
                /*
                while (pos > 0 && arr[pos - 1] > temp)
                {
                    arr[pos] = arr[--pos];
                }
                */
                for (; pos > 0 && arr[pos - 1] > temp; arr[pos] = arr[--pos]) ;
                arr[pos] = temp;
            }
        }

        [Test]
        public void TestBubbleSort()
        {
            int[] arr = { 3, 5, 2, 7, 9, 6, 1, 4, 0, 8 };
            Console.WriteLine("Bubble Sort:");
            BubbleSort(arr);
            Write(arr);
        }

        [Test]
        public void TestSelectionSort()
        {
            int[] arr = { 3, 5, 2, 7, 9, 6, 1, 4, 0, 8 };
            Console.WriteLine("Selection Sort:");
            SelectionSort(arr);
            Write(arr);
        }

        [Test]
        public void TestInsertionSort()
        {
            int[] arr = { 3, 5, 2, 7, 9, 6, 1, 4, 0, 8 };
            Console.WriteLine("Insertion Sort:");
            InsertionSort(arr);
            Write(arr);
        }

        private void Write(int[] arr)
        {
            foreach (int i in arr)
            {
                Console.Write("{0} ", i);
            }
            Console.WriteLine();
        }
    }
}