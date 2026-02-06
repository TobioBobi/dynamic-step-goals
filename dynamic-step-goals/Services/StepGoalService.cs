using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dynamic_step_goals.Services
{
    internal class StepGoalService
    {
        public int CalculateTomorrowGoal(int todaysSteps)
        {
            return todaysSteps + 100;
        }

        internal (int todayGoal, int tomorrowGoal) CalculateGoals(int todaySteps, int todayGoal)
        {
            return (5, 10);
        }
    }
}
