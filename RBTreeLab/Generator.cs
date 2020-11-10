using System;
using System.Collections.Generic;
using System.Linq;
using RBTreeLab.Interfaces;

namespace RBTreeLab
{
    public class Generator : IGenerator
    {
        public List<int> Generate()
        {
            var result = new HashSet<int>();
            var rnd = new Random();
            
            while (result.Count < 25)
            {
                result.Add(rnd.Next(1, 101));
            }
            
            return result.ToList();
        }
    }
}
