using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructureRepo.BackTracking
{
    /*  https://leetcode.com/explore/learn/card/recursion-ii/472/backtracking/2654/
       Backtracking is a general algorithm for finding all (or some) solutions to some computational problems
      (notably Constraint satisfaction problems or CSPs), which incrementally builds candidates to the solution and abandons a candidate 
      ("backtracks")  as soon as it determines that the candidate cannot lead to a valid solution
      There are three types of problems in backtracking : 

        1-  Decision Problem – In this, we search for a feasible solution.
        2 - Optimization Problem – In this, we search for the best solution.
        3 - Enumeration Problem – In this, we find all feasible solutions.
     */


    /*
     * BackTracking pseudocode template
      
    void findSolutions(n, other params) :
        if (found a solution) :
            solutionsFound = solutionsFound + 1;
            displaySolution();
            if (solutionsFound >= solutionTarget) : 
                System.exit(0);
            return

        for (val = first to last) :
            if (isValid(val, n)) :
                applyValue(val, n);
                findSolutions(n+1, other params);
                removeValue(val, n);
     */
}
