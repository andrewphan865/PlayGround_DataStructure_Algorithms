using System;
using System.Collections.Generic;


/*
Greedy is an algorithmic paradigm that builds up a solution piece by piece, 
always choosing the next piece that offers the most obvious and immediate benefit. 
Greedy algorithms are used for optimization problems.
An optimization problem can be solved using Greedy if the problem has the following property: 
At every step, we can make a choice that looks best at the moment, and we get the optimal solution of the complete problem. 
If a Greedy Algorithm can solve a problem, then it generally becomes the best method to solve that problem 
as the Greedy algorithms are in general more efficient than other techniques like Dynamic Programming
*/

/*
 Types of Greedy Algorithm:

    1.Activity-Selection: given a set of activities with start and end time (s, e), 
    our task is to schedule maximum non-overlapping activities or remove minimum number of intervals to get maximum non-overlapping intervals.
    In cases that each interval or activity has a cost, we might need to retreat to dynamic programming to solve it.
    2.Frog Jumping: usually it is array. This normally corresponds with the coordinate type of dynamic programming problems.
    3.Data Compression
    4.File Merging
    5.Graph Algorithms, such as Minimum Spanning Trees algorithms (prim and Kruskal), Minimum path (Dijkstra).
    For weighted graph that has negative values, we have to use dynamic programming, such as Bellman Ford algorithms.
 */
namespace Greedy
{
    /*
    Given N activities with their start and finish day given in array start[ ] and end[ ].
    Select the maximum number of activities that can be performed by a single person, 
    assuming that a person can only work on a single activity at a given day.
    Note : Duration of the activity includes both starting and ending day.
    https://practice.geeksforgeeks.org/problems/activity-selection-1587115620/1
    */
    class ActivitySelection
    {
        public int AzctivitySelection(int[] start, int[] end, int n)
        {
            //Your code here
            return 1;
        }
    }

    /*
     * Given an array of intervals intervals where intervals[i] = [starti, endi],
     * return the minimum number of intervals you need to remove to make the rest of the intervals non-overlapping.
     * https://leetcode.com/problems/non-overlapping-intervals/
     */
    class NonOverlappingIntervals_Greedy
    {

    }

    class Solution
    {
        public int MinMeetingRooms(int[][] intervals)
        {
            // Sort Intervals by start time (first index in each array) -- Time: O(nlogn)
            Array.Sort(intervals, (x, y) => x[0].CompareTo(y[0]));
            var minHeap = new PriorityQueue<int, int>(); //Space - O(n)

            // Add the first meeting's end time inside the PQ
            minHeap.Enqueue(intervals[0][1], intervals[0][1]);

            int roomsRequired = 1;
            for (int i = 1; i < intervals.Length; i++)
            {
                // if the current meeting doesn't overlap with previous meeting, remove it from PQ
                if (intervals[i][0] >= minHeap.Peek())
                    minHeap.Dequeue();

                // add next meeting and keep track of current rooms that are occupied
                minHeap.Enqueue(intervals[i][1], intervals[i][1]);
                roomsRequired = Math.Max(roomsRequired, minHeap.Count);
            }

            return roomsRequired;
        }
    }
}
