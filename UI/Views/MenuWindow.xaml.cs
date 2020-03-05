using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Application.DatabaseControllers;
using Application.Repositories;
using Domain.Models;

namespace UI.Views
{
    public partial class MenuWindow : Window
    {
        public static MenuWindow MenuWindowInstance { get; set; }

        public MenuWindow()
        {
            InitializeComponent();
            MenuWindowInstance = this;
            if (ShopCB.SelectedIndex == -1)
            {
                ShowRostersBtn.IsEnabled = false;
            }
            if (EmployeeCombobox.SelectedIndex == -1)
            {
                ShowMyDutiesBtn.IsEnabled = false;
            }
            UpdateEmployeeCB();
            this.Closing += WindowClosed;
        }

        private void WindowClosed(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            MainWindow.MainWindowInstance.Show();
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
            EmployeeCombobox.ItemsSource = newEmployees;
        }

        private void CreateRosterBtn_Click(object sender, RoutedEventArgs e)
        {
            CreateRosterWindow createRosterWindow = new CreateRosterWindow();
            createRosterWindow.Show();
            this.Hide();
        }

        private void ShowRostersBtn_Click(object sender, RoutedEventArgs e)
        {
            DateTime now = DateTime.Now;
            int month = now.Month;
            int week = 0;
            int day = now.Day;
            if (day >= 1)
                week = 0;
            if (day >= 8)
                week = 1;
            if (day >= 15)
                week = 2;
            if (day >= 23)
                week = 3;
            if (day >= 30)
                week = 4;
            ShowRosterWindow showRosterWindow = new ShowRosterWindow(ShopCB.Text);
            showRosterWindow.tabControl.SelectedIndex = month - 1;
            switch (month)
            {
                case 1:
                    showRosterWindow.JanuaryTabControl.SelectedIndex = week;
                    break;
                case 2:
                    showRosterWindow.FebruaryTabControl.SelectedIndex = week;
                    break;
                case 3:
                    showRosterWindow.MarchTabControl.SelectedIndex = week;
                    break;
                case 4:
                    showRosterWindow.AprilTabControl.SelectedIndex = week;
                    break;
                case 5:
                    showRosterWindow.MayTabControl.SelectedIndex = week;
                    break;
                case 6:
                    showRosterWindow.JuneTabControl.SelectedIndex = week;
                    break;
                case 7:
                    showRosterWindow.JulyTabControl.SelectedIndex = week;
                    break;
                case 8:
                    showRosterWindow.AugustTabControl.SelectedIndex = week;
                    break;
                case 9:
                    showRosterWindow.SeptemberTabControl.SelectedIndex = week;
                    break;
                case 10:
                    showRosterWindow.OctoberTabControl.SelectedIndex = week;
                    break;
                case 11:
                    showRosterWindow.NovemberTabControl.SelectedIndex = week;
                    break;
                case 12:
                    showRosterWindow.DecemberTabControl.SelectedIndex = week;
                    break;
            }
            showRosterWindow.Show();
            this.Hide();
        }

        private void LogOutBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
        }

        private void ShopCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ShopCB.SelectedIndex != -1)
            {
                ShowRostersBtn.IsEnabled = true;
            }
        }

        private void WishForDayOff_Click(object sender, RoutedEventArgs e)
        {
            WishForDayOffWindow dayOff = new WishForDayOffWindow();
            List<Employee> employees = EmployeeRepository.GetEmployees();
            List<string> newEmployees = new List<string>();
            foreach (Employee employee in employees)
            {
                string newEmployee = employee.FirstName;
                newEmployees.Add(newEmployee);
            }
            dayOff.WishForDayOffCB.ItemsSource = newEmployees;
            dayOff.Show();
            this.Hide();
        }

        private void ExchangeDuty_Click(object sender, RoutedEventArgs e)
        {
            ExchangeDutyWindow exchangeDutyWindow = new ExchangeDutyWindow();
            exchangeDutyWindow.Show();
            this.Hide();
        }

        private void ShowMyDuties_Click(object sender, RoutedEventArgs e)
        {
            ShowMyDutiesWindow showMyDutiesWindow = new ShowMyDutiesWindow(EmployeeRepository.GetEmployeeID(EmployeeCombobox.SelectedValue.ToString()));
            showMyDutiesWindow.Show();
            this.Hide();
        }

        private void EmployeeCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowMyDutiesBtn.IsEnabled = true;
        }

        private void CreateEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            CreateEmployeeWindow createEmployeeWindow = new CreateEmployeeWindow();
            createEmployeeWindow.Show();
            this.Hide();
        }

        private void DeleteEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            DeleteEmployeeWindow deleteEmployeeWindow = new DeleteEmployeeWindow();
            deleteEmployeeWindow.Show();
            this.Hide();
        }
    }
}
