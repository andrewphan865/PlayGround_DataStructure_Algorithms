using System;
namespace DynamicProgramMing
/* Pathing Problems
    Typically, DP will be applicable when the allowed movement is constrained in a way that prevents moving "backwards", 
    for example if we are only allowed to move down and right.

    If we are allowed to move in all 4 directions, then it might be a graph/BFS problem instead.
    This pattern is sometimes combined with other patterns we have looked at, such as counting DP.
 */
{
    /*
     * There is a robot on an m x n grid. The robot is initially located at the top-left corner (i.e., grid[0][0]). 
    The robot tries to move to the bottom-right corner (i.e., grid[m - 1][n - 1]). 
    The robot can only move either down or right at any point in time.
    Given the two integers m and n, return the number of possible unique paths that the robot can take to reach the bottom-right corner.
    The test cases are generated so that the answer will be less than or equal to 2 * 109.
   https://leetcode.com/problems/unique-paths/description/
     */
    class UniquePaths
    {
        /*
         * Time complexity: O(N×M)\mathcal{O}(N \times M)O(N×M).

        Space complexity: O(N×M)\mathcal{O}(N \times M)O(N×M).
         */
        public int UniquePathsResult(int m, int n)
        {
            int[][] d = new int[m][];
            d[0] = new int[n];
            Array.Fill(d[0], 1);
            for (int row = 1; row < m; ++row)
            {
                d[row] = new int[n];
                Array.Fill(d[row], 1);

                for (int col = 1; col < n; ++col)
                {
                    d[row][col] = d[row - 1][col] + d[row][col - 1];
                }
            }
            return d[m - 1][n - 1];
        }
    }
    /*You are given an m x n integer array grid. There is a robot initially located at the top-left corner (i.e., grid[0][0]). 
    The robot tries to move to the bottom-right corner (i.e., grid[m-1][n-1]). The robot can only move either down or right at any point in time.
    An obstacle and space are marked as 1 or 0 respectively in grid. A path that the robot takes cannot include any square that is an obstacle.
    Return the number of possible unique paths that the robot can take to reach the bottom-right corner.
    The testcases are generated so that the answer will be less than or equal to 2 * 109.
    https://leetcode.com/problems/unique-paths-ii/solution/
     */

    class UniquePathsII
    {
        public int UniquePathsWithObstacles(int[][] obstacleGrid)
        {

            int R = obstacleGrid.Length;
            int C = obstacleGrid[0].Length;

            // If the starting cell has an obstacle, then simply return as there would be
            // no paths to the destination.
            if (obstacleGrid[0][0] == 1)
            {
                return 0;
            }

            // Number of ways of reaching the starting cell = 1.
            obstacleGrid[0][0] = 1;

            // Filling the values for the first column
            for (int i = 1; i < R; i++)
            {
                obstacleGrid[i][0] = (obstacleGrid[i][0] == 0 && obstacleGrid[i - 1][0] == 1) ? 1 : 0;
            }

            // Filling the values for the first row
            for (int i = 1; i < C; i++)
            {
                obstacleGrid[0][i] = (obstacleGrid[0][i] == 0 && obstacleGrid[0][i - 1] == 1) ? 1 : 0;
            }

            // Starting from cell(1,1) fill up the values
            // No. of ways of reaching cell[i][j] = cell[i - 1][j] + cell[i][j - 1]
            // i.e. From above and left.
            for (int i = 1; i < R; i++)
            {
                for (int j = 1; j < C; j++)
                {
                    if (obstacleGrid[i][j] == 0)
                    {
                        obstacleGrid[i][j] = obstacleGrid[i - 1][j] + obstacleGrid[i][j - 1];
                    }
                    else
                    {
                        obstacleGrid[i][j] = 0;
                    }
                }
            }

            // Return value stored in rightmost bottommost cell. That is the destination.
            return obstacleGrid[R - 1][C - 1];

            /*
             *  Time Complexity: O(M×N) times. The rectangular grid given to us is of size M×N times and we process each cell just once.
                Space Complexity: O(1). We are utilizing the obstacleGrid as the DP array. Hence, no extra space.
             */
        }
    }

        /*
       Given a m x n grid filled with non-negative numbers, find a path from top left to bottom right, which Minimizes the sum of all numbers along its path.
      Note: You can only move either down or right at any point in time.
      https://leetcode.com/problems/Minimum-path-sum/description/
       */
    class MinPathSum_DP
    {
        public int MinPathSum_2D(int[][] grid)
        {
            int[][] dp = new int[grid.Length][];

            //grid[0].Length
            for (int i = grid.Length - 1; i >= 0; i--)
            {
                grid[i] = new int[grid[0].Length];
                for (int j = grid[0].Length - 1; j >= 0; j--)
                {
                    if (i == grid.Length - 1 && j != grid[0].Length - 1)
                        dp[i][j] = grid[i][j] + dp[i][j + 1];
                    else if (j == grid[0].Length - 1 && i != grid.Length - 1)
                        dp[i][j] = grid[i][j] + dp[i + 1][j];
                    else if (j != grid[0].Length - 1 && i != grid.Length - 1)
                        dp[i][j] = grid[i][j] + Math.Min(dp[i + 1][j], dp[i][j + 1]);
                    else
                        dp[i][j] = grid[i][j];
                }
            }
            return dp[0][0];
            /*
             Time complexity : O(mn). We traverse the entire matrix once.
            Space complexity : O(mn). Another matrix of the same size is used.
             */
        }

        public int MinPathSum_1D(int[][] grid)
        {
            int[] dp = new int[grid[0].Length];
            for (int i = grid.Length - 1; i >= 0; i--)
            {
                for (int j = grid[0].Length - 1; j >= 0; j--)
                {
                    if (i == grid.Length - 1 && j != grid[0].Length - 1)
                        dp[j] = grid[i][j] + dp[j + 1];
                    else if (j == grid[0].Length - 1 && i != grid.Length - 1)
                        dp[j] = grid[i][j] + dp[j];
                    else if (j != grid[0].Length - 1 && i != grid.Length - 1)
                        dp[j] = grid[i][j] + Math.Min(dp[j], dp[j + 1]);
                    else
                        dp[j] = grid[i][j];
                }
            }
            return dp[0];
            /*
             Time complexity : O(mn). We traverse the entire matrix once.
            Space complexity : O(n). Another array of row size is used.
             */
        }

        public int MinPathSum_WithoutExtraSpace(int[][] grid)
        {
            for (int i = grid.Length - 1; i >= 0; i--)
            {
                for (int j = grid[0].Length - 1; j >= 0; j--)
                {
                    if (i == grid.Length - 1 && j != grid[0].Length - 1)
                        grid[i][j] = grid[i][j] + grid[i][j + 1];
                    else if (j == grid[0].Length - 1 && i != grid.Length - 1)
                        grid[i][j] = grid[i][j] + grid[i + 1][j];
                    else if (j != grid[0].Length - 1 && i != grid.Length - 1)
                        grid[i][j] = grid[i][j] + Math.Min(grid[i + 1][j], grid[i][j + 1]);
                }
            }
            return grid[0][0];
        }

    
    }

    /*
    You are given an m x n integer matrix points (0-indexed). Starting with 0 points, you want to maximize the number of points you can get from the matrix.
    To gain points, you must pick one cell in each row. Picking the cell at coordinates (r, c) will add points[r][c] to your score.

    However, you will lose points if you pick a cell too far from the cell that you picked in the previous row. For every two adjacent rows r and r + 1 (where 0 <= r < m - 1), picking cells at coordinates (r, c1) and (r + 1, c2) will subtract abs(c1 - c2) from your score.

    Return the maximum number of points you can achieve.
    abs(x) is defined as:
        x for x >= 0.
        -x for x < 0.
    https://leetcode.com/problems/maximum-number-of-points-with-cost/
    */
    class MaxPoints_DP
    {
        public long MaxPoints(int[][] points)
        {
            return 0;
        }
    }



}
