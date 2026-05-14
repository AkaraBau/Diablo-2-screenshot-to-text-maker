using DiabloItemMuleSystem.Utilities;
using System;

namespace DiabloItemMuleSystem.Models
{
    public class Stats
    {
        public Guid Id { get; set; }
        public Guid ItemId {  get; set; } 
        public readonly StatType Name;
        public readonly int Amount; 

        public Stats(Guid ID ,string data, StatType stat)
        {
            Id = Guid.NewGuid();
            ItemId = ID;
            Amount = StringUtils.ExtractInt(data);
            Name = stat; 
        }
        // empty constructor for the ItemContext
        public Stats()
        {

        }
        public override string ToString()
        {
            string result = $"{Amount}{Name}";
            return result;
        }

        public override bool Equals(object obj)
        {
            Stats other = obj as Stats;

            if (other == null) return false;

            else if (this.Name == other.Name && this.Amount == other.Amount)
            {
                return true;
            }

            else return false; 
             

        }
        public override int GetHashCode() 
            { 
            return this.Amount.GetHashCode() ^ this.Name.GetHashCode(); 
        }   
    }
}