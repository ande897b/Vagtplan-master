using Domain.Models;
using System;
using System.Collections.Generic;

namespace Application.Repositories
{
    public static class DutyExchangeRepository
    {
        private static List<DutyExchange> dutyExchanges = new List<DutyExchange>();

        public static void AddDutyExchange(DutyExchange dutyExchange)
        {
            if (!DutyExchangeExist(dutyExchange))
                dutyExchanges.Add(dutyExchange);
        }
        

        public static DutyExchange GetDutyExchange(int dutyID)
        {
            DutyExchange tempDutyExchange = null;
            foreach(DutyExchange dutyExchange in dutyExchanges)
            {
                if(dutyExchange.DutyID == dutyID)
                {
                    tempDutyExchange = dutyExchange;
                }
            }
            return tempDutyExchange;
        }

        public static void UpdateDutyExchange(int oldDutyID, int newDutyID)
        {
            foreach(DutyExchange dutyExchange in dutyExchanges)
            {
                if(dutyExchange.DutyID == oldDutyID)
                {
                    dutyExchange.DutyID = newDutyID;
                }
            }
        }

        public static bool DutyExchangeExist(DutyExchange dutyExchange)
        {
            bool exist = false;
            foreach (DutyExchange dutyExchange2 in dutyExchanges)
            {
                if (dutyExchange2.DutyID == dutyExchange.DutyID && dutyExchange2.EmployeeID == dutyExchange.EmployeeID)
                {
                    if (dutyExchange2.DutyExchangeID != dutyExchange.DutyExchangeID)
                    {
                        dutyExchange2.DutyExchangeID = dutyExchange.DutyExchangeID;
                    }
                    exist = true;
                }
            }
            return exist;
        }

        public static List<DutyExchange> GetDutyExchanges()
        {
            return dutyExchanges;
        }

        public static void RemoveDutyExchange(int dutyID)
        {
            try
            {
                foreach (DutyExchange dutyExchange2 in dutyExchanges)
                {
                    if (dutyExchange2.DutyID == dutyID)
                    {
                        dutyExchanges.Remove(dutyExchange2);
                    }
                }
            }
            catch (Exception) { }
        }
    }
}
