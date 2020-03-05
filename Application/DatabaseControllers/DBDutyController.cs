using Application.Repositories;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Application.DatabaseControllers
{
    public static class DBDutyController
    {
        public static void LoadDuties()
        {
            DBConnection.DatabaseName = "CANE";
            if (DBConnection.IsConnected())
            {
                string query = "SELECT * FROM Duty";
                SqlCommand cmd = new SqlCommand(query, DBConnection.Connection);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int employeeID = (int)reader["EmployeeID"];
                        int dateID = (int)reader["DateID"];
                        int dutyID = (int)reader["DutyID"];
                        DateTime startTime = (DateTime)reader["StartTime"];
                        DateTime endTime = (DateTime)reader["EndTime"];
                        Duty duty = new Duty(dutyID, employeeID, dateID, startTime, endTime);
                        DutyRepository.AddDuty(duty);
                    }
                }
                DBConnection.Close();
            }
        }

        public static int GetDutyID(int employeeID, int dateID, DateTime startTime, DateTime endTime)
        {
            int dutyID = 0;
            DBConnection.DatabaseName = "CANE";
            if (DBConnection.IsConnected())
            {
                string query = "Get_Duty";
                SqlCommand cmd = new SqlCommand(query, DBConnection.Connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EmployeeID_IN", employeeID));
                cmd.Parameters.Add(new SqlParameter("@DateID_IN", dateID));
                cmd.Parameters.Add(new SqlParameter("@StartTime_IN", startTime));
                cmd.Parameters.Add(new SqlParameter("@EndTime_IN", endTime));
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        dutyID = (int)reader["DutyID"];
                    }
                }
                DBConnection.Close();
            }
            return dutyID;
        }

        public static void CreateDuty(Duty duty)
        {
            string query = "Create_Duty";
            DBConnection.DatabaseName = "CANE";
            if (!DutyRepository.DutyExist(duty))
            {
                if (DBConnection.IsConnected())
                {
                    var cmd = new SqlCommand(query, DBConnection.Connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@EmployeeID_IN", duty.EmployeeID));
                    cmd.Parameters.Add(new SqlParameter("@DateID_IN", duty.DateID));
                    cmd.Parameters.Add(new SqlParameter("@StartTime_IN", duty.StartTime));
                    cmd.Parameters.Add(new SqlParameter("@EndTime_IN", duty.EndTime));
                    cmd.ExecuteReader();
                    DBConnection.Close();
                }
                DutyRepository.AddDuty(duty);
            }
        }

        public static void DeleteDuties(int dateID)
        {

            
            string query = "Delete_Duties";
            DBConnection.DatabaseName = "CANE";
            if (DBConnection.IsConnected())
            {
                var cmd = new SqlCommand(query, DBConnection.Connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@DateID_IN", dateID));
                cmd.ExecuteReader();
                DBConnection.Close();
            }
        }

        public static void DeleteDuties_EmpID(int employeeID)
        {
            string query = "Delete_Duties_EmpID";
            DBConnection.DatabaseName = "CANE";
            if (DBConnection.IsConnected())
            {
                var cmd = new SqlCommand(query, DBConnection.Connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EmployeeID_IN", employeeID));
                cmd.ExecuteReader();
                DBConnection.Close();
            }
            DutyRepository.Removeduties_EmpID(employeeID);
        }

        public static void UpdateDuty(int newEmployeeID, int dutyID)
        {
            DBConnection.DatabaseName = "CANE";
            string query = "Update_Duty";
            if (DBConnection.IsConnected())
            {
                var cmd = new SqlCommand(query, DBConnection.Connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@DutyID_IN", dutyID));
                cmd.Parameters.Add(new SqlParameter("@EmployeeID_IN", newEmployeeID));
                cmd.ExecuteReader();
            }
            DBConnection.Close();
            DutyRepository.UpdateDuty(newEmployeeID, dutyID);
        }
    }
}
