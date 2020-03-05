using System;
using System.Data.SqlClient;
using Domain.Models;
using System.Data;
using Application.Repositories;

namespace Application.DatabaseControllers
{
    public static class DBRosterController
    {
        public static void LoadRosters()
        {
            DBConnection.DatabaseName = "CANE";
            if (DBConnection.IsConnected())
            {
                string query = "SELECT * FROM Roster";
                SqlCommand cmd = new SqlCommand(query, DBConnection.Connection);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int rosterID = (int)reader["RosterID"];
                        DateTime startDate = (DateTime)reader["StartDate"];
                        DateTime endDate = (DateTime)reader["EndDate"];
                        string shop = reader["Shop"].ToString();
                        Shop newShop;
                        if (shop == "kongensgade")
                        {
                            newShop = Shop.kongensgade;
                        }
                        else 
                        {
                            newShop = Shop.skibhusvej;
                        }
                        Roster addRoster = new Roster(rosterID, startDate, endDate, newShop);
                        RosterRepository.AddRoster(addRoster);
                    }
                }
                DBConnection.Close();
            }
        }

        public static void CreateRoster(Roster roster)
        {
            DBConnection.DatabaseName = "CANE";
            string query = "Create_Roster";
            if (!RosterRepository.RosterExist(roster))
            {
                if (DBConnection.IsConnected())
                {
                    var cmd = new SqlCommand(query, DBConnection.Connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@StartDate_IN", roster.StartDate));
                    cmd.Parameters.Add(new SqlParameter("@EndDate_IN", roster.EndDate));
                    cmd.Parameters.Add(new SqlParameter("@Shop_IN", roster.Shop.ToString()));
                    cmd.ExecuteReader();
                    DBConnection.Close();
                }
                RosterRepository.AddRoster(roster);
            }
        }

        public static int GetRosterID(Roster roster)
        {
            int rosterID = 0;
            DBConnection.DatabaseName = "CANE";
            if (DBConnection.IsConnected())
            {
                string query = "Get_Roster";
                var cmd = new SqlCommand(query, DBConnection.Connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("StartDate_IN", roster.StartDate));
                cmd.Parameters.Add(new SqlParameter("EndDate_IN", roster.EndDate));
                cmd.Parameters.Add(new SqlParameter("Shop_IN", roster.Shop.ToString()));
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        rosterID = (int)reader["RosterID"];
                    }
                }
                DBConnection.Close();
            }
            return rosterID;
        }
    }
}
        
