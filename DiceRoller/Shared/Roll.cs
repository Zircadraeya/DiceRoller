using System.Collections.Generic;
using System.Linq;

namespace DiceRoller.Shared
{
    public class Roll
    {
        public string Request { get; set; }
        public List<int> Result { get; set; } = new List<int>();
        public int Modifier { get; set; }

        public int Total => Result.Sum() + Modifier;       
    }
}
