using dynamic_step_goals.Models;

namespace dynamic_step_goals.Services
{
    internal class StepGoalService
    {

        public int CalculateTomorrowGoal(DailyStepEntry dailyStepEntry, List<DailyStepEntry> history)
        {
            var delta = dailyStepEntry.CurrentSteps - dailyStepEntry.Goal;
            int adjustment;

            // If the goal was exceeded by at least 1000 steps
            if (delta >= 1000)
                adjustment = 500; // Increase tomorrow's goal by 500 steps
            // If the goal was almost reached (no more than 500 steps below the goal)
            else if (delta >= -500)
                adjustment = 200; // Moderately increase tomorrow's goal
            // If the goal was missed by more than 500 steps
            else
                adjustment = -200; // Decrease tomorrow's goal

            return dailyStepEntry.Goal + adjustment;
        }


    }
}
