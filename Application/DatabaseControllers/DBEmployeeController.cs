using Application.Repositories;
using Domain.Models;
using System.Data;
using System.Data.SqlClient;

namespace Application.DatabaseControllers
{
    public static class DBEmployeeController
    {
        public static void LoadEmployees()
        {
            DBConnection.DatabaseName = "CANE";
            if (DBConnection.IsConnected())
            {
                string query = "SELECT * FROM Employee";
                SqlCommand cmd = new SqlCommand(query, DBConnection.Connection);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int employeeID = (int)reader["EmployeeID"];
                        string rank = reader["EmployeeRank"].ToString();
                        Rank newRank;
                        if (rank == "Parttimer")
                        {
                            newRank = Rank.parttimer;
                        }
                        else
                        {
                            newRank = Rank.manager;
                        }
                        string firstName = reader["FirstName"].ToString();
                        string lastName = reader["LastName"].ToString();
                        Employee employee = new Employee(employeeID, firstName, lastName, newRank);
                        EmployeeRepository.AddEmployee(employee);
                    }
                }
                DBConnection.Close();
            }
        }

        public static void CreateEmployee(Employee employee)
        {
            string query = "Create_Employee";
            DBConnection.DatabaseName = "CANE";
            if (!EmployeeRepository.EmployeeExist(employee))
            {
                if (DBConnection.IsConnected())
                {
                    var cmd = new SqlCommand(query, DBConnection.Connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FirstName_IN", employee.FirstName));
                    cmd.Parameters.Add(new SqlParameter("@LastName_IN", employee.LastName));
                    cmd.Parameters.Add(new SqlParameter("@EmployeeRank_IN", employee.Rank.ToString()));
                    cmd.ExecuteReader();
                    DBConnection.Close();
                }
                EmployeeRepository.AddEmployee(employee);
            }
        }

        public static void DeleteEmployee(int employeeID)
        {
            DBWishForDayOffController.DeleteWishForDayOffs(employeeID);
            DBDutyController.DeleteDuties_EmpID(employeeID);
            DBConnection.DatabaseName = "CANE";
            string query = "Delete_Employee";
            if (DBConnection.IsConnected())
            {
                var cmd = new SqlCommand(query, DBConnection.Connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EmployeeID_IN", employeeID));
                cmd.ExecuteReader();
                DBConnection.Close();
            }
            EmployeeRepository.RemoveEmployee(employeeID);
        }
    }
}
