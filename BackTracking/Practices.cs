using System.Collections.Generic;

namespace DataStructureRepo.BackTracking
{
    /*
     Find all valid combinations of k numbers that sum up to n such that the following conditions are true:
        Only numbers 1 through 9 are used.
        Each number is used at most once.
    Return a list of all possible valid combinations. 
    The list must not contain the same combination twice, and the combinations may be returned in any order.
    https://leetcode.com/problems/combination-sum-iii/
     */
    class CombinationSumIII
    {
        Stack<int> stack = new Stack<int>();
        List<IList<int>> results = new List<IList<int>>();
        public IList<IList<int>> CombinationSum3(int k, int n)
        {           
            FindSum(1, k, n);
            return results;          
        }
        void FindSum(int startingValue, int numberOfNumbers, int targetSum)
        {
            if (targetSum is 0 && numberOfNumbers is 0) 
                results.Add(stack.ToArray());

            if (targetSum is 0 || numberOfNumbers is 0) 
                return;

            for (int value = startingValue; value < 10; value++)
            {
                stack.Push(value);
                FindSum(value + 1, numberOfNumbers - 1, targetSum - value);
                stack.Pop();
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
/*    class CombinationSumI
    {
        public IList<IList<int>> CombinationSum(int[] candidates, int target)
        {

        }
    }*/
}
