using Microsoft.Win32;
using Restanko.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Provider;
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
    /// Логика взаимодействия для AddOrEditMachineWindow.xaml
    /// </summary>
    public partial class AddOrEditMachineWindow : Window
    {
        private Machine Machine { get; set; }
        private string photoMachine;
        string mainPhotosPath = Environment.CurrentDirectory + "/MachineImage/";

        public AddOrEditMachineWindow(Machine machine)
        {
            InitializeComponent();
            Machine = machine;
            Mark_Combobox.ItemsSource = RestankoContext.restankoContext.Marks.ToList();
            MachineType_Combobox.ItemsSource = RestankoContext.restankoContext.Machinetypes.ToList();
            var years = Enumerable.Range(2000, int.Parse(DateTime.Now.ToString("yy")) + 1);
            foreach(var year in years)
            {
                YearOfManufacture_Combobox.Items.Add(year);
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Machine != null)
            {
                AddOrEditImage_Bitton.Content = "Изменить";
                AddOrEditMachine_Button.Content = "Изменить станок";
                Mark_Combobox.SelectedItem = Machine.Mark;
                NameMachine_Textbox.Text = Machine.Name;
                MachineType_Combobox.SelectedItem = Machine.MachineType;
                YearOfManufacture_Combobox.SelectedItem = Machine.YearOfManufacture;
                if(!string.IsNullOrEmpty(Machine.Image))
                {
                    Machine_Image.Source = new BitmapImage(new Uri(mainPhotosPath + Machine.Image));
                }
                else
                {
                    Machine_Image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/defaultImage.jpg"));
                }
            }
            else
            {
                Mark_Combobox.SelectedIndex= 0;
                MachineType_Combobox.SelectedIndex = 0;
                YearOfManufacture_Combobox.SelectedIndex = 0;
                RemoveMachine_Button.Visibility = Visibility.Hidden;
                Machine_Image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/defaultImage.jpg"));
            }
            
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddOrEditMachine_Button_Click(object sender, RoutedEventArgs e)
        {
            if(AddOrEditMachine_Button.Content.ToString() == "Добавить cтанок")
            {
                if (!string.IsNullOrEmpty(NameMachine_Textbox.Text))
                {
                    Machine = new Machine();
                    Machine.Mark = (Mark)Mark_Combobox.SelectedItem;
                    Machine.MachineType = (Machinetype)MachineType_Combobox.SelectedItem;
                    Machine.Name = NameMachine_Textbox.Text;
                    Machine.YearOfManufacture = (int)YearOfManufacture_Combobox.SelectedItem;
                    RestankoContext.restankoContext.Add(Machine);
                    RestankoContext.restankoContext.SaveChanges();
                    if(photoMachine != null)
                    {
                        string img = System.IO.Path.GetFileName(photoMachine);
                        File.Copy(photoMachine, mainPhotosPath + img, true);
                        Machine.Image = img;
                    }
                    Close();
                }
            }
            else
            {
                if (photoMachine != null)
                {
                    string img = System.IO.Path.GetFileName(photoMachine);
                    File.Copy(photoMachine, mainPhotosPath + img, true);
                    Machine.Image = img;
                }
                Machine.Mark = (Mark)Mark_Combobox.SelectedItem;
                Machine.MachineType = (Machinetype)MachineType_Combobox.SelectedItem;
                Machine.Name = NameMachine_Textbox.Text;
                Machine.YearOfManufacture = (int)YearOfManufacture_Combobox.SelectedItem;
                RestankoContext.restankoContext.SaveChanges();
                Close();
            }
        }

        private void RemoveMachine_Button_Click(object sender, RoutedEventArgs e)
        {
            Repair repair = RestankoContext.restankoContext.Repairs.FirstOrDefault(r => r.Machine == Machine);
            if(repair == null)
            {
                var msg = MessageBox.Show("Вы действительно хотите удалить станок?", "Уведомление", MessageBoxButton.YesNo);
                if (msg == MessageBoxResult.Yes)
                {
                    RestankoContext.restankoContext.Remove(Machine);
                    RestankoContext.restankoContext.SaveChanges();
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Станок содержится в заказе на ремонт", "Уведомление");
            }
        }

        private void AddOrEditImage_Bitton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Png files *.png| *.png*;|Jpg files *.jpg| *.jpg*;| Webp files *.webp | *.webp*";
            if(ofd.ShowDialog() == true)
            {
                photoMachine = ofd.FileName;
                Machine_Image.Source = new BitmapImage(new Uri(photoMachine));
            }    
        }
    }
}
