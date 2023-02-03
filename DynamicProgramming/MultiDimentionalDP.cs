using System;

/*
 * Typically, the more dimensions a DP problem has, the more difficult it is to solve. 
 * Two-dimensional problems are common, and sometimes a problem might even require five dimensions. 
 * The good news is, the framework works regardless of the number of dimensions
 * 
 The following are common things to look out for in DP problems that require a state variable:

- An index along some input. This is usually used if an input is given as an array or string. 
    This has been the sole state variable for all the problems that we've looked at so far, 
    and it has represented the answer to the problem if the input was considered only up to that index - 
    for example, if the input is nums = [0, 1, 2, 3, 4, 5, 6], then dp(4) would represent the answer to the problem for the input nums = [0, 1, 2, 3, 4].

- A second index along some input. Sometimes, you need two index state variables, say i and j. 
    In some questions, these variables represent the answer to the original problem if you considered the input starting at index i and ending at index j. 
    Using the same example above, dp(1, 3) would solve the problem for the input nums = [1, 2, 3], if the original input was [0, 1, 2, 3, 4, 5, 6].

- Explicit numerical constraints given in the problem. 
    For example, "you are only allowed to complete k transactions", or "you are allowed to break up to k obstacles", etc.

- Variables that describe statuses in a given state.
    For example "true if currently holding a key, false if not", "currently holding k packages" etc.
    Some sort of data like a tuple or bitmask used to indicate things being "visited" or "used". For example, "bitmask is a mask where 
    the i^ith bit indicates if the i^ith city has been visited".
    Note that mutable data structures like arrays cannot be used - typically, 
    only immutable data structures like numbers and strings can be hashed, and therefore memoized.
 */



/// <summary>
/// When to Use DP:
/// 1. The problem can be broken down into "overlapping subproblems" - smaller versions of the original problem that are re-used multiple times
/// 2. The problem has an "optimal substructure" - an optimal solution can be formed from optimal solutions to the overlapping subproblems of the original problem            
/// 
/// - The first characteristic that is common in DP problems is that the problem will ask for the optimum value (max /min / longest, how many ways,et ) of something
/// - The second characteristic that is common in DP problems is that future "decisions" depend on earlier decisions.
/// </summary>
/// 
/// <summary>
/// To solve a DP problem, we need to combine 3 things:
/// 1. A function or data structure that will compute/contain the answer to the problem for every given state.  dp(i)
///     - What state variables we need to pass to it, and what it will return
/// 2. A recurrence relation to transition between states.                                                      dp(i) = dp(i - 1) + dp(i - 2)
/// 3. Base cases, so that our recurrence relation doesn't go on infinitely.                                    dp(1) = 1 and dp(2) = 2
/// </summary>
namespace DynamicProgramming
{
    /// <summary>
    /// You are given two 0-indexed integer arrays nums and multipliers of size n and m respectively, where n >= m.
    /// You begin with a score of 0. You want to perform exactly m operations.On the ith operation(0-indexed), you will:
    ///     Choose one integer x from either the start or the end of the array nums.
    ///     Add multipliers[i] * x to your score.
    ///         Note that multipliers[0] corresponds to the first operation, multipliers[1] to the second operation, and so on.
    ///     Remove x from the array nums.
    /// Return the maximum score after performing m operations.
    /// https://leetcode.com/problems/maximum-score-from-performing-multiplication-operations/
    /// </summary>
    /// 

    /*
      1. A function or data structure that will compute/contain the answer to the problem for every given state.  dp(i)
        Since we're doing top-down, we need to decide on two things for our function dp.
        a)What state variables we need to pass to it?
        b) and what it will return.?

        a) What state variables we need to pass to it?

        We are given two input arrays: nums and multipliers. The problem says we need to do m operations, and on the i^i th operation,
        we gain score equal to multipliers[i] times a number from either the left or right end of nums, which we remove after the operation. 
        That means we need to know 3 things for each operation:
            - How many operations have we done so far; this tells us what number from multipliers we will be using?
            - The index of the leftmost number remaining in nums.
            - The index of the rightmost number remaining in nums.
        We can use one state variable, i, to indicate how many operations we have done so far, 
        which means multipliers[i] is the current multiplier to be used. 
        For the leftmost number remaining in nums, we can use another state variable, left, that indicates how many left operations we have done so far.
        If we have done, say 3 left operations, if we were to do another left operation we would use nums[3].
        We can say the same thing for the rightmost remaining number - let's use a state variable right that indicates how many right operations we have done so far.
        
        It may seem like we need all 3 of these state variables, but we can formulate an equation for one of them using the other two.
        If we know how many elements we have picked from the leftside, left, and we know how many elements we have picked in total, i, 
        then we know that we must have picked i - left elements from the rightside. The original length of nums is n, 
        which means the index of the rightmost element is right = n - 1 - (i - left). 
        Therefore, we only need 2 state variables: i and left, and we can calculate right inside the function. 

        b)  what it will return.?
        Now that we have our state variables, what should our function return? 
        The problem is asking for the maximum score from some number of operations, so let's have our function dp(i, left) 
        return the maximum possible score if we have already done i total operations and used left numbers from the left side.
        To answer the original problem, we should return dp(0, 0).

    2. A recurrence relation to transition between states  dp(i) = dp(i - 1) + dp(i - 2)
        At each state, we have to perform an operation. As stated in the problem description, we need to decide whether to take from the left end (nums[left]) or the right end (nums[right]) of the current nums. 
        Then we need to multiply the number we choose by multipliers[i], add this value to our score, and finally remove the number we chose from nums.
        For implementation purposes, "removing" a number from nums means incrementing our state variables i and left so that they point to the next two left and right numbers.

        Let mult=multipliers[i] and right = nums.length - 1 - (i - left). The only decision we have to make is whether to take from the left or right of nums.

            If we choose left, we gain mult * nums[left] points from this operation.
            Then, the next operation will occur at (i + 1, left + 1). i gets incremented at every operation because it represents how many operations we have done,
            and left gets incremented because it represents how many left operations we have done. Therefore, our total score is mult * nums[left] + dp(i + 1, left + 1).
            
            If we choose right, we gain mult * nums[right] points from this operation. 
            Then, the next operation will occur at (i + 1, left). Therefore, our total score is mult * nums[right] + dp(i + 1, left).

        Since we want to maximize our score, we should choose the side that gives more points. This gives us our recurrence relation:
        dp(i, left)=max(mult * nums[left] + dp(i + 1, left + 1), mult * nums[right] + dp(i + 1, left))

        Where mult * nums[left]+dp(i + 1, left + 1) represents the points we gain by taking from the left end of nums plus the maximum points
        we can get from the remaining nums array and mult * nums[right]+dp(i + 1, left) represents the points we gain by taking from 
        the right end of nums plus the maximum points we can get from the remaining nums array.
    
    3. Base cases                                          dp(1) = 1 and dp(2) = 2
        The problem statement says that we need to perform m operations. 
        When i equals m, that means we have no operations left. Therefore, we should return 0.
     */

    class MaximumScoreFromPerformingMultiplicationOperations_TopDown
    {
        // The time and space complexity of the implementations is O(m^2)
 
        private int[,] memo; // Due to tight time limits, 2D array will be used instead of Dictionary
        private int[] nums, multipliers;
        private int n, m;

        private int dp(int i, int left)
        {
            if (i == m) return 0; // Base case
           
            int mult = multipliers[i];
            int right = n - 1 - (i - left);

            if (memo[i,left] == 0)
            {
                // Recurrence relation
                memo[i,left] = Math.Max(mult * nums[left] + dp(i + 1, left + 1),
                                         mult * nums[right] + dp(i + 1, left));
            }

            return memo[i,left];
        }

        public int MaximumScore(int[] nums, int[] multipliers)
        {
            n = nums.Length;
            m = multipliers.Length;
            this.nums = nums;
            this.multipliers = multipliers;
            this.memo = new int[m,m];
            return dp(0, 0);
        }      
    }

    class MaximumScoreFromPerformingMultiplicationOperations_BottomUp
    {
        //we learned that while bottom-up is typically faster than top-down, it is often harder to implement.
        // This is because the order in which we iterate needs to be precise.
        public int MaximumScore(int[] nums, int[] multipliers)
        {
            // The time and space complexity of the implementations is O(m^2)
            int n = nums.Length;
            int m = multipliers.Length;
            int[,] dp = new int[m + 1,m + 1]; // need to initialize dp with one extra row so that we don't go out of bounds in the first iteration of the outer loop.

            for (int i = m - 1; i >= 0; i--) // we need to iterate backwards starting from m (because the base case happens when i equals m)
            {
                for (int left = i; left >= 0; left--)
                {
                    int mult = multipliers[i];
                    int right = n - 1 - (i - left);
                    dp[i,left] = Math.Max(mult * nums[left] + dp[i + 1,left + 1],
                                           mult * nums[right] + dp[i + 1,left]);
                }
            }

            return dp[0,0];
        }

        // The time and space complexity of both implementations is O(m^2) where m the length of multipliers
    }

/*    /// <summary>
    /// Given two strings text1 and text2, return the length of their longest common subsequence. If there is no common subsequence, return 0.
    /// A subsequence of a string is a new string generated from the original string with some characters(can be none) deleted without changing the relative order of the remaining characters.
    ///     For example, "ace" is a subsequence of "abcde".
    /// A common subsequence of two strings is a subsequence that is common to both strings.
    /// https://leetcode.com/problems/longest-common-subsequence/
    /// </summary>
    class LongestCommonSubsequence_TopDown
    {
        private int[,] memo;
        private String text1;
        private String text2;  
        public int LongestCommonSubsequence(String text1, String text2)
        {
            // Make the memo big enough to hold the cases where the pointers
            // go over the edges of the strings.
            this.memo = new int[text1.Length + 1,text2.Length + 1];
            // We need to initialise the memo array to -1's so that we know
            // whether or not a value has been filled in. Keep the base cases
            // as 0's to simplify the later code a bit.
            for (int i = 0; i < text1.Length; i++)
            {
                for (int j = 0; j < text2.Length; j++)
                {
                    this.memo[i,j] = -1;
                }
            }
            this.text1 = text1;
            this.text2 = text2;
            return MemoSolve(0, 0);
        }

        private int MemoSolve(int p1, int p2)
        {
            // Check whether or not we've already solved this subproblem.
            // This also covers the base cases where p1 == text1.length
            // or p2 == text2.length.
            if (memo[p1,p2] != -1)
            {
                return memo[p1,p2];
            }

            // Option 1: we don't include text1[p1] in the solution.
            int option1 = MemoSolve(p1 + 1, p2);

            // Option 2: We include text1[p1] in the solution, as long as
            // a match for it in text2 at or after p2 exists.
            int firstOccurence = text2.IndexOf(text1[p1], p2);
            int option2 = 0;
            if (firstOccurence != -1)
            {
                option2 = 1 + MemoSolve(p1 + 1, firstOccurence + 1);
            }

            // Add the best answer to the memo before returning it.
            memo[p1,p2] = Math.Max(option1, option2);
            return memo[p1,p2];
        }

    }

    class LongestCommonSubsequence_BottomUp
    {

    }

    class LongestCommonSubsequence_BottomUp_SpaceOptimization
    {

    }
    /// <summary>
    /// Given an m x n binary matrix filled with 0's and 1's, 
    /// find the largest square containing only 1's and return its area.
    /// https://leetcode.com/problems/maximal-square/
    /// </summary>
    class MaximalSquare_BottomUp
    {
        public int MaximalSquare(char[][] matrix)
        {
            int rows = matrix.Length, cols = rows > 0 ? matrix[0].Length : 0;
            int[][] dp = new int[rows + 1][];
            dp[0] = new int[cols + 1];
            int maxsqlen = 0;
            // for convenience, we add an extra all zero column and row
            // outside of the actual dp table, to simpify the transition
            for (int i = 1; i <= rows; i++)
            { 
                dp[i] = new int[cols + 1];
                for (int j = 1; j <= cols; j++)
                {
                    if (matrix[i - 1][j - 1] == '1')
                    {
                        dp[i][j] = Math.Min(Math.Min(dp[i][j - 1], dp[i - 1][j]), dp[i - 1][j - 1]) + 1;
                        maxsqlen = Math.Max(maxsqlen, dp[i][j]);
                    }
                }
            }
            return maxsqlen * maxsqlen;
        }
    }*/



}
