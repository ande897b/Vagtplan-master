using Domain.Models;
using System;
using System.Collections.Generic;

namespace Application.Repositories
{
    public static class WishForDayOffRepository
    {
        private static List<WishForDayOff> wishForDayOffList = new List<WishForDayOff>();
        public static void AddWishForDayOff(WishForDayOff wishForDayOff)
        {
            if (!WishForDayOffExist(wishForDayOff))
                wishForDayOffList.Add(wishForDayOff);
        }

        public static bool WishForDayOffExist(WishForDayOff wishForDayOff)
        {
            bool exist = false;
            foreach (WishForDayOff wishForDayOff2 in wishForDayOffList)
            {
                if (wishForDayOff2.Date == wishForDayOff.Date && wishForDayOff2.EmployeeID == wishForDayOff.EmployeeID)
                {
                    if (wishForDayOff2.WishForDayOffID != wishForDayOff.WishForDayOffID)
                    {
                        wishForDayOff2.WishForDayOffID = wishForDayOff.WishForDayOffID;
                    }
                    exist = true;
                }
            }
            return exist;
        }

        public static List<WishForDayOff> GetWishForDayOffs(string date)
        {
            List<WishForDayOff> newListOfWishes = new List<WishForDayOff>();
            foreach (WishForDayOff wish in wishForDayOffList)
            {
                if (wish.Date.ToString().Substring(0,10) == date.Substring(0,10))
                {
                    newListOfWishes.Add(wish);
                }
            }
            return newListOfWishes;
        }

        public static void RemoveWishForDayOffs(int employeeID)
        {
            try
            {
                foreach (WishForDayOff wishForDayOff in wishForDayOffList)
                {
                    if (wishForDayOff.EmployeeID == employeeID)
                    {
                        wishForDayOffList.Remove(wishForDayOff);
                    }
                }
            }
            catch (Exception) { }
        }
    }
}
