using Application.Repositories;
using Domain.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Application.DatabaseControllers
{
    public static class DBDateController
    {
        public static void LoadDates()
        {
            DBConnection.DatabaseName = "CANE";
            if (DBConnection.IsConnected())
            {
                string query = "SELECT * FROM Date";
                var cmd = new SqlCommand(query, DBConnection.Connection);
                cmd.CommandType = CommandType.Text;
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int dateID = (int)reader["DateID"];
                        int rosterID = (int)reader["RosterID"];
                        DateTime day = (DateTime)reader["Day"];
                        string shopDB = reader["Shop"].ToString();
                        Shop newShop;
                        if (shopDB == "kongensgade")
                        {
                            newShop = Shop.kongensgade;
                        }
                        else
                        {
                            newShop = Shop.skibhusvej;
                        }
                        Date date = new Date(day, dateID, rosterID, newShop);
                        DateRepository.AddDate(date);
                    }
                }
                DBConnection.Close();
            }
        }

        public static void CreateDates(Roster roster)
        {
            DBConnection.DatabaseName = "CANE";
            string query = "Create_Dates";
            int daysDiff = ((TimeSpan)(roster.EndDate.Date - roster.StartDate.Date)).Days;
            Shop newShop;
            if (roster.Shop.ToString().ToLower() == "kongensgade")
            {
                newShop = Shop.kongensgade;
            }
            else
            {
                newShop = Shop.skibhusvej;
            }
            for (int i = 0; i <= daysDiff; i++)
            {
                Date date = new Date(roster.StartDate.AddDays(i), roster.RosterID, newShop);
                if (!DateRepository.DateExist(date))
                {
                    if (DBConnection.IsConnected())
                    {
                        var cmd = new SqlCommand(query, DBConnection.Connection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@RosterID_IN", roster.RosterID));
                        cmd.Parameters.Add(new SqlParameter("@Shop_IN", roster.Shop.ToString()));
                        cmd.Parameters.Add(new SqlParameter("@Day_IN", roster.StartDate.AddDays(i)));
                        cmd.ExecuteReader();
                    }
                    DBConnection.Close();
                    DateRepository.AddDate(date);
                }
            }
        }
    }
}
    

