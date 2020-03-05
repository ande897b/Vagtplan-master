using Application.DatabaseControllers;
using Application.Repositories;
using Domain.Models;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace UI.Views
{
    public partial class WishForDayOffWindow : Window
    {
        public static WishForDayOffWindow WishForDayOffWindowInstance { get; set; }

        public WishForDayOffWindow()
        {
            InitializeComponent();
            WishForDayOffWindowInstance = this;
            WishForDayOffBtn.IsEnabled = false;
            WishForDayOffDP.SelectedDateChanged += SelectedDateChanged;
            this.Closing += WindowClosed;
        }

        private void SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            int dateCompare = DateTime.Compare((DateTime)WishForDayOffDP.SelectedDate, DateTime.Now);
            if(dateCompare > 0)
            isWishForDayOffBtnActivated();
        }

        private void WindowClosed(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            DBWishForDayOffController.LoadWishForDayOffs();
            MenuWindow.MenuWindowInstance.Show();
            e.Cancel = false;
        }

        private bool isWishForDayOffBtnActivated()
        {
            bool isActivated = false;
            
            if (WishForDayOffCB.SelectedIndex != -1 && WishForDayOffDP.SelectedDate != null)
            {
                WishForDayOffBtn.IsEnabled = true;
                isActivated = true;
            }
            return isActivated;
        }

        private void WishForDayOffBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxButton btn = MessageBoxButton.YesNo;
            MessageBoxResult result = MessageBox.Show($"Er du sikker på at du vil ønske fri på datoen: {WishForDayOffDP.SelectedDate.ToString().Substring(0,10)}?", "ønsk fri", btn);
            if(result == MessageBoxResult.Yes)
            {
                int employeeID = EmployeeRepository.GetEmployeeID(WishForDayOffCB.SelectedItem.ToString());
                DateTime date = WishForDayOffDP.SelectedDate.Value;
                WishForDayOff newWish = new WishForDayOff(employeeID, date);
                DBWishForDayOffController.CreateWishForDayOff(newWish);
                this.Close();
            }
        }

        private void WishForDayOffCB_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            isWishForDayOffBtnActivated();
        }
    }
}
