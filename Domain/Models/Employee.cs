
namespace Domain.Models
{
	public enum Rank
	{
		parttimer,
		manager
	}
	public class Employee
	{
		public int EmployeeID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public Rank Rank { get; set; }

		public Employee(int employeeID, string firstName, string lastName, Rank rank)
		{
			EmployeeID = employeeID;
			FirstName = firstName;
			LastName = lastName;
			Rank = rank;
		}
        public Employee(string firstName, string lastName, Rank rank)
        {
            EmployeeID = -1;
            FirstName = firstName;
            LastName = lastName;
            Rank = rank;
        }
    }
}
