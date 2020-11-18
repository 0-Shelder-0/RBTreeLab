using System.Collections.Generic;

namespace RBTreeLab.Interfaces
{
    public interface IGenerator
    {
        IEnumerable<int> Generate(int count, int minValue, int maxValue);
    }
}
