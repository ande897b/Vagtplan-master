using Application.Repositories;
using Domain.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Application.DatabaseControllers
{
    public static class DBDutyExchangeController
    {
        public static void LoadDutyExchanges()
        {
            DBConnection.DatabaseName = "CANE";
            if (DBConnection.IsConnected())
            {
                string query = "SELECT * FROM DutyExchange";
                var cmd = new SqlCommand(query, DBConnection.Connection);
                cmd.CommandType = CommandType.Text;
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int dutyExchangeID = (int)reader["DutyExchangeID"];
                        int dutyID = (int)reader["DutyID"];
                        int employeeID = (int)reader["EmployeeID"];
                        DutyExchange newDutyExchange = new DutyExchange(dutyExchangeID, dutyID, employeeID);
                        DutyExchangeRepository.AddDutyExchange(newDutyExchange);
                    }
                }
                DBConnection.Close();
            }
        }

        public static void CreateDutyExchange(DutyExchange dutyExchange)
        {
            DBConnection.DatabaseName = "CANE";
            string query = "Create_DutyExchange";
            if (!DutyExchangeRepository.DutyExchangeExist(dutyExchange))
            {
                if (DBConnection.IsConnected())
                {
                    var cmd = new SqlCommand(query, DBConnection.Connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@DutyID_IN", dutyExchange.DutyID));
                    cmd.Parameters.Add(new SqlParameter("@EmployeeID_IN", dutyExchange.EmployeeID));
                    cmd.ExecuteReader();
                }
                DBConnection.Close();
            }
            DutyExchangeRepository.AddDutyExchange(dutyExchange);
        }
        
        public static void DeleteDutyExchange(int dutyID)
        {
            DBConnection.DatabaseName = "CANE";
            string query = "Delete_DutyExchanges";
            if (DBConnection.IsConnected())
            {
                var cmd = new SqlCommand(query, DBConnection.Connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@DutyID_IN", dutyID));
                cmd.ExecuteReader();
                DBConnection.Close();
            }
        }
    }    
}
