using System;

namespace Domain.Models
{
    public class Duty
    {
        public int DutyID { get; set; }
        public int EmployeeID { get; set; }
        public int DateID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public Duty (int employeeID, int dateID, DateTime startTime, DateTime endTime)
        {
            DutyID = -1;
            EmployeeID = employeeID;
            DateID = dateID;
            StartTime = startTime;
            EndTime = endTime;
        }

        public Duty(int dutyID, int employeeID, int dateID, DateTime startTime, DateTime endTime)
        {
            DutyID = dutyID;
            EmployeeID = employeeID;
            DateID = dateID;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}