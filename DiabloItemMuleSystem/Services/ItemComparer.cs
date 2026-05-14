using System.Collections.Generic;
using DiabloItemMuleSystem.Utilities;
using DiabloItemMuleSystem.Models;


namespace DiabloItemMuleSystem.Services
{
    class SortByStat : IComparer<Item>
    {
        public StatType SortParameter { get; set; }

        public SortByStat(StatType sortParameter)
        {
            SortParameter = sortParameter;
        }

        public int Compare(Item left, Item right)
        {
            Stats statsLeft = left.GetStat(SortParameter);
            Stats statsRight = right.GetStat(SortParameter);

            return StatComparerUtils.Single(statsLeft, statsRight);
        }
    }
    public class GenericItemSort : IComparer<Item>
    {
        public StatType[] SortParameters { get; set; }

        public GenericItemSort(StatType[] sortParameters)
        {
            SortParameters = sortParameters;
        }

        public int Compare(Item left, Item right)
        {
            return StatComparerUtils.Multiple(left, right, SortParameters);
        }

    }
}