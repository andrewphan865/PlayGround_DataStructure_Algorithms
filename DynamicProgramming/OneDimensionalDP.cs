using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// When to Use DP:
/// 1. The problem can be broken down into "overlapping subproblems" - smaller versions of the original problem that are re-used multiple times
/// 2. The problem has an "optimal substructure" - an optimal solution can be formed from optimal solutions to the overlapping subproblems of the original problem            
/// 
/// - The first characteristic that is common in DP problems is that the problem will ask for the optimum value
/// (max /min / longest, counting DP :how many ways,etc, Is it possible to reach a certain point ) of something
/// - The second characteristic that is common in DP problems is that future "decisions" depend on earlier decisions.
/// </summary>
/// 
/// <summary>
/// To solve a DP problem, we need to combine 3 things:
/// 1. A function or data structure that will compute/contain the answer to the problem for every given state.  dp(i)
/// 2. A recurrence relation to transition between states.                                                      dp(i) = dp(i - 1) + dp(i - 2)
/// 3. Base cases, so that our recurrence relation doesn't go on infinitely.                                    dp(1) = 1 and dp(2) = 2
/// </summary>
/*
 *  A bottom-up implementation's runtime is usually faster, as iteration does not have the overhead that recursion does.
    A top-down implementation is usually much easier to write. 
    This is because with recursion, the ordering of subproblems does not matter, whereas with tabulation,
    we need to go through a logical ordering of solving subproblems.
*/

/*    
    1. A function or data structure that will compute/contain the answer to the problem for every given state.  dp(i)
        a) What state variables we need to pass to it?           

        b)  what it will return?          

    2. A recurrence relation to transition between states  dp(i) = dp(i - 1) + dp(i - 2)
            
    3. Base cases                                          dp(1) = 1 and dp(2) = 2       
 */

namespace DynamicProgramming
{
    /// <summary>
    /// You are a professional robber planning to rob houses along a street. Each house has a certain amount of money stashed, 
    /// the only constraint stopping you from robbing each of them is that adjacent houses have security systems connected 
    /// and it will automatically contact the police if two adjacent houses were broken into on the same night.
    /// 
    /// Given an integer array nums representing the amount of money of each house,
    /// return the maximum amount of money you can rob tonight without alerting the police.
    /// https://leetcode.com/problems/house-robber/
    /// </summary>

    class HouseRobber_TopDown
    {
        //time complexity  O(n)
        private Dictionary<int, int> memo = new Dictionary<int, int>();
        private int[] nums;
        public int Rob(int[] nums)
        {
            this.nums = nums;
            return dp(nums.Length - 1);

        }

        // A function that represents the answer to the problem for a given state
        private int dp(int i)
        {
            // Base cases
            if (i == 0) return nums[0];
            if (i == 1) return Math.Max(nums[0], nums[1]);

            if (!memo.ContainsKey(i))
            {
                memo.Add(i, Math.Max(dp(i - 1), dp(i - 2) + nums[i])); // Recurrence relation
            }

            return memo[i];
        }

    }

    class HouseRobber_BottomUp_Tabulation
    {
        public int Rob(int[] nums)
        {
            if (nums.Length == 1) return nums[0];

            int[] dp = new int[nums.Length];    // An array that represents the answer to the problem for a given state
            dp[0] = nums[0];                    // Base cases
            dp[1] = Math.Max(nums[0], nums[1]); // Base cases

            for (int i = 2; i < nums.Length; i++)
            {
                dp[i] = Math.Max(dp[i - 1], dp[i - 2] + nums[i]); // Recurrence relation
            }

            return dp[nums.Length - 1];
        }

        /*
            * Complexity Analysis

             Time Complexity: O(N) since we have a loop from N - 2 ...0 and we simply use the pre-calculated values of our dynamic programming table for 
                 calculating the current value in the table which is a constant time operation.

             Space Complexity: O(N) which is used by the table. 
        */
    }


    class HouseRobber_BottomUp_StateReduction
    {
        public int Rob(int[] nums)
        {            
            // Special handling for empty array case.
            if (nums.Length == 1) return nums[0];

            // Base cases
            int first = nums[0];
            int second = Math.Max(nums[0], nums[1]);

            for (int i = 2; i < nums.Length; i++)
            {
                int third = Math.Max(second, first + nums[i]);
                first = second;
                second = third;
            }

            return second;
        }

        /*
         * Time Complexity

            Time Complexity: O(N) since we have a loop from N - 2 ...0 and we use the precalculated values of
            our dynamic programming table to calculate the current value in the table which is a constant time operation.

            Space Complexity: O(1) since we are not using a table to store our values. Simply using two variables will suffice for our calculations.
         */
    }

    /// <summary>
    /// You are climbing a staircase. It takes n steps to reach the top. Each time you can either climb 1 or 2 steps.
    /// In how many distinct ways can you climb to the top?
    /// https://leetcode.com/problems/climbing-stairs/
    /// </summary>
    class ClimbingStairs_TopDown
    {
        // A top-down implementation is usually much easier to write. 
        // This is because with recursion, the ordering of subproblems does not matter,
        // whereas with tabulation, we need to go through a logical ordering of solving subproblems.
        // time complexity  O(n)
        private Dictionary<int, int> memo = new Dictionary<int, int>();
        public int ClimbStairs(int n)
        {
            return dp(n);
        }

        // A function that represents the answer to the problem for a given state
        private int dp(int i)
        {
            if (i <= 2) return i; // Base cases

            if (!memo.ContainsKey(i))
            {
                memo.Add(i, dp(i - 1) + dp(i - 2)); // Recursion with Memoization
            }

            return memo[i];
        }
    }

    class ClimbingStairs_BottomUp
    {
        //A bottom-up implementation's runtime is usually faster, as iteration does not have the overhead that recursion does.
        public int ClimbStairs(int n)
        {
            if (n == 1) return 1;

            int[] dp = new int[n + 1]; // An array that represents the answer to the problem for a given state
            dp[1] = 1; // Base cases
            dp[2] = 2; // Base cases

            for (int i = 3; i <= n; i++)
            {
                dp[i] = dp[i - 1] + dp[i - 2]; // Recurrence relation
            }

            return dp[n];
        }
    }

    class ClimbingStairs_BottomUp_StateReduction
    {
        // const space
        public int ClimbStairs(int n)
        {
            if (n == 1) return 1;

            // Base cases
            int first = 1;
            int second = 2;

            for (int i = 3; i <= n; i++)
            {
                int third = first + second;
                first = second;
                second = third; // Recurrence relation
            }

            return second;
        }
    }


    /// <summary>
    /// You are given an integer array cost where cost[i] is the cost of ith step on a staircase.
    /// Once you pay the cost, you can either climb one or two steps.
    /// You can either start from the step with index 0, or the step with index 1.
    /// Return the minimum cost to reach the top of the floor.
    /// https://leetcode.com/problems/min-cost-climbing-stairs/
    /// </summary>

    /*Note: The "top of the floor" does not refer to the final index of costs. We actually need to "arrive" beyond the array's bounds.
        */
    class MinCostClimbingStairs_TopDown
    {
        private Dictionary<int, int> memo = new Dictionary<int, int>();
        private int[] cost;
        public int MinCostClimbingStairs(int[] cost)
        {
            this.cost = cost;
            return minimumCost(cost.Length);
        }

        // A function that represents the answer to the problem for a given state
        private int minimumCost(int i)
        {
            if (i <= 1) return 0; // Base cases,  we are allowed to start at either step 0 or step 1

            if (!memo.ContainsKey(i))
            {
                int downOne = cost[i - 1] + minimumCost(i - 1);
                int downTwo = cost[i - 2] + minimumCost(i - 2);
                memo.Add(i, Math.Min(downOne, downTwo)); // Recursion with Memoization
            }

            return memo[i];
        }
        /*Complexity Analysis

            Time complexity: O(N)
                    minimumCost gets called with each index from 0 to N. Because of our memoization, each call will only take O(1) time.

            Space complexity: O(N)
                The extra space used by this algorithm is the recursion call stack. For example, minimumCost(10000) will call minimumCost(9999),
                which calls minimumCost(9998) etc., all the way down until the base cases at minimumCost(0) and minimumCost(1).
                In addition, our hash map memo will be of size N at the end, since we populate it with every index from 0 to N.
         */
    }

    class MinCostClimbingStairs_BottomUp
    {
        public int MinCostClimbingStairs(int[] cost)
        {
            if (cost.Length < 1) return 0;

            // The array's length should be 1 longer than the length of cost
            // This is because we can treat the "top floor" as a step to reach
            int[] minimumCost = new int[cost.Length + 1];    // An array that represents the answer to the problem for a given state
            minimumCost[0] = 0;                          // Base cases
            minimumCost[1] = 0;                          // Base cases

            for (int i = 2; i < cost.Length; i++)
            {
                int takeOneStep = cost[i - 1] + minimumCost[i - 1];
                int takeTwoSteps = cost[i - 2] + minimumCost[i - 2];
                minimumCost[i] = Math.Min(takeOneStep, takeTwoSteps); // Recursion with Memoization
            }

            return minimumCost[cost.Length - 1]; // so the last step which has the minimum Cost should be minimumCost - 2 => cost.Length -1

            /*
             * Complexity Analysis           

                Time complexity: O(N).
                    We iterate N - 1 times, and at each iteration we apply an equation that requires O(1) time.

                Space complexity: O(N).
                    The array minimumCost is always 1 element longer than the array cost.
             */
        }
    }
    class MinCostClimbingStairs_BottomUp_StateReduction
    {
        public int MinCostClimbingStairs(int[] cost)
        {

            int downOne = 0;                          // Base cases
            int downTwo = 0;                          // Base cases

            for (int i = 2; i < cost.Length + 1; i++)
            {
                int currentStep = Math.Min(downOne + cost[i - 1], downTwo + cost[i - 2]);
                downTwo = downOne;
                downOne = currentStep;
            }

            return downOne;
        }
        /*
         * Complexity Analysis

            Time complexity: O(N)
                We only iterate N - 1 times, and at each iteration we apply an equation that uses O(1) time.

            Space complexity: O(1)
                The only extra space we use is 2 variables, which are independent of input size.
         */
    }

    /*
     * The Tribonacci sequence Tn is defined as follows: 
        T0 = 0, T1 = 1, T2 = 1, and Tn+3 = Tn + Tn+1 + Tn+2 for n >= 0.
        Given n, return the value of Tn.
        https://leetcode.com/problems/n-th-tribonacci-number/
     */

    class NthTribonacciNumber_TopDown
    {
        private Dictionary<int, int> memo = new Dictionary<int, int>();

        public int Tribonacci(int n)
        {
            return dp(n);
        }

        private int dp(int i)
        {
            if (i < 3) return i == 0 ? 0 : 1;

            if (!memo.ContainsKey(i))
            {
                memo.Add(i, dp(i - 3) + dp(i - 2) + dp(i - 1));
            }

            return memo[i];
        }
    }

    class NthTribonacciNumber_BottomUp
    {
        public int Tribonacci(int n)
        {
            if (n < 3) return n == 0 ? 0 : 1;

            int[] values = new int[n + 1];    // An array that represents the answer to the problem for a given state
            values[0] = 0;                          // Base cases
            values[1] = 1;                          // Base cases
            values[2] = 1;

            for (int i = 3; i < n + 1; i++)
            {
                values[i] = values[i - 3] + values[i - 2] + values[i - 1];
            }

            return values[n];
        }
    }

    class NthTribonacciNumber_BottomUp_StateReduction
    {
        public int Tribonacci(int n)
        {
            if (n < 3) return n == 0 ? 0 : 1;

            int downOne = 0;                          // Base cases
            int downTwo = 1;                          // Base cases
            int downThree = 1;

            for (int i = 3; i < n + 1; i++)
            {
                int currentValue = downOne + downTwo + downThree;
                downThree = downTwo;
                downTwo = downOne;
                downOne = currentValue;
            }

            return downOne;
        }
    }



    /// <summary>
    /// You are given an integer array nums. You want to maximize the number of points you get by performing the following operation any number of times:
    /// Pick any nums[i] and delete it to earn nums[i] points.Afterwards, you must delete every element equal to nums[i] - 1 and every element equal to nums[i] + 1.
    /// Return the maximum number of points you can earn by applying the above operation some number of times.
    /// https://leetcode.com/problems/delete-and-earn/
    /// </summary>
    class DeleteAndEarn_TopDown
    {
        private Dictionary<int, int> points = new Dictionary<int, int>();
        private Dictionary<int, int> cache = new Dictionary<int, int>();


        public int DeleteAndEarn(int[] nums)
        {
            int maxNumber = 0;

            // Precompute how many points we gain from taking an element
            foreach (var num in nums)
            {
                points.Add(num, num + (points.TryGetValue(num, out var value) ? value : 0));

                maxNumber = Math.Max(maxNumber, num);
            }

            return MaxPoints(maxNumber);
        }
        private int MaxPoints(int num)
        {
            // Check for base cases
            if (num == 0) return 0;

            if (num == 1) return points.TryGetValue(num, out var value) ? value : 0;


            if (!cache.ContainsKey(num))
            {
                // Apply recurrence relation
                int gain = points.TryGetValue(num, out var value) ? value : 0;
                cache.Add(num, Math.Max(MaxPoints(num - 1), MaxPoints(num - 2) + gain));
            }


            return cache[num];
        }
    }

    class DeleteAndEarn_BottomUp
    {
        public int DeleteAndEarn(int[] nums)
        { 
             Dictionary<int, int> points = new Dictionary<int, int>();
            int maxNumber = 0;

            // Precompute how many points we gain from taking an element
            foreach (var num in nums)
            {
                points.Add(num, num + (points.TryGetValue(num, out var value1) ? value1 : 0));

                maxNumber = Math.Max(maxNumber, num);
            }

            // Declare our array along with base cases
            int[] maxPoints = new int[maxNumber + 1];
            maxPoints[1] = points.TryGetValue(1, out var value2) ? value2 : 0;

            for (int num = 2; num < maxPoints.Length; num++)
            {
                // Apply recurrence relation
                int gain = points.TryGetValue(num, out var value3) ? value3 : 0;
                maxPoints[num] = Math.Max(maxPoints[num - 1], maxPoints[num - 2] + gain);
            }

            return maxPoints[maxNumber];
        }
    }
    class DeleteAndEarn_BottomUp_StateReduction
    {
        public int DeleteAndEarn(int[] nums)
        {
            int maxNumber = 0;
            Dictionary<int, int> points = new Dictionary<int, int>();

            // Precompute how many points we gain from taking an element
            foreach (var num in nums)
            {
                points.Add(num, num + (points.TryGetValue(num, out var value1) ? value1 : 0));
                maxNumber = Math.Max(maxNumber, num);
            }

            // Base cases
            int twoBack = 0;
            int oneBack = points.TryGetValue(1, out var value2) ? value2 : 0;

            for (int num = 2; num <= maxNumber; num++)
            {
                int temp = oneBack;
                oneBack = Math.Max(oneBack, twoBack + (points.TryGetValue(num, out var value3) ? value3 : 0));
                twoBack = temp;
            }

            return oneBack;
        }
    }





}
