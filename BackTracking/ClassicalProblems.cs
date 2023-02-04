using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructureRepo.BackTracking
{
    /*  https://leetcode.com/explore/learn/card/recursion-ii/472/backtracking/2654/
       Backtracking is a general algorithm for finding all (or some) solutions to some computational problems
      (notably Constraint satisfaction problems or CSPs), which incrementally builds candidates to the solution and abandons a candidate 
      ("backtracks")  as soon as it determines that the candidate cannot lead to a valid solution
      There are three types of problems in backtracking : 

        1-  Decision Problem – In this, we search for a feasible solution.
        2 - Optimization Problem – In this, we search for the best solution.
        3 - Enumeration Problem – In this, we find all feasible solutions.
     */


    /*
     * BackTracking pseudocode template
      
    void findSolutions(n, other params) :
        if (found a solution) :
            solutionsFound = solutionsFound + 1;
            displaySolution();
            if (solutionsFound >= solutionTarget) : 
                System.exit(0);
            return

        for (val = first to last) :
            if (isValid(val, n)) :
                applyValue(val, n);
                findSolutions(n+1, other params);
                removeValue(val, n);
     */


    /*
     Given two integers n and k, return all possible combinations of k numbers chosen from the range [1, n].
    You may return the answer in any order.
     https://leetcode.com/problems/combinations/description/
     */
    public class Combinations
    {
        IList<IList<int>> result = new List<IList<int>>();
        int n;
        int k;
        public IList<IList<int>> Combine(int n, int k)
        {
            this.n = n;
            this.k = k;
            
            Backtrack(1, new List<int>());
            return result;
        }

        private void Backtrack(  int start, List<int> combination)
        {
            if (combination.Count == k)
            {
                result.Add(new List<int>(combination));
                return;
            }
            for (int i = start; i <= n; i++)
            {
                combination.Add(i);
                Backtrack( i + 1, combination);
                combination.RemoveAt(combination.Count - 1);
            }
        }
    }
}
