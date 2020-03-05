using Application.DatabaseControllers;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Repositories
{
    public static class DutyRepository
    {
        private static List<Duty> duties = new List<Duty>();
        public static void AddDuty(Duty duty)
        {
            if (!DutyExist(duty))
                duties.Add(duty);
        }

        public static Duty GetDuty(string date, string firstName)
        {
            Duty newDuty = null;
            foreach (Duty duty in duties)
            {
                string duty2 = duty.StartTime.ToString().Substring(0, 10);
                string firstName2 = EmployeeRepository.GetEmployee(duty.EmployeeID).FirstName;
                if (duty2 == date.Substring(0, 10) && firstName2 == firstName)
                {
                    newDuty = duty;
                }
            }
            return newDuty;
        }

        public static Duty GetDuty(int dutyID)
        {
            Duty newDuty = null;
            foreach (Duty duty in duties)
            {
                if (duty.DutyID == dutyID)
                {
                    newDuty = duty;
                }
            }
            return newDuty;
        }

        public static List<Duty> GetDuties(int dateID)
        {
            List<Duty> newDuties = new List<Duty>();
            foreach(Duty duty in duties)
            {
                if(duty.DateID == dateID)
                {
                    newDuties.Add(duty);
                }
            }
            return newDuties;
        }

        public static List<Duty> GetDuties(string firstName)
        {
            List<Duty> newDuties = new List<Duty>();
            int employeeID = EmployeeRepository.GetEmployeeID(firstName);
            foreach (Duty duty in duties)
            {
                if (duty.EmployeeID == employeeID)
                {
                    newDuties.Add(duty);
                }
            }
            return newDuties;
        }

        public static void RemoveDuties(int dateID)
        {
            
            foreach (Duty duty in duties.ToList()) 
            {
                if (duty.DateID == dateID)
                {

                    duties.Remove(duty);
                }
            }
            
            
        }

        public static void Removeduties_EmpID(int employeeID)
        {
            foreach (Duty duty in duties.ToList())
            {
                if (duty.EmployeeID == employeeID)
                {
                    duties.Remove(duty);
                }
            }
        }

        public static bool DutyExist(Duty duty)
        {
            bool exist = false;
            foreach(Duty duti in duties)
            {
                if (duti.DateID == duty.DateID && duti.EmployeeID == duty.EmployeeID && duti.StartTime == duty.StartTime && duti.EndTime == duty.EndTime)
                {
                    if (duti.DutyID != duty.DutyID)
                    {
                        
                        duti.DutyID = duty.DutyID;
                        
                    }
                    exist = true;
                    DutyExchangeRepository.UpdateDutyExchange(duti.DutyID, duty.DutyID);
                }

            }
            return exist;
        }

        public static void UpdateDuty(int newEmployeeID, int dutyID)
        {
            foreach(Duty duty in duties)
            {
                if(duty.DutyID == dutyID)
                {
                    duty.EmployeeID = newEmployeeID;
                }
            }
        }
    }
}
