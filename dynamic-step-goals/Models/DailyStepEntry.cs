using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dynamic_step_goals.Models
{
    public class DailyStepEntry
    {
        public DateOnly Date { get; set; }
        public int Goal { get; set; }
        public int ActualSteps { get; set; }
    }
}
