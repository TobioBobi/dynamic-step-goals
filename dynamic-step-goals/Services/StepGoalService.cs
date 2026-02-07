using dynamic_step_goals.Models;

namespace dynamic_step_goals.Services
{
    internal class StepGoalService
    {

        public int CalculateTomorrowGoal(DailyStepEntry dailyStepEntry, List<DailyStepEntry> history)
        {
            const double adjustmentFactor = 0.3;
            const int maxIncrease = 800;
            const int maxDecrease = 500;

            var delta = dailyStepEntry.CurrentSteps - dailyStepEntry.Goal;
            var relativeDelta = (double)delta / dailyStepEntry.Goal;

            var rawAdjustment = dailyStepEntry.Goal * relativeDelta * adjustmentFactor;
            var adjustment = (int)Math.Round(rawAdjustment);

            adjustment = Math.Clamp(adjustment, -maxDecrease, maxIncrease);

            var tomorrowGoal = dailyStepEntry.Goal + adjustment;
            return tomorrowGoal;
        }
    }
}
