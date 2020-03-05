using Application.Repositories;
using Domain.Models;
using Model.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;


namespace UI.Views
{
    public partial class ShowMyDutiesWindow : Window
    {
        public Employee Employee { get; private set; }
        public ShowMyDutiesWindow(int employeeID)
        {
            InitializeComponent();
            Employee = EmployeeRepository.GetEmployee(employeeID);

            List<Duty> duties = DutyRepository.GetDuties(Employee.FirstName);
            List<DutyListView> dutyListViews = new List<DutyListView>();
            foreach (Duty duty in duties)
            {
                dutyListViews.Add(new DutyListView
                {
                    Duty = duty,
                    EmployeeName = EmployeeRepository.GetEmployeeName(duty.EmployeeID)
                });
            }
            DutyListView.ItemsSource = dutyListViews;
            this.Closing += WindowClosed;
        }

        private void WindowClosed(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            MenuWindow.MenuWindowInstance.Show();
            e.Cancel = false;
        }
    }
}
