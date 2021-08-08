using System;
using System.Collections.Generic;

namespace ScheduleController.Tools
{
    public static class ArrayTools
    {
        public static int[] BubbleSort(int[] arr) //BubleSort of double array
        {
            var lastIndex = arr.Length;

            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < lastIndex; j++)
                {
                    if (j + 1 != lastIndex && arr[j] > arr[j + 1])
                    {
                        var saveVar = arr[j + 1];
                        arr[j + 1] = arr[j];
                        arr[j] = saveVar;
                    }
                }

                lastIndex--;
            }

            return arr;
        }

        public static List<string> BubbleSort(List<string> list) //BubleSort of double array
        {
            var lastIndex = list.Count;

            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < lastIndex; j++)
                {
                    if (j + 1 != lastIndex && Convert.ToInt32(list[j][0]) > Convert.ToInt32(list[j + 1][0]))
                    {
                        var saveVar = list[j + 1];
                        list[j + 1] = list[j];
                        list[j] = saveVar;
                    }
                }

                lastIndex--;
            }

            return list;
        }

        public static double[] BubbleSort(double[] arr) //BubleSort of double array
        {
            var lastIndex = arr.Length;

            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < lastIndex; j++)
                {
                    if (j + 1 != lastIndex && arr[j] > arr[j + 1])
                    {
                        var saveVar = arr[j + 1];
                        arr[j + 1] = arr[j];
                        arr[j] = saveVar;
                    }
                }

                lastIndex--;
            }

            return arr;
        }
    }
}
