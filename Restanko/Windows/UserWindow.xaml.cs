using Restanko.Entities;
using Restanko.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        private static List<Repair> displayRepair = new List<Repair>();

        private Repairtype currentRepairType { get; set; }

        private Repair currentRepair { get; set; }

        private DateOnly date;

        public User User { get; set; }

        public UserWindow(User user)
        {
            InitializeComponent();
            if (user != null)
            {
                User = user;
                FIO_Label.Content = string.Join(" ", user.Surname, user.Name, user.Patryonomic);
                if(user.RoleId != 1)
                {
                    Machine_Button.Visibility = Visibility.Hidden;
                    RepairType_Button.Visibility = Visibility.Hidden;
                    Grid.SetColumn(FIO_Label, 2);
                }
                if(user.RoleId != 2)
                {
                    ReportPDF_Button.Visibility = Visibility.Hidden;
                }
            }
            UpdateTypeRepair();
            Sort_Combobox.Items.Add("По релевантности");
            Sort_Combobox.Items.Add("По умолчанию");
            Filter_Combobox.Items.Add("Не завершенные");
            Filter_Combobox.Items.Add("Завершенные");
            Filter_Combobox.Items.Add("Все");
            int year = Int32.Parse(DateTime.Now.ToString("yyyy"));
            int month = Int32.Parse(DateTime.Now.ToString("MM"));
            int day = Int32.Parse(DateTime.Now.ToString("dd"));
            date = new DateOnly(year, month, day);
            UpdateRepair();
        }

        private void UpdateRepair()
        {
            Repair_ListView.Items.Clear();
            displayRepair.Clear();
            displayRepair = RestankoContext.restankoContext.Repairs.ToList();
            if(displayRepair.Count > 0)
            {
                if(!string.IsNullOrEmpty(Search_TextBox.Text))
                {
                    displayRepair = displayRepair.Where(p => p.Machine.Name.ToLower().Contains(Search_TextBox.Text.ToLower()) || p.RepairType.Name.ToLower().Contains(Search_TextBox.Text.ToLower()) || p.Machine.Mark.Name.ToLower().Contains(Search_TextBox.Text.ToLower())).ToList();
                }
                switch (Sort_Combobox.SelectedIndex)
                {
                    case 0:
                        displayRepair = displayRepair.OrderByDescending(r => r.Id).ToList();
                        break;
                    case 1:
                        displayRepair = displayRepair.OrderBy(r => r.Id).ToList();
                        break;                   
                }
                switch (Filter_Combobox.SelectedIndex)
                {
                    case 0:
                        displayRepair = displayRepair.Where(p => p.DateEndOfRepair >= date).ToList();
                        break;
                    case 1:
                        displayRepair = displayRepair.Where(p => p.DateEndOfRepair < date).ToList();
                        break;
                }
                NotFound_Label.Visibility = Visibility.Hidden;
                foreach(Repair repair in displayRepair)
                {
                    Repair_ListView.Items.Add(new RepairControl(repair) {Width = GetNormalWidth() });
                }
                CountDisplay_Label.Content = displayRepair.Count + " из " + RestankoContext.restankoContext.Repairs.Count();
            }
            if(displayRepair.Count < 1)
            {
                NotFound_Label.Visibility = Visibility.Visible;
            }


        }

        private double GetNormalWidth()
        {
            if(WindowState == WindowState.Maximized)
            {
                return RenderSize.Width - 50;
            }
            else
            {
                return Width - 50;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Owner.Show();
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddRepair_Button_Click(object sender, RoutedEventArgs e)
        {
            var aoerw = new AddOrEditRepairWindow(null, User).ShowDialog();
            UpdateRepair();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            foreach(RepairControl item in Repair_ListView.Items)
            {
                item.Width = GetNormalWidth();
            }
        }

        private void Repair_ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Repair_ListView.SelectedItem != null)
            {
                currentRepair = ((RepairControl)Repair_ListView.SelectedItem).Repair;
            }
        }

        private void Repair_ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(currentRepair != null)
            {
                var aoerw = new AddOrEditRepairWindow(currentRepair, User).ShowDialog();                
                UpdateRepair();
            }
        }

        private void Machine_Button_Click(object sender, RoutedEventArgs e)
        {
            var mw = new MachineWindow().ShowDialog();
            UpdateRepair();
        }

        private void Sort_Combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateRepair();
        }

        private void Filter_Combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateRepair();
        }

        private void UpdateTypeRepair()
        {
            TypeRapair_Combobox.ItemsSource = RestankoContext.restankoContext.Repairtypes.ToList();
        }

        private void TypeRapair_Combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(TypeRapair_Combobox.IsEnabled == true)
            {
                if (TypeRapair_Combobox.SelectedItem != null)
                {
                    currentRepairType = (Repairtype)TypeRapair_Combobox.SelectedItem;
                    AddTypeRepair_Button.IsEnabled = false;
                    RepairTypeName_Textbox.Text = currentRepairType.Name;
                    CostRepairType_Textbox.Text = currentRepairType.Cost.ToString();
                    DurationRepairType_Textbox.Text = currentRepairType.Duration.ToString();
                    UpdateTypeRepair();
                }
            }
        }

        private void AddTypeRepair_Button_Click(object sender, RoutedEventArgs e)
        {
            Repairtype repairtype = RestankoContext.restankoContext.Repairtypes.FirstOrDefault(rt => rt.Name == RepairTypeName_Textbox.Text);
            if (repairtype == null)
            {
                if (!string.IsNullOrEmpty(RepairTypeName_Textbox.Text)
                && !string.IsNullOrEmpty(CostRepairType_Textbox.Text) && !string.IsNullOrEmpty(DurationRepairType_Textbox.Text))
                {
                    if (TryInt(CostRepairType_Textbox.Text) && TryInt(DurationRepairType_Textbox.Text))
                    {
                        repairtype = new Repairtype();
                        repairtype.Name = RepairTypeName_Textbox.Text;
                        repairtype.Cost = int.Parse(CostRepairType_Textbox.Text);
                        repairtype.Duration = int.Parse(DurationRepairType_Textbox.Text);
                        RestankoContext.restankoContext.Add(repairtype);
                        RestankoContext.restankoContext.SaveChanges();
                        ClearTypeRepairTextBox();
                        UpdateTypeRepair();
                        MessageBox.Show("Вид ремонта успешно добавлен", "Уведомление");
                    }
                }
                
            }
            else
            {
                MessageBox.Show("Данный вид работы уже существует", "Уведомление");
            }
        }

        private void EditTypeRepair_Button_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(RepairTypeName_Textbox.Text)
                && !string.IsNullOrEmpty(CostRepairType_Textbox.Text) && !string.IsNullOrEmpty(DurationRepairType_Textbox.Text))
            { 
                if(TryInt(CostRepairType_Textbox.Text) && TryInt(DurationRepairType_Textbox.Text))
                {
                    currentRepairType.Name = RepairTypeName_Textbox.Text;
                    currentRepairType.Cost = int.Parse(CostRepairType_Textbox.Text);
                    currentRepairType.Duration = int.Parse(DurationRepairType_Textbox.Text);
                    RestankoContext.restankoContext.SaveChanges();
                    UpdateTypeRepair();
                    MessageBox.Show("Вид ремонта успешно изменён", "Уведомление");
                }
            }

        }

        private void RemoveTypeRepair_Button_Click(object sender, RoutedEventArgs e)
        {
            Repair repair = RestankoContext.restankoContext.Repairs.FirstOrDefault(r => r.RepairType == currentRepairType);
            if (repair == null)
            {
                var msg = MessageBox.Show("Вы дейтсвительно хотите удалить вид ремонта?", "Предупреждение", MessageBoxButton.YesNo);
                if (msg == MessageBoxResult.Yes)
                {
                    RestankoContext.restankoContext.Remove(currentRepairType);
                    RestankoContext.restankoContext.SaveChanges();
                    UpdateTypeRepair();
                    ClearTypeRepairTextBox();
                    TypeRapair_Combobox.SelectedIndex = 0;
                    MessageBox.Show("Вид ремонта успешно удалён", "Уведомление");
                }
            }
            else
            {
                MessageBox.Show("Вид заказа есть в заказах на ремонт", "Уведомление");
            }
        }

        private void TypeRapair_Combobox_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(TypeRapair_Combobox.IsEnabled)
            {
                AddTypeRepair_Button.IsEnabled = false;
                EditTypeRepair_Button.IsEnabled = true;
                RemoveTypeRepair_Button.IsEnabled = true;
                RepairTypeName_Textbox.Text = currentRepairType.Name;
                CostRepairType_Textbox.Text = currentRepairType.Cost.ToString();
                DurationRepairType_Textbox.Text = currentRepairType.Duration.ToString();
            }
            else
            {
                ClearTypeRepairTextBox();
                AddTypeRepair_Button.IsEnabled = true;
                EditTypeRepair_Button.IsEnabled = false;
                RemoveTypeRepair_Button.IsEnabled = false;               
            }
        }

        private void ClearTypeRepairTextBox()
        {
            RepairTypeName_Textbox.Text = default;
            CostRepairType_Textbox.Text = default;
            DurationRepairType_Textbox.Text = default;
        }

        private void RepairType_Button_Click(object sender, RoutedEventArgs e)
        {
            if (RepairType_Button.Content.ToString() == "Виды ремонта")
            {
                EditTypeRepair_Grid.Width = 300;
            }    
        }

        private void GoBack_Button_Click(object sender, RoutedEventArgs e)
        {
            EditTypeRepair_Grid.Width = 0;
        }

        private void ReportPDF_Button_Click(object sender, RoutedEventArgs e)
        {
            var rw = new ReportWindow(User).ShowDialog();
        }

        private void CostRepairType_Textbox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789".IndexOf(e.Text) < 0;
            
        }

        private void DurationRepairType_Textbox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789".IndexOf(e.Text) < 0;
        }

        private void Search_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateRepair();
        }

        private bool TryInt(string tryint)
        {
            int num;
            bool flag = int.TryParse(tryint, out num);
            return flag;
        }

    }
}
