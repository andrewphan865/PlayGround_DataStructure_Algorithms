using System.Collections.Generic;

/*With counting DP, the base cases are often not set to 0.
  * This is because the recurrence relation usually only involves addition terms with other states, 
  * so if the base case was set to 0 then you would only ever add 0 to itself. 
  * Finding these base cases involves some logical thinking - for example, 
  * when we looked at Climbing Stairs - we reasoned that there is 1 way to climb to the first step and 2 ways to climb to the second step.
  */
namespace DynamicProgramming
{
    /* You are painting a fence of n posts with k different colors. You must paint the posts following these rules:
        Every post must be painted exactly one color.
        There cannot be three or more consecutive posts with the same color.
        Given the two integers n and k, return the number of ways you can paint the fence.
    https://leetcode.com/problems/paint-fence/solution/
    */
    class PaintFence_TopDown
    {
        /*For this approach, we are using a hash map as our data structure to memorize function calls.
         * We could also use an array since the calls to totalWays are very well defined (between 1 and n). 
         * However, a hash map is used for most top-down dynamic programming solutions, as there will often be multiple function arguments, 
         * the arguments might not be integers, or a variety of other reasons that require a hash map instead of an array. 
         * Although using an array is slightly more efficient, using a hash map here is a good practice that can be applied to other problems.
         */
        private Dictionary<int, int> memo = new Dictionary<int, int>();
        int k;
        private int TotalWays(int i)
        {
            if (i == 1) return k;
            if (i == 2) return k * k;

            if (!memo.ContainsKey(i))
            {
                // Use the recurrence relation to calculate totalWays(i)
                memo.Add(i, (k - 1) * TotalWays(i - 1) + (k - 1) * TotalWays(i - 2));
            }

            return memo[i];
            /*  Time complexity: O(n)
                Space complexity: O(n)
             */
        }

        public int NumWays(int n, int k)
        {
            this.k = k;
            return TotalWays(n);
        }
    }

    class PaintFence_BottomUp
    {
        public int NumWays(int n, int k)
        {
            // Base cases for the problem to avoid index out of bound issues
            if (n == 1) return k;
            if (n == 2) return k * k;

            int[] totalWays = new int[n + 1];
            totalWays[1] = k;
            totalWays[2] = k * k;

            for (int i = 3; i <= n; i++)
            {
                totalWays[i] = (k - 1) * totalWays[i - 1] + (k - 1) * totalWays[i - 2];
            }

            return totalWays[n];
            /*  Time complexity: O(n)
                Space complexity: O(n)
             */
        }
    }

    class PaintFence_ConstantSpace
    {
        public int numWays(int n, int k)
        {
            if (n == 1) return k;

            int twoPostsBack = k;
            int onePostBack = k * k;

            for (int i = 3; i <= n; i++)
            {
                int curr = (k - 1) * (onePostBack + twoPostsBack);
                twoPostsBack = onePostBack;
                onePostBack = curr;
            }

            return onePostBack;
        }
        /*  Time complexity: O(n)
            Space complexity: O(1)
         */
    }

    /*You are given an integer array coins representing coins of different denominations 
     * and an integer amount representing a total amount of money.
        Return the number of combinations that make up that amount. 
        If that amount of money cannot be made up by any combination of the coins, return 0.
        You may assume that you have an infinite number of each kind of coin
     */
    class CoinChange2_TopDown
    {

    }
    class CoinChange2_BottomUp
    {
        public int Change(int amount, int[] coins)
        {
            int[] dp = new int[amount + 1];
            dp[0] = 1;

            foreach (int coin in coins)
            {
                for (int x = coin; x < amount + 1; ++x)
                {
                    dp[x] += dp[x - coin];
                }
            }
            return dp[amount];
        }
    }

    /*To decode an encoded message, all the digits must be grouped then mapped back into letters using the reverse of the mapping above (there may be multiple ways). 
     * For example, "11106" can be mapped into:

    "AAJF" with the grouping (1 1 10 6)
    "KJF" with the grouping (11 10 6)
    Note that the grouping (1 11 06) is invalid because "06" cannot be mapped into 'F' since "6" is different from "06".

    Given a string s containing only digits, return the number of ways to decode it.
    https://leetcode.com/problems/decode-ways/solution/
     */
    class DecodeWays_TopDown
    {

    }

    class DecodeWays_BottomUp
    {

        public int NumDecodings(string s)
        {
            // DP array to store the subproblem results
            int[] dp = new int[s.Length + 1];
            dp[0] = 1;

            // Ways to decode a string of size 1 is 1. Unless the string is '0'.
            // '0' doesn't have a single digit decode.
            dp[1] = s[0] == '0' ? 0 : 1;

            for (int i = 2; i < dp.Length; i++)
            {
                // Check if successful single digit decode is possible.
                if (s[i - 1] != '0')
                {
                    dp[i] = dp[i - 1];
                }

                // Check if successful two digit decode is possible.
                int twoDigit = int.Parse(s.Substring(i - 2, i));
                if (twoDigit >= 10 && twoDigit <= 26)
                {
                    dp[i] += dp[i - 2];
                }
            }

            return dp[s.Length];
        }
    }

    class DecodeWays_ConstSpace
    {
        public int NumDecodings(string s)
        {
            if (s[0] == '0')
            {
                return 0;
            }

            int n = s.Length;
            int twoBack = 1;
            int oneBack = 1;
            for (int i = 1; i < n; i++)
            {
                int current = 0;
                if (s[i] != '0')
                {
                    current = oneBack;
                }
                int twoDigit = int.Parse(s.Substring(i - 1, i + 1));
                if (twoDigit >= 10 && twoDigit <= 26)
                {
                    current += twoBack;
                }

                twoBack = oneBack;
                oneBack = current;
            }
            return oneBack;
        }
    }


















}
