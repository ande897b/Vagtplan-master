using Application.DatabaseControllers;
using System.ComponentModel;
using System.Windows;
using Domain.Models;
using System.Collections.Generic;
using Application.Repositories;
using System;

namespace UI.Views
{
    public partial class CreateEmployeeWindow : Window
    {
        public static CreateEmployeeWindow CreateEmployeeWindowInstance { get; set; }

        public CreateEmployeeWindow()
        {
            InitializeComponent();
            CreateEmployeeWindowInstance = this;
            RankCB.ItemsSource = new List<string>
            {
                "Deltidsmedarbejder",
                "Butikschef"
            };
            CreateEmployeeBtn.IsEnabled = false;
            FirstNameTB.GotFocus += FirstNameTBGotFocus;
            LastNameTB.GotFocus += LastNameTBGotFocus;
            this.Closing += WindowClosed;
        }

        private void WindowClosed(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            DBEmployeeController.LoadEmployees();
            MenuWindow.MenuWindowInstance.UpdateEmployeeCB();
            MenuWindow.MenuWindowInstance.Show();
            e.Cancel = false;
        }

        private void LastNameTBGotFocus(object sender, RoutedEventArgs e)
        {
            LastNameTB.Clear();
        }

        private void FirstNameTBGotFocus(object sender, RoutedEventArgs e)
        {
            FirstNameTB.Clear();
        }

        private bool isCreateBtnActivated()
        {
            bool isActivated = false;
            if(FirstNameTB.Text != "" && FirstNameTB.Text != "Fornavn" && LastNameTB.Text != "" && LastNameTB.Text != "Efternavn" && RankCB.SelectedIndex != -1)
            {
                CreateEmployeeBtn.IsEnabled = true;
                isActivated = true;
            }
            return isActivated;
        }

        private void CreateEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            Rank rank;
            if (RankCB.SelectedValue.ToString() == "Deltidsmedarbejder")
            {
                rank = Rank.parttimer;
            }
            else
            {
                rank = Rank.manager;
            }
            DBEmployeeController.CreateEmployee(new Employee(FirstNameTB.Text, LastNameTB.Text, rank));
            this.Close();
        }

        private void FirstNameTB_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            isCreateBtnActivated();
        }

        private void LastNameTB_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            isCreateBtnActivated();
        }

        private void RankCB_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            isCreateBtnActivated();
        }
    }
}
