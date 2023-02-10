using System.Collections.Generic;

namespace AdaptiveCardPlayground.Models
{
    public class ApplyLeaveForm
    {
        public string Applicant { get; set; }

        public string Sex { get; set; }

        public List<LeaveAgent> Agents { get; set; } = new List<LeaveAgent>();

        public string LeaveType { get; set; }

        public string StartDate { get; set; }

        public double LeaveHours { get; set; }

        public string Subject { get; set; }
    }

    public class LeaveAgent
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public LeaveAgent()
        {

        }
        public LeaveAgent(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
