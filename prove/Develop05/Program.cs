// Showing Creativity and Exceeding Requirements:
// I added a leveling/rank system based on the user's total score.
// This adds extra gamification because the user can see themselves progress
// from Beginner to Eternal Quest Master as they earn more points.

using System;

class Program
{
    static void Main(string[] args)
    {
        GoalManager manager = new GoalManager();
        manager.Start();
    }
}
