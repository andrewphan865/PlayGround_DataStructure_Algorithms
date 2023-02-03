using System;
using System.Collections.Generic;
using System.Text;

/*
 In all the problems we have looked at so far, the recurrence relation is a static equation - it never changes. 
Recall Min Cost Climbing Stairs. The recurrence relation was:

dp(i)=min(dp(i - 1) + cost[i - 1], dp(i - 2) + cost[i - 2])

because we are only allowed to climb 1 or 2 steps at a time. What if the question was rephrased so that we could take up to k steps at a time?
The recurrence relation would become dynamic - it would be:

dp(i)=min(dp(j) + cost[j]) for all (i - k)≤j<i

We would need iteration in our recurrence relation.

This is a common pattern in DP problems, and in this chapter, we're going to take a look at some problems using the framework where this pattern is applicable.
While iteration usually increases the difficulty of a DP problem, particularly with bottom-up implementations, the idea isn't too complicated.
Instead of choosing from a static number of options, we usually add a for-loop to iterate through a dynamic number of options and choose the best one.
 
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
    /*   You are given an integer array coins representing coins of different denominations 
   *   and an integer amount representing a total amount of money.
         Return the fewest number of coins that you need to make up that amount.
          If that amount of money cannot be made up by any combination of the coins, return -1.

         You may assume that you have an infinite number of each kind of coin.
  https://leetcode.com/problems/coin-change/solution/
  */

    /*
    1. A function or data structure that will compute/contain the answer to the problem for every given state.  dp(i)
        a) What state variables we need to pass to it?           

        b)  what it will return?          

    2. A recurrence relation to transition between states  dp(i) = dp(i - 1) + dp(i - 2)
            
    3. Base cases                                          dp(1) = 1 and dp(2) = 2       
 */
    class CoinChange_TopDown
    {
        int[] coins;
        public int CoinChange(int[] coins, int amount)
        {
            if (amount < 1) return 0;
            this.coins = coins;
            return CoinChange(amount, new int[amount]);
        }

        private int CoinChange(int remain, int[] count)
        {
            if (remain < 0) return -1;
            if (remain == 0) return 0;

            if (count[remain - 1] != 0) return count[remain - 1];

            int min = Int32.MaxValue;
            foreach (var coin in coins)
            {
                int res = CoinChange(remain - coin, count);
                if (res >= 0 && res < min)
                    min = 1 + res;
            }
            count[remain - 1] = (min == Int32.MaxValue) ? -1 : min;
            return count[remain - 1];
        }
    }

    class CoinChange_BottomUp
    {
        public int CoinChange(int[] coins, int amount)
        {
            int max = amount + 1;
            int[] dp = new int[amount + 1];
            Array.Fill(dp, max);
            dp[0] = 0;
            for (int i = 1; i <= amount; i++)
            {
                for (int j = 0; j < coins.Length; j++)
                {
                    if (coins[j] <= i)
                    {
                        dp[i] = Math.Min(dp[i], dp[i - coins[j]] + 1);
                    }
                }
            }
            return dp[amount] > amount ? -1 : dp[amount];
        }
    }


    /// <summary>
    /// You want to schedule a list of jobs in d days.
    ///  Jobs are dependent (i.e To work on the ith job, you have to finish all the jobs j where 0 <= j < i).
    ///  You have to finish at least one task every day.The difficulty of a job schedule is the sum of difficulties of each day of the d days.
    ///  The difficulty of a day is the maximum difficulty of a job done on that day.
    ///  You are given an integer array jobDifficulty and an integer d.The difficulty of the ith job is jobDifficulty[i].
    ///  Return the minimum difficulty of a job schedule. If you cannot find a schedule for the jobs return -1.
    /// https://leetcode.com/problems/minimum-difficulty-of-a-job-schedule/
    /// </summary>
    /// 
        /*
      1. A function or data structure that will compute/contain the answer to the problem for every given state.  dp(i)
        a) What state variables we need to pass to it?
            Let's first decide on state variables. What decisions are there to make, and what information do we need to make these decisions? 
            Reading the problem description carefully, there are d total days, and on each day we need to complete some number of jobs. By the end of the d days, 
            we must have finished all jobs (in the given order). Therefore, we can see that on each day, we need to decide how many jobs to take.

                Let's use one state variable i, where i is the index of the first job that will be done on the current day.
                Let's use another state variable day, where day indicates what day it currently is.

        b)  what it will return.?
            The problem is asking for the minimum difficulty, so let's have a function dp(i, day) that returns the minimum difficulty of a job schedule
            which starts on the i^th job and day.
            To solve the original problem, we will just return dp(0, 1), since we start on the first day with no jobs done yet.


    2. A recurrence relation to transition between states  dp(i) = dp(i - 1) + dp(i - 2)
        At each state, we are on day day and need to do job i. Then, we can choose to do a few more jobs. 
        How many more jobs are we allowed to do? 
        The problem says that we need to do at least one job per day. 
        This means we must leave at least d - day jobs so that all the future days have at least one job that can be scheduled on that day. 
        If n is the total number of jobs, jobDifficulty * length, that means from any given state (i, day),
        we are allowed to do the jobs from index i up to but not including index n - (d - day).

        We should try all the options for a given day - try doing only one job, then two jobs, etc. until we can't do any more jobs.
        The best option is the one that results in the easiest job schedule.

        The difficulty of a given day is the most difficult job that we did that day. Since the jobs have to be done in order, 
        if we are trying all the jobs we are allowed to do on that day (iterating through them), 
        then we can use a variable hardest to keep track of the difficulty of the hardest job done today. 
        If we choose to do jobs up to the j^th job (inclusive), where i≤j<n - (d - day) (as derived above),
        then that means on the next day, we start with the (j+1)^th job. 
        Therefore, our total difficulty is hardest + dp(j + 1, day + 1). This gives us our scariest recurrence relation so far:

        dp(i, day) = min(hardest + dp(j + 1, day + 1)) for all i≤j<n - (d - day), where
        hardest = max(jobDifficulty[k]) for all i≤ k ≤j.

        The codified recurrence relation is a scary one to look at for sure. However, it is easier to understand when we break it down bit by bit.
        On each day, we try all the options - do only one job, then two jobs, etc. until we can't do any more (since we need to leave some jobs for future days).
        hardest is the hardest job we do on the current day, which means it is also the difficulty of the current day. 
        We add hardest to the next state which is the next day, starting with the next job. 
        After trying all the jobs we are allowed to do, choose the best result.
    
    3. Base cases                                          dp(1) = 1 and dp(2) = 2
       
        Despite the recurrence relation being complicated, the base cases are much simpler.
        We need to finish all jobs in d days. Therefore, if it is the last day day == d, we need to finish up all the remaining jobs on this day,
        and the total difficulty will just be the largest number in jobDifficulty on or after index i.

        if day == d then return the maximum job difficulty between job i and the end of the array (inclusive).
        We can precompute an array hardestJobRemaining where hardestJobRemaining[i] represents the difficulty of the hardest job on or after day i, 
        so that this base case is handled in constant time.

         Additionally, if there are more days than jobs (n < d), then it is impossible to do at least one job each day, and per the problem description, we should return -1. 
        We can check for this case at the very start.
     */
    class MinimumDifficultyofJobSchedule_TopDown
    {
        private int n, d;
        private int[,] memo;
        private int[] jobDifficulty;
        private int[] hardestJobRemaining;


        public int MinDifficulty(int[] jobDifficulty, int d)
        {
            n = jobDifficulty.Length;
            // If we cannot schedule at least one job per day, 
            // it is impossible to create a schedule
            if (n < d) return -1;

            hardestJobRemaining = new int[n];
            int hardestJob = 0;
            for (int i = n - 1; i >= 0; i--)
            {
                hardestJob = Math.Max(hardestJob, jobDifficulty[i]);
                hardestJobRemaining[i] = hardestJob;
            }

            // Initialize memo array with value of -1.
            memo = new int[n, d + 1];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < d + 1; j++)
                {
                    memo[i, j] = -1;
                }
            }

            this.d = d;
            this.jobDifficulty = jobDifficulty;
            return dp(0, 1);
        }

        private int dp(int i, int day)
        {
            // Base case, it's the last day so we need to finish all the jobs
            if (day == d)
            {
                return hardestJobRemaining[i];
            }

            if (memo[i, day] == -1)
            {
                int best = Int32.MaxValue;
                int hardest = 0;
                // Iterate through the options and choose the best
                for (int j = i; j < n - (d - day); j++)
                {
                    hardest = Math.Max(hardest, jobDifficulty[j]);
                    // Recurrence relation
                    best = Math.Min(best, hardest + dp(j + 1, day + 1));
                }
                memo[i, day] = best;
            }

            return memo[i, day];
        }

    }

    class MinimumDifficultyofJobSchedule_BottomUp
    {
        public int MinDifficulty(int[] jobDifficulty, int d)
        {
            int n = jobDifficulty.Length;
            // If we cannot schedule at least one job per day, 
            // it is impossible to create a schedule
            if (n < d) return -1;


            int[,] dp = new int[n, d + 1];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < d + 1; j++)
                {
                    dp[i, j] = Int32.MaxValue;
                }
            }

            // Set base cases
            dp[n - 1, d] = jobDifficulty[n - 1];

            // On the last day, we must schedule all remaining jobs, so dp[i][d]
            // is the maximum difficulty job remaining
            for (int i = n - 2; i >= 0; i--)
            {
                dp[i, d] = Math.Max(dp[i + 1, d], jobDifficulty[i]);
            }

            for (int day = d - 1; day > 0; day--)
            {
                for (int i = day - 1; i < n - (d - day); i++)
                {
                    int hardest = 0;
                    // Iterate through the options and choose the best
                    for (int j = i; j < n - (d - day); j++)
                    {
                        hardest = Math.Max(hardest, jobDifficulty[j]);
                        // Recurrence relation
                        dp[i, day] = Math.Min(dp[i, day], hardest + dp[j + 1, day + 1]);
                    }
                }
            }

            return dp[0, 1];
        }
    }

  
    /*
     * Given a string s and a dictionary of strings wordDict,
     * return true if s can be segmented into a space-separated sequence of one or more dictionary words.
        Note that the same word in the dictionary may be reused multiple times in the segmentation.
        https://leetcode.com/problems/word-break/
     */
    class WordBreak_BottomUp
    {
        public bool WordBreak(string s, IList<string> wordDict)
        {
            bool[] dp = new bool[s.Length];
            for (int i = 0; i < s.Length; i++)
            {
                foreach (var word in wordDict)
                {
                    // Make sure to stay in bounds while checking criteria
                    if (i >= word.Length - 1 && (i == word.Length - 1 || dp[i - word.Length]))
                    {
                        if (s.Substring(i - word.Length + 1, i + 1).Equals(word))
                        {
                            dp[i] = true;
                            break;
                        }
                    }
                }
            }

            return dp[s.Length - 1];
        }
    }
    class WordBreak_TopDown
    {
        private string s;
        private List<string> wordDict;
        private int[] memo;

        public bool WordBreak(string s, List<string> wordDict)
        {
            this.s = s;
            this.wordDict = wordDict;
            this.memo = new int[s.Length];
            Array.Fill(this.memo, -1);
            return dp(s.Length - 1);
        }
        private bool dp(int i)
        {
            if (i < 0) return true;

            if (memo[i] == -1)
            {
                foreach (var word in wordDict)
                {
                    if (i >= word.Length - 1 && dp(i - word.Length))
                    {
                        if (s.Substring(i - word.Length + 1, i + 1).Equals(word))
                        {
                            memo[i] = 1;
                            break;
                        }
                    }
                }
            }

            if (memo[i] == -1) memo[i] = 0;
          
            return memo[i] == 1;
        }       
    }
    /*
     * Given an integer array nums, return the length of the longest strictly increasing subsequence.
        A subsequence is a sequence that can be derived from an array by deleting some or no elements without changing the order of the remaining elements.
        For example, [3,6,2,7] is a subsequence of the array [0,3,1,6,2,2,7]
    https://leetcode.com/problems/longest-increasing-subsequence/
     */
    class LongestIncreasingSubsequence
    {
        public int LengthOfLIS(int[] nums)
        {
            int[] dp = new int[nums.Length];
            Array.Fill(dp, 1);

            for (int i = 1; i < nums.Length; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (nums[i] > nums[j]) // for each element, check all the elements that come before it
                    {
                        dp[i] = Math.Max(dp[i], dp[j] + 1);
                    }
                }
            }

            int longest = 0;
            foreach (var  c in dp)
            {
                longest = Math.Max(longest, c);
            }

            return longest;
        }
    }


}
