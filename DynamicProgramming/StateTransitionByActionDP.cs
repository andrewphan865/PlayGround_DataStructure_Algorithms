using System;

namespace DynamicProgramming
{
    /*    
    1. A function or data structure that will compute/contain the answer to the problem for every given state.  dp(i)
        a) What state variables we need to pass to it?           

        b)  what it will return?          

    2. A recurrence relation to transition between states  dp(i) = dp(i - 1) + dp(i - 2)
            
    3. Base cases                                          dp(1) = 1 and dp(2) = 2       
 */
    /*
       State Transition by Inaction
     
    This is a small pattern that occasionally shows up in DP problems. Here, "doing nothing" refers to two different states having the same value. 
    We're calling it "doing nothing" because often the way we arrive at a new state with the same value as the previous state is by "doing nothing". 
    Of course, a decision making process needs to coexist with this pattern, because if we just had all states having the same value, the problem wouldn't really make sense dp(i) = dp(i - 1)?
    It is just that if we are trying to maximize or minimize a score for example, sometimes the best option is to "do nothing", which leads to two states having the same value. 

    The actual recurrence relation would look something like dp(i, j) = max(dp(i - 1, j), ...).
     
     Usually when we "do nothing", it is by moving to the next element in some input array (which we usually use i as a state variable for).
    As mentioned above, this will be part of a decision making process due to some restriction in the problem.
    For example, think back to House Robber: we could choose to rob or not rob each house we were at. 
    Sometimes, not robbing the house is the best decision (because we aren't allowed to rob adjacent houses), then dp(i) = dp(i - 1).
     */

    /*
     * You are given an integer array prices where prices[i] is the price of a given stock on the ith day, and an integer k.
     * Find the maximum profit you can achieve. You may complete at most k transactions.
     * Note: You may not engage in multiple transactions simultaneously (i.e., you must sell the stock before you buy again).
     * https://leetcode.com/problems/best-time-to-buy-and-sell-stock-iv/
     */

    /*    
        1. A function or data structure that will compute/contain the answer to the problem for every given state.  dp(i)
            a) What state variables we need to pass to it?           

            b)  what it will return?          

        2. A recurrence relation to transition between states  dp(i) = dp(i - 1) + dp(i - 2)
            
        3. Base cases                                          dp(1) = 1 and dp(2) = 2       
    */


    class BestTimetoBuyandSellStockIV_Topdown
    {
        private int[] prices;
        private int[,,] memo;
        // turns the maximum achievable profit starting from the ith day with transactionsRemaining transactions remaining, and 
        // holding indicating if we start with a stock or not.
        private int dp(int i, int transactionsRemaining, int holding)
        {
            // Base cases
            if (transactionsRemaining == 0 || i == prices.Length)
            {
                return 0;
            }

            if (memo[i,transactionsRemaining,holding] == 0)
            {
                int doNothing = dp(i + 1, transactionsRemaining, holding);
                int doSomething;

                if (holding == 1)
                {
                    // Sell Stock
                    doSomething = prices[i] + dp(i + 1, transactionsRemaining - 1, 0);
                }
                else
                {
                    // Buy Stock
                    doSomething = -prices[i] + dp(i + 1, transactionsRemaining, 1);
                }

                // Recurrence relation. Choose the most profitable option.
                memo[i,transactionsRemaining,holding] = Math.Max(doNothing, doSomething);
            }

            return memo[i,transactionsRemaining,holding];
        }

        public int MaxProfit(int k, int[] prices)
        {
            this.prices = prices;
            this.memo = new int[prices.Length,k + 1,2];
            return dp(0, k, 0);
        }
    }
    /*
     the recurrence relation is the same with top-down, but we need to be careful about how we configure our for loops.
     The base cases are automatically handled because the dp array is initialized with all values set to 0. 
     For iteration direction and order, remember with bottom-up we start at the base cases. 
     Therefore we will start iterating from the end of the input and with only 1 transaction remaining.
     */

    class BestTimetoBuyandSellStockIV_BottomUp
    {
        public int maxProfit(int k, int[] prices)
        {
            int n = prices.Length;
            int [,,] dp = new int[n + 1,k + 1,2];

            for (int i = n - 1; i >= 0; i--)
            {
                for (int transactionsRemaining = 1; transactionsRemaining <= k; transactionsRemaining++)
                {
                    for (int holding = 0; holding < 2; holding++)
                    {
                        int doNothing = dp[i + 1,transactionsRemaining,holding];
                        int doSomething;
                        if (holding == 1)
                        {
                            // Sell stock
                            doSomething = prices[i] + dp[i + 1,transactionsRemaining - 1,0];
                        }
                        else
                        {
                            // Buy stock
                            doSomething = -prices[i] + dp[i + 1,transactionsRemaining,1];
                        }

                        // Recurrence relation
                        dp[i,transactionsRemaining,holding] = Math.Max(doNothing, doSomething);
                    }
                }
            }

            return dp[0,k,0];
        }
    }

    /*
     * You are given an array prices where prices[i] is the price of a given stock on the ith day.
        Find the maximum profit you can achieve. You may complete as many transactions as you like 
        (i.e., buy one and sell one share of the stock multiple times) with the following restrictions:
            After you sell your stock, you cannot buy stock on the next day (i.e., cooldown one day).
        Note: You may not engage in multiple transactions simultaneously (i.e., you must sell the stock before you buy again).
    https://leetcode.com/problems/best-time-to-buy-and-sell-stock-with-cooldown/
     */

    /*
     * Let us define a state machine to model our agent. The state machine consists of three states, which we define as follows:

            state held: in this state, the agent holds a stock that it bought at some point before.
            state sold: in this state, the agent has just sold a stock right before entering this state. And the agent holds no stock at hand.
            state reset: first of all, one can consider this state as the starting point, where the agent holds no stock and did not sell a stock before. 
    More importantly, it is also the transient state before the held and sold. Due to the cooldown rule, after the sold state, 
    the agent can not immediately acquire any stock, but is forced into the reset state. One can consider this state as a "reset" button for the cycles of buy and sell transactions.
    At any moment, the agent can only be in one state. The agent would transition to another state by performing some actions, namely:

            action sell: the agent sells a stock at the current moment. After this action, the agent would transition to the sold state.
            action buy: the agent acquires a stock at the current moment. After this action, the agent would transition to the held state.
            action rest: this is the action that the agent does no transaction, neither buy or sell. 
    For instance, while holding a stock at the held state, the agent might simply do nothing, 
    and at the next moment the agent would remain in the held state
     */
    class BestTimeToBuyAndSellStockWithCooldown_StateMachine
    {
        public int MaxProfit(int[] prices)
        {

            int sold = Int32.MinValue;  // just sold a stock right before entering this state. And the agent holds no stock at hand
            int held = Int32.MinValue;  //holds a stock that it bought at some point before.
            int reset = 0;              //holds no stock and did not sell a stock before.

            foreach (int price in prices)
            {
                int preSold = sold;

                sold = held + price;
                held = Math.Max(held, reset - price);
                reset = Math.Max(reset, preSold);
            }

            return Math.Max(sold, reset);
        }
    }

}
