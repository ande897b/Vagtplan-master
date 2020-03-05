using Application.DatabaseControllers;
using Application.Repositories;
using Domain.Models;
using Model.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows;

namespace UI.Views
{
    public partial class PopupExchangeDutyWindow : Window
    {
        DutyListView DutyExchangeListView { get; set; }
        public static PopupExchangeDutyWindow PopupExchangeDutyWindowInstance { get; set; }

        public PopupExchangeDutyWindow(DutyListView dutyListView)
        {
            InitializeComponent();
            PopupExchangeDutyWindowInstance = this;
            DutyExchangeListView = dutyListView;
            UpdateEmployeeCB();
            DutyIDLabel.Content = DutyExchangeListView.Duty.DutyID;
            EmployeeLabel.Content = EmployeeRepository.GetEmployeeName(DutyExchangeListView.Duty.EmployeeID);
            StartTimeLabel.Content = DutyExchangeListView.Duty.StartTime;
            EndTimeLabel.Content = DutyExchangeListView.Duty.EndTime;
            if (EmployeeCB.SelectedIndex == -1)
                Confirm_Btn.IsEnabled = false;
            this.Closing += WindowClosed;
        }

        private void WindowClosed(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            DBDutyController.LoadDuties();
            DBDutyExchangeController.LoadDutyExchanges();
            ExchangeDutyWindow.ExchangeDutyWindowInstance.DutyExchangeListView.ItemsSource = null;
            ExchangeDutyWindow.ExchangeDutyWindowInstance.UpdateDutyExchangeList();
            ExchangeDutyWindow.ExchangeDutyWindowInstance.Show();
            e.Cancel = false;
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

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            int newEmployeeID = EmployeeRepository.GetEmployeeID(EmployeeCB.SelectedValue.ToString());
            int oldEmployeeID = EmployeeRepository.GetEmployeeID(EmployeeRepository.GetEmployeeName(DutyExchangeListView.Duty.EmployeeID));
            try
            {
                DBDutyController.UpdateDuty(newEmployeeID, DutyExchangeListView.Duty.DutyID);
            }
            catch(Exception t)
            {
                MessageBox.Show(t.Message);
            }
            DBDutyExchangeController.DeleteDutyExchange(DutyExchangeListView.Duty.DutyID);
            DutyExchangeRepository.RemoveDutyExchange(DutyExchangeListView.Duty.DutyID);
            this.Close();
        }

        private void Regret_Btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            ExchangeDutyWindow.ExchangeDutyWindowInstance.DutyExchangeListView.SelectedIndex = -1;
            ExchangeDutyWindow.ExchangeDutyWindowInstance.DutyListView.SelectedIndex = -1;
            ExchangeDutyWindow.ExchangeDutyWindowInstance.Show();
        }

        private void EmployeeCB_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Confirm_Btn.IsEnabled = true;
        }
    }
}
