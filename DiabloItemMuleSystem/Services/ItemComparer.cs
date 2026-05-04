using System.Collections.Generic;
using DiabloItemMuleSystem.Utilities;
using DiabloItemMuleSystem.Models;


namespace DiabloItemMuleSystem.Services
{
    class SortByStat : IComparer<Item>
    {
        public string SortParameter { get; set; }

        public SortByStat(string sortParameter)
        {
            SortParameter = sortParameter;
        }

        public int Compare(Item left, Item right)
        {
            Stats statsLeft = left.GetStat(SortParameter);
            Stats statsRight = right.GetStat(SortParameter);

            return Utils.CompareStat(statsLeft, statsRight);
        }
    }
    public class GenericItemSort : IComparer<Item>
    {
        public string[] SortParameters { get; set; }

        public GenericItemSort(string[] sortParameters)
        {
            SortParameters = sortParameters;
        }

        public int Compare(Item left, Item right)
        {
            return Utils.CompareMultipleStats(left, right, SortParameters);
        }

    }
}