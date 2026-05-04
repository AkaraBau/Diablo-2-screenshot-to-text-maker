using DiabloItemMuleSystem.Utilities;

namespace DiabloItemMuleSystem.Models
{
    public class Stats
    {
        private static int GenerateStatsId = Database.GetHighestId("Stats") + 1; 
        public int StatsId { get; set; } // Id unique to every stats
        public int ItemId {  get; set; } // Shared id with the item it "belongs too"
        public readonly string Name;
        public readonly int Amount; 

        public Stats(int ID ,string data)
        {
            ItemId = ID;
            StatsId = GenerateStatsId++;
            Amount = StringUtils.ExtractInt(data);
            Name = StringUtils.RemoveNumbers(data);

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