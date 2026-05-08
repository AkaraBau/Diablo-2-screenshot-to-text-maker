using DiabloItemMuleSystem.Models;
using System.Collections.Generic;

namespace DiabloItemMuleSystem.Utilities
{
    public static class StatComparer
    {
        public static int Single(Stats inputA, Stats inputB)
        {

            if (inputA == null && inputB == null)
            {
                return 0;
            }
            else if (inputA == null)
            {
                return 1;
            }
            else if (inputB == null)
            {
                return -1;
            }
            else if (inputA != null && inputB != null)
            {
                var result = (int)inputB.Amount - (int)inputA.Amount;

                return result;
            }

            return 0;
        }
        public static int Multiple(Item left, Item right, StatType[] sortParameters)
        {


            for (int i = 0; i < sortParameters.Length; i++)
            {
                Stats statLeft = left.GetStat(sortParameters[i]);
                Stats statRight = right.GetStat(sortParameters[i]);

                int result = Single(statLeft, statRight);

                if (result != 0)
                {
                    return result;
                }

            }
            return 0;
        }
        public static bool CheckIfEqual(List<Stats> left, List<Stats> right)
        {



            if (left.Count != right.Count) return false;

            for (int i = 0; i < left.Count - 1; i++)
            {
                if (!left[i].Equals(right[i]))
                {
                    return false;
                }
            }



            return true;

        }
    }
}