using System;

namespace DataStructureRepo.DynamicProgramming.Practices
{
    /*
    1. A function or data structure that will compute/contain the answer to the problem for every given state.  dp(i)
        a) What state variables we need to pass to it?           

        b)  what it will return?          

    2. A recurrence relation to transition between states  dp(i) = dp(i - 1) + dp(i - 2)
            
    3. Base cases                                          dp(1) = 1 and dp(2) = 2       
    */

    /*
     * Usually, solving and fully understanding a dynamic programming problem is a 4 step process:
        1.Start with the recursive backtracking solution
        2. Optimize by using a memoization table (top-down2 dynamic programming)
        3.Remove the need for recursion (bottom-up dynamic programming)
        4.Apply final tricks to reduce the time / memory complexity
     */
    
 #region Jump Game 1
    /*
     * You are given an integer array nums. You are initially positioned at the array's first index, 
     * and each element in the array represents your maximum jump Length at that position.
    Return true if you can reach the last index, or false otherwise.
    https://leetcode.com/problems/jump-game/description/
     */


    class JumpGame_Backtracking
    {
        /*
         * This is the inefficient solution where we try every single jump pattern that takes us from the first position to the last.
         * We start from the first position and jump to every index that is reachable. We repeat the process until last index is reached. When stuck, Backtrack.
         * 
         * Time complexity : O(2^n) There are 2^n (upper bound) ways of jumping from the first position to the last, where n is the Length of array nums.
         * For a complete proof, please refer to Appendix A.

        Space complexity : O(n). Recursion requires additional memory for the stack frames.
         */
        public bool CanJumpFromPosition(int position, int[] nums)
        {
            if (position == nums.Length - 1)
            {
                return true;
            }

            int furthestJump = Math.Min(position + nums[position], nums.Length - 1);
            for (int nextPosition = furthestJump; nextPosition > position; nextPosition--)
            {
                if (CanJumpFromPosition(nextPosition, nums))
                {
                    return true;
                }
            }

            return false;
        }

        public bool CanJump(int[] nums)
        {
            return CanJumpFromPosition(0, nums);
        }

    }


    class JumpGame_TopDown
    {
        /*
         * Top-down Dynamic Programming can be thought of as optimized backtracking.
         * It relies on the observation that once we determine that a certain index is good / bad, this result will never change. 
         * This means that we can store the result and not need to recompute it every time.
         * 
         * Time complexity : O(n^2)
        For every element in the array, say i, we are looking at the next nums[i] elements to its right aiming to find a GOOD index. 
        nums[i] can be at most n, where n is the Length of array nums.

        Space complexity : O(2n)=O(n). First n originates from recursion. Second n comes from the usage of the memo table.
         */
        enum Index
        {
            GOOD, BAD, UNKNOWN
        }

        Index[] memo;

        public bool CanJumpFromPosition(int position, int[] nums)
        {
            if (memo[position] != Index.UNKNOWN)
            {
                return memo[position] == Index.GOOD ? true : false;
            }

            int furthestJump = Math.Min(position + nums[position], nums.Length - 1);
            for (int nextPosition = position + 1; nextPosition <= furthestJump; nextPosition++)
            {
                if (CanJumpFromPosition(nextPosition, nums))
                {
                    memo[position] = Index.GOOD;
                    return true;
                }
            }

            memo[position] = Index.BAD;
            return false;
        }

        public bool canJump(int[] nums)
        {
            memo = new Index[nums.Length];
            for (int i = 0; i < memo.Length; i++)
            {
                memo[i] = Index.UNKNOWN;
            }
            //base case
            memo[memo.Length - 1] = Index.GOOD;

            return CanJumpFromPosition(0, nums);
        }
    }

    class JumpGame_BottomUp
    {
        /*
         * Top-down to bottom-up conversion is done by eliminating recursion. 
         * In practice, this achieves better performance as we no longer have the method stack overhead and might even benefit from some caching.
         * More importantly, this step opens up possibilities for future optimization.
         * The recursion is usually eliminated by trying to reverse the order of the steps from the top-down approach.
         * 
         * Time complexity : O(n^2). For every element in the array, say i, we are looking at the next nums[i] elements to its right aiming to find a GOOD index. 
         * nums[i] can be at most nnn, where nnn is the length of array nums.
           Space complexity : O(n). This comes from the usage of the memo table.
         */
        enum Index
        {
            GOOD, BAD, UNKNOWN
        }
        public bool CanJump(int[] nums)
        {
            Index[] memo = new Index[nums.Length];
            for (int i = 0; i < memo.Length; i++)
            {
                memo[i] = Index.UNKNOWN;
            }
            //base case
            memo[memo.Length - 1] = Index.GOOD;



            for (int i = nums.Length - 2; i >= 0; i--)
            {
                int furthestJump = Math.Min(i + nums[i], nums.Length - 1);
                for (int j = i + 1; j <= furthestJump; j++)
                {
                    if (memo[j] == Index.GOOD)
                    {
                        memo[i] = Index.GOOD;
                        break;
                    }
                }
            }

            return memo[0] == Index.GOOD;
        }

    }

    class JumpGame_Greedy
    {
        /*
         * Once we have our code in the bottom-up state, we can make one final, important observation. 
         * From a given position, when we try to see if we can jump to a GOOD position, we only ever use one - the first one (see the break statement). In other words, the left-most one.
         * If we keep track of this left-most GOOD position as a separate variable, we can avoid searching for it in the array. Not only that, but we can stop using the array altogether.
         *
         * Time complexity : O(n). We are doing a single pass through the nums array, hence nnn steps, where nnn is the length of array nums.
            Space complexity : O(1). We are not using any extra memory.
         */

        public bool CanJump(int[] nums)
        {
            int lastPos = nums.Length - 1;
            for (int i = nums.Length - 1; i >= 0; i--)
            {
                if (i + nums[i] >= lastPos)
                {
                    lastPos = i;
                }
            }
            return lastPos == 0;
        }
    }

    #endregion

    #region Jump Game II
    class JumpGameII_Greedy
    {       
        public int Jump(int[] nums)
        {
            int n = nums.Length;
            int jumpCount = 0, reach = 0, next = 0;

            for (int i = 0; i < n && reach < n - 1; i++)
            {
                next = Math.Max(next, nums[i] + i);

                if (i == reach)
                {
                    reach = next;
                    jumpCount++;
                }
            }

            return jumpCount;
        }
    }

    #endregion



}
