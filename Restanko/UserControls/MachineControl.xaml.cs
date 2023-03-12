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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Restanko.UserControls
{
    /// <summary>
    /// Логика взаимодействия для MachineControl.xaml
    /// </summary>
    public partial class MachineControl : UserControl
    {
        public Machine Machine { get; set; }
        string mainPhotosPath = Environment.CurrentDirectory + "/MachineImage/";
        public MachineControl(Machine machine)
        {
            InitializeComponent();
            Machine = machine;
            Id_Label.Content = "Id:" + Machine.Id;
            Mark_Label.Content = Machine.Mark.Name;
            NameMachine_Label.Content = Machine.Name; 
            MachineType_Label.Content = Machine.MachineType.Name;
            YearOfManufacture_Label.Content = Machine.YearOfManufacture;
            if (Machine.Image!= null)
            {
                Machine_Image.Source = new BitmapImage(new Uri(mainPhotosPath + Machine.Image)); 
            }
            else
            {
                Machine_Image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/defaultImage.jpg"));
            }
        }
    }
}
