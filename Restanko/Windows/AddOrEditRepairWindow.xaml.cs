using Restanko.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Restanko.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddOrEditRepairWindow.xaml
    /// </summary>
    public partial class AddOrEditRepairWindow : Window
    {
        private Repair Repair { get; set; }

        private Repairtype currentRepairType { get; set; }


        private DateOnly date = new DateOnly();

        private DateOnly dateEnd = new DateOnly();

        public AddOrEditRepairWindow(Repair repair, User user)
        {
            InitializeComponent();
            Repair = repair;
            Machine_Combobox.Items.Clear();
            Machine_Combobox.ItemsSource = RestankoContext.restankoContext.Machines.ToList();
            RepairType_Combobox.Items.Clear();
            RepairType_Combobox.ItemsSource = RestankoContext.restankoContext.Repairtypes.ToList();
            if(user != null )
            {
                if(user.RoleId != 1)
                {
                    DeleteRepair_Button.Visibility = Visibility.Hidden;
                }
            }
            if (Repair != null)
            {
                AddOrEditRepair_Button.Content = "Изменить";
                Machine_Combobox.SelectedItem = Repair.Machine;
                RepairType_Combobox.SelectedItem = Repair.RepairType;
                date = Repair.DateOfRepair;
                DateOfRepair_Label.Content = date;
            }
            else
            {
                DeleteRepair_Button.Visibility = Visibility.Hidden;
                int year = Int32.Parse(DateTime.Now.ToString("yyyy"));
                int month = Int32.Parse(DateTime.Now.ToString("MM"));
                int day = Int32.Parse(DateTime.Now.ToString("dd"));
                date = new DateOnly(year, month, day);
                DateOfRepair_Label.Content = date;
            }
            UpdateRepair();
        }

        private void UpdateRepair()
        {          
            Duration_Label.Content = currentRepairType.Duration + " дн(я/ей)";
            dateEnd = date.AddDays(currentRepairType.Duration);
            DateEndofRepair_Label.Content = dateEnd;
        }

        private void DeleteRepair_Button_Click(object sender, RoutedEventArgs e)
        {

            var msg = MessageBox.Show("Вы действительно хотите удалить заказ на ремонт", "Уведомление", MessageBoxButton.YesNo);
            if (msg == MessageBoxResult.Yes)
            {
                RestankoContext.restankoContext.Remove(Repair);
                RestankoContext.restankoContext.SaveChanges();
                Close();
            }

        }

        private void Exit_button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void RepairType_Combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentRepairType = (Repairtype)RepairType_Combobox.SelectedItem;
            UpdateRepair();
        }

        private void AddOrEditRepair_Button_Click(object sender, RoutedEventArgs e)
        {
            if(AddOrEditRepair_Button.Content.ToString() == "Добавить")
            {
                Repair = new Repair();
                Repair.RepairType = currentRepairType;
                Repair.Machine = (Machine)Machine_Combobox.SelectedItem;
                Repair.DateOfRepair = date;
                Repair.DateEndOfRepair = dateEnd;
                RestankoContext.restankoContext.Add(Repair);
                RestankoContext.restankoContext.SaveChanges();
                MessageBox.Show("Успешно добавлено", "Уведомление");
                Close();
            }
            else
            {
                Repair.RepairType = currentRepairType;
                Repair.Machine = (Machine)Machine_Combobox.SelectedItem;
                Repair.DateEndOfRepair = dateEnd;
                RestankoContext.restankoContext.SaveChanges();
                MessageBox.Show("Успшено изменено", "Уведомление");
                Close();
            }
        }
    }
}
