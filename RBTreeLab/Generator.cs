using System;
using System.Collections.Generic;
using System.Linq;
using RBTreeLab.Interfaces;

namespace RBTreeLab
{
    public class Generator : IGenerator
    {
        public IEnumerable<int> Generate(int count, int minValue, int maxValue)
        {
            var result = new HashSet<int>();
            var rnd = new Random();

            while (result.Count < count)
            {
                result.Add(rnd.Next(minValue, maxValue));
            }

            return result.ToList();
        }
    }
}
