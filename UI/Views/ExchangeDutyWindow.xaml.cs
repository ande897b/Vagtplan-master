using Application.DatabaseControllers;
using Application.Repositories;
using Domain.Models;
using Model.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using UI;

namespace UI.Views
{
    public partial class ExchangeDutyWindow : Window
    {
        public static ExchangeDutyWindow ExchangeDutyWindowInstance { get; set; }

        public ExchangeDutyWindow()
        {
            InitializeComponent();
            ExchangeDutyWindowInstance = this;
            UpdateEmployeeCB();
            UpdateDutyExchangeList();
            this.Closing += WindowClosed;
        }

        public void UpdateEmployeeCB()
        {
            List<string> newEmployees = new List<string>();
            List<Employee> employees = EmployeeRepository.GetEmployees();
            foreach (Employee employee in employees)
            {
                string newEmployee = employee.FirstName;
                newEmployees.Add(newEmployee);
            }
            EmployeeCB.ItemsSource = newEmployees;
        }

        private void WindowClosed(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            DBDutyController.LoadDuties();
            DBDutyExchangeController.LoadDutyExchanges();
            MenuWindow.MenuWindowInstance.Show();
            e.Cancel = false;
        }

        public void UpdateDutyList()
        {
            List<Duty> duties = DutyRepository.GetDuties(EmployeeCB.SelectedItem.ToString());
            List<DutyListView> dutyListViews = new List<DutyListView>();
            foreach (Duty duty in duties)
            {
                int dateCompare = DateTime.Compare(duty.StartTime, DateTime.Now);
                if (dateCompare > 0)
                {
                    dutyListViews.Add(new DutyListView
                    {
                        Duty = duty,
                        EmployeeName = EmployeeRepository.GetEmployeeName(duty.EmployeeID)
                    });
                }
            }
            DutyListView.ItemsSource = dutyListViews;
            DutyListView.SelectedIndex = -1;
        }

        public void UpdateDutyExchangeList()
        {
            List<DutyExchange> dutyExchanges = DutyExchangeRepository.GetDutyExchanges();
            List<DutyListView> dutyExchangeListViews = new List<DutyListView>();
            foreach (DutyExchange dutyExchange in dutyExchanges)
            {
                dutyExchangeListViews.Add(new DutyListView
                {
                    Duty = DutyRepository.GetDuty(dutyExchange.DutyID),
                    EmployeeName = EmployeeRepository.GetEmployeeName(dutyExchange.EmployeeID)
                });
            }
            DutyExchangeListView.ItemsSource = dutyExchangeListViews;
            DutyExchangeListView.SelectedIndex = -1;
        }

        private void EmployeeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateDutyList();
        }

        private void DutyListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(DutyListView.SelectedIndex != -1)
            {
                DutyListView dutyListView = (DutyListView)DutyListView.SelectedItem;
                MessageBoxButton btn = MessageBoxButton.YesNo;
                MessageBoxImage image = MessageBoxImage.Exclamation;
                MessageBoxResult result = MessageBox.Show("Er du sikker på at du vil bytte denne vagt.", "Vagt bytte", btn, image);
                if (result == MessageBoxResult.Yes)
                {
                    DutyExchange dutyExchange = new DutyExchange(dutyListView.Duty.DutyID, dutyListView.Duty.EmployeeID);
                    DBDutyExchangeController.CreateDutyExchange(dutyExchange);
                    UpdateDutyExchangeList();
                    UpdateDutyList();
                }
                else if (result == MessageBoxResult.No)
                {
                    DutyListView.SelectedIndex = -1;
                }
            }
        }

        private void DutyExchangeListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DutyExchangeListView.SelectedIndex != -1)
            {
                DutyListView dutyExchange = (DutyListView)DutyExchangeListView.SelectedItem;
                PopupExchangeDutyWindow popupExchangeDutyWindow = new PopupExchangeDutyWindow(dutyExchange);
                this.Hide();
                popupExchangeDutyWindow.Show();
            }
        }
    }
}
