using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Application.DatabaseControllers;
using Application.Repositories;

namespace UI.Views
{
    public partial class CreateRosterWindow : Window
    {
        public static CreateRosterWindow CreateRosterWindowInstance { get; set; }
        public CreateRosterWindow()
        {
            InitializeComponent();
            CreateRosterWindowInstance = this;
            CreatRosterbtn.IsEnabled = false;
            DatePickerEnd.IsEnabled = false;
            this.Closing += WindowClosed;
            DatePickerEnd.SelectedDateChanged += SelectedDateChanged;
        }

        private void WindowClosed(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            DBRosterController.LoadRosters();
            DBDateController.LoadDates();
            MenuWindow.MenuWindowInstance.Show();
            e.Cancel = false;
        }

        private void SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            int dateCompare = DateTime.Compare((DateTime)DatePickerEnd.SelectedDate, DateTime.Now);
            int dateCompare2 = DateTime.Compare((DateTime)DatePickerEnd.SelectedDate, (DateTime)DatePickerStart.SelectedDate);
            if(dateCompare > 0 && dateCompare2 > 0)
            {
                CreatRosterbtn.IsEnabled = true;
            }
        }

        private void CreateRosterBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RosterRepository.CreateRoster(DatePickerStart.SelectedDate.Value, DatePickerEnd.SelectedDate.Value, comboBoxShop.Text.ToString().ToLower());
                MessageBox.Show("Vagtplan i " + comboBoxShop.Text + " oprettet. Du kan nu indsætte vagter", "Success");
            }
            catch(Exception r)
            {
                MessageBox.Show($"{r.Message}\nDu skal udfylde alle felter");
            }
            this.Close();
        }

        private void ComboBoxShop_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string shop = (comboBoxShop.SelectedItem as ComboBoxItem).Content.ToString().ToLower();
            DateTime startDate;
            if (RosterRepository.CurrentRosterExist(shop) == false)
            {
                startDate = DateTime.Now;
            }
            else
            {
                startDate = RosterRepository.GetEndDate(shop);
            }
            DatePickerEnd.IsEnabled = true;
            DatePickerStart.SelectedDate = startDate;
            DatePickerEnd.SelectedDate = DatePickerStart.SelectedDate;
        }
    }
}

