using System.Collections.Generic;

namespace Sudoku.Services.Math;

public class Combinatorics
{
    public List<HashSet<T>> GetAllSubsets<T>(IEnumerable<T> fullSet, int setSize)
    {
        var subsets = new List<HashSet<T>>();
        var remainders = new HashSet<T>(fullSet);
        foreach (var item in fullSet)
        {
            var subset = new HashSet<T>() { item };
            remainders.ExceptWith(subset);
            AddSubset(remainders, subset, subsets, accumulatedSetSize: 1, setSize);
        }
        return subsets;
    }

    private void AddSubset<T>(
        HashSet<T> set,
        HashSet<T> accumulatingSet,
        List<HashSet<T>> subsets,
        int accumulatedSetSize,
        int maxSetSize)
    {
        if (accumulatedSetSize == maxSetSize)
        {
            subsets.Add(accumulatingSet);
            return;
        }

        HashSet<T> remainders = new HashSet<T>(set);
        foreach (var item in set)
        {
            HashSet<T> newAccumulatingSet = new HashSet<T>(accumulatingSet);
            newAccumulatingSet.Add(item);
            remainders.ExceptWith(newAccumulatingSet);
            AddSubset(remainders, newAccumulatingSet, subsets, accumulatedSetSize + 1, maxSetSize);
        }
    }
}
