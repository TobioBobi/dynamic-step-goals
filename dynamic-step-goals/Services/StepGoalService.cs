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
    }
}
