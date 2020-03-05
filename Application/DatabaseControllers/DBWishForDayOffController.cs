using Application.Repositories;
using Domain.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Application.DatabaseControllers
{
    public static class DBWishForDayOffController
    {
        public static void LoadWishForDayOffs()
        {
            DBConnection.DatabaseName = "CANE";
            if (DBConnection.IsConnected())
            {
                string query = "select * from WishForDayOff";
                var cmd = new SqlCommand(query, DBConnection.Connection);
                cmd.CommandType = CommandType.Text;              
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int wishForDayOffID = (int)reader["WishForDayOffID"];
                        int employeeID = (int)reader["EmployeeID"];
                        DateTime day = (DateTime)reader["Date"];
                        WishForDayOff newWish = new WishForDayOff(wishForDayOffID, employeeID, day);
                        WishForDayOffRepository.AddWishForDayOff(newWish);
                    }
                }
                DBConnection.Close();
            }
        }

        public static void CreateWishForDayOff(WishForDayOff wish)
        {
            string query = "Create_WishForDayOff";
            DBConnection.DatabaseName = "CANE";
            if (!WishForDayOffRepository.WishForDayOffExist(wish))
            {
                if (DBConnection.IsConnected())
                {
                    var cmd = new SqlCommand(query, DBConnection.Connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@EmployeeID_IN", wish.EmployeeID));
                    cmd.Parameters.Add(new SqlParameter("@Date_IN", wish.Date));
                    cmd.ExecuteReader();
                    DBConnection.Close();
                }
                WishForDayOffRepository.AddWishForDayOff(wish);
            }
        }

        public static void DeleteWishForDayOffs(int employeeID)
        {
            DBConnection.DatabaseName = "CANE";
            string query = "Delete_WishForDayOffs";
            if (DBConnection.IsConnected())
            {
                var cmd = new SqlCommand(query, DBConnection.Connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EmployeeID_IN", employeeID));
                cmd.ExecuteReader();
                DBConnection.Close();
            }
            WishForDayOffRepository.RemoveWishForDayOffs(employeeID);
        }
    }
}
