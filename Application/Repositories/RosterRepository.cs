using System;
using System.Collections.Generic;
using Application.DatabaseControllers;
using Domain.Models;

namespace Application.Repositories
{
	public static class RosterRepository
	{
		private static List<Roster> rosters = new List<Roster>();
		public static void AddRoster(Roster roster)
		{
			if (!RosterExist(roster))
				rosters.Add(roster);
		}

		public static bool RosterExist(Roster roster)
		{
			bool exist = false;
			foreach (Roster roster2 in rosters)
			{
				if (roster2.Shop == roster.Shop && roster2.StartDate == roster.StartDate && roster2.EndDate == roster.EndDate)
				{
					if (roster2.RosterID != roster.RosterID)
					{
						roster2.RosterID = roster.RosterID;
					}
					exist = true;
				}
			}
			return exist;
		}

		public static DateTime GetEndDate(string shop)
		{
			DateTime endDate = DateTime.Now;
			int i = rosters.Count;
			while (endDate == DateTime.Now)
			{
				if (rosters[i-1].Shop.ToString().ToLower() == shop)
				{
					endDate = rosters[i-1].EndDate;
				}
				i--;
			}
			return endDate;
		}

		public static bool CurrentRosterExist(string shop)
		{
			bool exists = false;
			foreach (Roster roster in rosters)
			{
				if (roster.Shop.ToString().ToLower() == shop)
				{
					exists = true;
				}
			}
			return exists;
		}

		public static void CreateRoster(DateTime startDate, DateTime endDate, string shop)
		{
			Shop newShop;
			if (shop == "kongensgade")
			{
				newShop = Shop.kongensgade;
			}
			else
			{
				newShop = Shop.skibhusvej;
			}
			Roster roster = new Roster(startDate, endDate, newShop);
			DBRosterController.CreateRoster(roster);
			int rosterID = DBRosterController.GetRosterID(roster);
			roster.RosterID = rosterID;
			DBDateController.CreateDates(roster);
		}


	}
}