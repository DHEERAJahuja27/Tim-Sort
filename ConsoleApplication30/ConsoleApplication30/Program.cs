using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApplication24
{
    class Filing
    {
        string mydocpath = "C:\\Users\\Abdul Basit\\Desktop\\Tim Sort.txt";
        public void Writer(int[] Array)
        {
            using (StreamWriter SW = new StreamWriter(mydocpath))
            {
                for (int i = 0; i < Array.Length; i++)
                {
                    SW.WriteLine(Array[i]);
                }
            }
        }
        public int Getlength()
        {
            int Count = 0;
            try
            {
                using (StreamReader SR = new StreamReader(mydocpath))
                {
                    string line;
                    while ((line = SR.ReadLine()) != null)
                    {
                        Count++;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error 404! File Not Found");
                Console.WriteLine(e.Message);
            }
            return Count;
        }
        public int[] Reader(int Len)
        {
            int[] Arr = new int[Len];
            try
            {
                using (StreamReader SR = new StreamReader(mydocpath))
                {
                    for (int i = 0; i < Len; i++)
                    {
                        Arr[i] = Convert.ToInt16(SR.ReadLine());
                        Console.WriteLine("  Array[{0}] : {1}", i, Arr[i]);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error 404! File Not Found");
                Console.WriteLine(e.Message);
            }
            return Arr;
        }
    }
    class Program
    {
        int RUN = 32;
        public void insertionSort(int[] arr, int left, int right)
        {
            for (int i = left + 1; i <= right; i++)
            {
                int temp = arr[i];
                int j = i - 1;
                while (arr[j] > temp && j >= left)
                {
                    arr[j + 1] = arr[j];
                    j--;
                }
                arr[j + 1] = temp;
            }
        }
        public void merge(int[] arr, int l, int m, int r)
        {
            int len1 = m - l + 1, len2 = r - m;
            int[] left = new int[len1];
            int[] right = new int[len2];
            for (int i = 0; i < len1; i++)
            {
                left[i] = arr[l + i];
            }
            for (int i = 0; i < len2; i++)
            {
                right[i] = arr[m + 1 + i];
            }
            int s = 0;
            int j = 0;
            int k = l;
            while (s < len1 && j < len2)
            {
                if (left[s] <= right[j])
                {
                    arr[k] = left[s];
                    s++;
                }
                else
                {
                    arr[k] = right[j];
                    j++;
                }
                k++;
            }
            while (s < len1)
            {
                arr[k] = left[s];
                k++;
                s++;
            }
            while (j < len2)
            {
                arr[k] = right[j];
                k++;
                j++;
            }
        }
        public int[] timSort(int[] arr)
        {
            for (int i = 0; i < arr.Length; i += RUN)
                insertionSort(arr, i, i + 31);
            for (int size = RUN; size < arr.Length; size = 2 * size)
            {
                for (int left = 0; left < arr.Length; left += 2 * size)
                {
                    int mid = left + size - 1;
                    int right = min((left + 2 * size - 1), (arr.Length - 1));
                    merge(arr, left, mid, right);
                }
            }
            return arr;
        }
        public void printArray(int[] arr, int n)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine("{0}", arr[i]);
            }
        }
        static void Main(string[] args)
        {

            Filing F = new Filing();
            int Len = F.Getlength();
            Console.WriteLine("\n Given array is ");
            int[] arr = F.Reader(Len);
            Program ts = new Program();
            Console.WriteLine("\n\n  After sorting array is ");
            arr = ts.timSort(arr);
            for (int i = 0; i < Len; i++)
            {
                Console.WriteLine("  Array[{0}] : {1}", i, arr[i]);
            }
            F.Writer(arr);
            Console.ReadLine();
        }
    }
}

