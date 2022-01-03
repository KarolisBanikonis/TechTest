using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaneSelection
{
    public class Destination
    {
        private int Number { get; set; }
        private int TotalLoadCount { get; set; }
        private int ReachedLoadCount { get; set; }
        public Destination(int nr)
        {
            Number = nr;
        }
        public int GetNumber()
        {
            return Number;
        }
        public int GetTotalLoadCount()
        {
            return TotalLoadCount;
        }
        public int GetReachedLoadCount()
        {
            return ReachedLoadCount;
        }
        public void IncreaseTotalLoadCount()
        {
            TotalLoadCount += 1;
        }
        public void IncreaseReachedLoadCount()
        {
            ReachedLoadCount += 1;
            TotalLoadCount += 1;
        }
        public double CalculateReachedPercentage()
        {
            return Math.Round((ReachedLoadCount + 0.0) / TotalLoadCount * 100);
        }
    }
}
