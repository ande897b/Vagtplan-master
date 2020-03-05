using Domain.Models;
using System;
using System.Collections.Generic;

namespace Application.Repositories
{
    public static class DateRepository
    {
        private static List<Date> dates = new List<Date>();
        public static void AddDate(Date date)
        {
            if (!DateExist(date))
                dates.Add(date);
        }

        public static bool DateExist(Date date)
        {
            bool exist = false;
            foreach (Date date2 in dates)
            {
                if (date2.Day == date.Day && date2.Shop == date.Shop)
                {
                    if (date2.DateID != date.DateID)
                    {
                        date2.DateID = date.DateID;
                    }
                    exist = true;
                }
            }
            return exist;
        }

        public static DateTime GetDate(string date)
        {
            DateTime newDate = DateTime.Now;
            foreach (Date date2 in dates)
            {
                if (date2.Day.ToString().Substring(0,10) == date.Substring(0,10))
                {
                    newDate = date2.Day;
                }
            }
            return newDate;
        }

        public static List<Date> GetDates(string shop)
        {
            List<Date> newDates = new List<Date>();
            foreach (Date date in dates)
            {
                if(date.Shop.ToString().Substring(0,10) == shop.Substring(0, 10))
                {
                    newDates.Add(date);
                }
            }
            return dates;
        }

        public static int GetDateID(string date, Shop shop)
        {
            int id = 0;
            foreach (Date day in dates)
            {
                if (day.Day.ToString().Substring(0, 10) == date.Substring(0,10) && day.Shop == shop)
                {
                    id = day.DateID;
                }
            }
            return id;
        }

        public static bool CheckIfDateExists(string date, string shop)
        {
            bool checkIfTrue = false;
            foreach (var day in dates)
            {
                if (day.Day.ToString().Substring(0, 10) == date.Substring(0, 10) && day.Shop.ToString() == shop.ToLower())
                {
                    checkIfTrue = true;
                }
            }
            return checkIfTrue;
        }
    }
}
