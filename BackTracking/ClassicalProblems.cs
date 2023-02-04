using System;
using System.Collections.Generic;

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

namespace DataStructureRepo.BackTracking
{
    /*
     * 
    Find all valid combinations of k numbers that sum up to n such that the following conditions are true:
    Only numbers 1 through 9 are used.
    Each number is used at most once.
    Return a list of all possible valid combinations. 
    The list must not contain the same combination twice, and the combinations may be returned in any order.
    https://leetcode.com/problems/combination-sum-iii/
    */
    class CombinationSumIII
    {
        private List<IList<int>> _result = new List<IList<int>>();
        private int _k;

        public IList<IList<int>> CombinationSum(int k, int n)
        {
            _k = k;
            Backtrack(n, 1, new List<int>());
            return _result;
        }

        private void Backtrack(int n, int start, List<int> combination)
        {
            if (combination.Count == _k && n == 0)
            {
                _result.Add(new List<int>(combination));
                return;
            }
            else if (combination.Count > _k)
            {
                return; // exceed the scope, stop exploration.
            }

            for (int i = start; i <= 9; i++)
            {
                combination.Add(i);
                Backtrack( n - i, i + 1, combination);
                combination.RemoveAt(combination.Count - 1);
            }
        }
    }


    /*
     Given an array of distinct integers candidates and a target integer target,
     return a list of all unique combinations of candidates where the chosen numbers sum to target. 
    You may return the combinations in any order.

    The same number may be chosen from candidates an unlimited number of times. Two combinations are unique if the 
    frequency of at least one of the chosen numbers is different.

    The test cases are generated such that the number of unique combinations that sum up to target is less than 150 combinations for the given input.
    https://leetcode.com/problems/combination-sum/description/
     */
    class CombinationSumI
    {
        private int[] candidates;
        private List<IList<int>> result = new List<IList<int>>();

        public IList<IList<int>> CombinationSum(int[] candidates, int target)
        {
            this.candidates = candidates;
            Array.Sort(this.candidates);
            Backtrack(target, 0, new List<int>());
            return result;
        }

        private void Backtrack(int target, int start, List<int> combination)
        {
            if (target == 0)
            {
                result.Add(new List<int>(combination));
                return;
            }
           

            for (int i = start; i < candidates.Length; i++)
            {
                if (target < candidates[i]) break; // optimization: early stopping
                
                combination.Add(candidates[i]);
                Backtrack(target - candidates[i], i, combination);
                combination.RemoveAt(combination.Count - 1);
            }
        }
    }

    /*
     * Given a collection of candidate numbers (candidates) and a target number (target), 
     * find all unique combinations in candidates where the candidate numbers sum to target.
        Each number in candidates may only be used once in the combination.
        Note: The solution set must not contain duplicate combinations.
         https://leetcode.com/problems/combination-sum-ii/
     */

    /*
     * There are two differences between this problem and the earlier problem:
        In this problem, each number in the input is not unique. 
            The implication of this difference is that we need some mechanism to avoid generating duplicate combinations.
        In this problem, each number can be used only once. 
            The implication of this difference is that once a number is chosen as a candidate in the combination, it will not appear again as a candidate later.
     */
    public class CombinationSumII
    {
        private List<IList<int>> results = new List<IList<int>>();
        private int[] candidates;

        public IList<IList<int>> CombinationSum(int[] candidates, int target)
        {
            this.candidates = candidates;
            Array.Sort(this.candidates);

            Backtrack(new List<int>(), target, 0);
            return results;
        }

        private void Backtrack(List<int> comb, int remain, int curr)
        {
            if (remain == 0)
            {
                results.Add(new List<int>(comb));
                return;
            }

            for (int nextCurr = curr; nextCurr < candidates.Length; ++nextCurr)
            {
                if (nextCurr > curr && candidates[nextCurr] == candidates[nextCurr - 1]) //avoid the generation of duplicated combinations
                    continue;

                int pick = candidates[nextCurr];
                if (remain - pick < 0) // optimization: early stopping
                    break;

                comb.Add(pick);
                Backtrack(comb, remain - pick, nextCurr + 1);
                comb.RemoveAt(comb.Count - 1);
            }
        }
    }



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

        private void Backtrack(int start, List<int> combination)
        {
            if (combination.Count == k)
            {
                result.Add(new List<int>(combination));
                return;
            }
            for (int i = start; i <= n; i++)
            {
                combination.Add(i);
                Backtrack(i + 1, combination);
                combination.RemoveAt(combination.Count - 1);
            }
        }
    }


}
