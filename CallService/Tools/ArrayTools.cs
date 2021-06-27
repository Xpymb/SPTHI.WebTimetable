using CallService.Calls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CallService.Tools
{
    public static class ArrayTools
    {
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
        public static List<Call> BubbleSort(List<Call> listCalls) //BubleSort of List<Call>
        {
            var lastIndex = listCalls.Count;

            for (int i = 0; i < listCalls.Count; i++)
            {
                for (int j = 0; j < lastIndex; j++)
                {
                    if (j + 1 != lastIndex && listCalls[j].CallTime > listCalls[j + 1].CallTime)
                    {
                        var saveVar = listCalls[j + 1];
                        listCalls[j + 1] = listCalls[j];
                        listCalls[j] = saveVar;
                    }
                }

                lastIndex--;
            }

            return listCalls;
        }
    }
}
