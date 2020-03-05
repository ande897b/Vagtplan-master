
namespace Domain.Models
{
    public class DutyExchange
    {
        public int DutyExchangeID { get; set; }
        public int DutyID { get; set; }
        public int EmployeeID { get; set; }

        public DutyExchange (int dutyExchangeID, int dutyID, int employeeID)
        {
            DutyExchangeID = dutyExchangeID;
            DutyID = dutyID;
            EmployeeID = employeeID;
        }
        public DutyExchange(int dutyID, int employeeID)
        {
            DutyExchangeID = -1;
            DutyID = dutyID;
            EmployeeID = employeeID;
        }
    }
}
