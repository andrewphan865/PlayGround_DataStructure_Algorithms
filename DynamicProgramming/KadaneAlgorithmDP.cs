using System;


namespace DynamicProgramming
{
    /*
     * Kadane's Algorithm is an algorithm that can find the maximum sum subarray given an array of numbers in O(n) time andO(1) space. 
     * Its implementation is a very simple example of dynamic programming, and the efficiency of the algorithm allows it to be a powerful tool in some DP algorithms. 
     * If you haven't already solved Maximum Subarray, take a quick look at the problem before continuing with this article
     * - Kadane's Algorithm specifically solves this problem.
     * 
     * Kadane's Algorithm involves iterating through the array using an integer variable current, and at each index i, 
     * determines if elements before index i are "worth" keeping, or if they should be "discarded". 
     * The algorithm is only useful when the array can contain negative numbers.
     * If current becomes negative, it is reset, and we start considering a new subarray starting at the current index.
     */


    /*Given an integer array nums, find the contiguous subarray (containing at least one number) 
     * which has the largest sum and return its sum.

    A subarray is a contiguous part of an array.
    https://leetcode.com/problems/maximum-subarray/
    */

    /*
     * The difficult part of this problem is figuring out when a negative number is "worth" keeping in a subarray. 
     * This question in particular is a popular problem that can be solved using an algorithm called Kadane's Algorithm.
     */
    class MaximumSubarray_BottomUp
    {
        public int MaxSubArray(int[] nums)
        {
            // Initialize our variables using the first element.
            int currentSubarray = nums[0];
            int maxSubarray = nums[0];

            // Start with the 2nd element since we already used the first one.
            for (int i = 1; i < nums.Length; i++)
            {                
                // If current_subarray is negative, throw it away. Otherwise, keep adding to it.
                currentSubarray = Math.Max(nums[i], currentSubarray + nums[i]);
                maxSubarray = Math.Max(maxSubarray, currentSubarray);
            }

            return maxSubarray;
        }

        /*
         Time complexity: O(N), where NN is the length of nums.
            We iterate through every element of nums exactly once.
        Space complexity: O(1)
            No matter how long the input is, we are only ever using 2 variables: currentSubarray and maxSubarray.
         */
    }

    /*https://leetcode.com/problems/best-time-to-buy-and-sell-stock/solution/
     */
    class BestTImeToBuyAndSellStock_BottomUp
    {
        public int MaxProfit(int[] prices)
        {
            int maxProfit = 0;
            int i = 0; // to track min price index

            for (int j = 0; j < prices.Length; j++)
            {
                if (prices[i] >= prices[j])
                {
                    i = j;
                }
                else
                {
                    var profit = prices[j] - prices[i];
                    maxProfit = Math.Max(maxProfit, profit);
                }
            }

            return maxProfit;
        }
    }
}

