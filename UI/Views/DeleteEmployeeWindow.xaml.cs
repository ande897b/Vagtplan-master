using Application.DatabaseControllers;
using Application.Repositories;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace UI.Views
{
    public partial class DeleteEmployeeWindow : Window
    {
        public static DeleteEmployeeWindow DeleteEmployeeWindowInstance { get; set; }

        public DeleteEmployeeWindow()
        {
            InitializeComponent();
            DeleteEmployeeWindowInstance = this;
            EmployeeListView.ItemsSource = EmployeeRepository.GetEmployees();
            this.Closing += WindowClosed;
            EmployeeListView.SelectionChanged += ListViewSelectionChanged;
        }

        private void ListViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EmployeeListView.ItemsSource = null;
            EmployeeListView.ItemsSource = EmployeeRepository.GetEmployees();
        }

        private void WindowClosed(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            DBEmployeeController.LoadEmployees();
            MenuWindow.MenuWindowInstance.Show();
            e.Cancel = false;
        }

        private void EmployeeListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if(EmployeeListView.SelectedIndex != -1)
            {
                MessageBoxButton btn = MessageBoxButton.YesNo;
                MessageBoxImage image = MessageBoxImage.Exclamation;
                MessageBoxResult result = MessageBox.Show("Er du sikker på at du vil slette denne medarbejder fra systemet?", "Slet medarbejder", btn, image);
                if (result == MessageBoxResult.Yes)
                {
                    DBEmployeeController.DeleteEmployee(((Employee)EmployeeListView.SelectedItem).EmployeeID);
                    EmployeeListView.SelectedIndex = -1;
                }
                else if (result == MessageBoxResult.No)
                {
                    EmployeeListView.SelectedIndex = -1;
                }
            }
        }
    }
}
