using Restanko.Entities;
using System;
using System.Collections.Generic;
using System.IO.Packaging;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Restanko.UserControls
{
    /// <summary>
    /// Логика взаимодействия для RepairControl.xaml
    /// </summary>
    public partial class RepairControl : UserControl
    {
        public Repair Repair { get; set; }
        string mainPhotosPath = Environment.CurrentDirectory + "/MachineImage/";
        public RepairControl(Repair repair)
        {
            InitializeComponent();
            Repair = repair;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            IdOrder_Label.Content = "№ заказа: " + Repair.Id;
            MachineName_Label.Content = Repair.Machine.Mark.Name + " " + Repair.Machine.Name;
            YearMachine_Label.Content = Repair.Machine.YearOfManufacture + " Год";
            RepairName_Label.Content = Repair.RepairType.Name;
            CostRepair_Label.Content = Repair.RepairType.Cost;
            DateRepair_Label.Content = Repair.DateOfRepair;
            DateEndRepair_Label.Content = Repair.DateEndOfRepair;
            if(Repair.Machine.Image != null)
            {
                Machine_Image.Source = new BitmapImage(new Uri(mainPhotosPath + Repair.Machine.Image));
            }
            else
            {
                Machine_Image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/defaultImage.jpg"));
            }

        }
    }
}
