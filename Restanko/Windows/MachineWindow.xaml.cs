using Restanko.Entities;
using Restanko.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Printing;
using System.Runtime.CompilerServices;
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
    /// Логика взаимодействия для MachineWindow.xaml
    /// </summary>
    public partial class MachineWindow : Window
    {
        private List<Machine> DisplayMachine { get; set; } 

        private Machine currentMachine { get; set; }

        public MachineWindow()
        {
            InitializeComponent();
            Sort_Combobox.Items.Add("Без сорировки");
            Sort_Combobox.Items.Add("По году выпуска ↓");
            Sort_Combobox.Items.Add("По году выпуска ↑");
            Filter_Combobox.Items.Add("Все марки");
            foreach(Mark mark in RestankoContext.restankoContext.Marks.ToList())
            {
                Filter_Combobox.Items.Add(mark.Name);
            }
            UpdateMachine();
        }

        private void UpdateMachine()
        {
            MachineView_ListView.Items.Clear();
            DisplayMachine = RestankoContext.restankoContext.Machines.ToList();
            if (DisplayMachine.Count > 0)
            {
                switch(Sort_Combobox.SelectedIndex)
                {
                    case 1:
                        DisplayMachine = DisplayMachine.OrderBy(m => m.YearOfManufacture).ToList();
                        break;
                    case 2:
                        DisplayMachine = DisplayMachine.OrderByDescending(m => m.YearOfManufacture).ToList();
                        break;
                }
                if(Filter_Combobox.SelectedIndex > 0)
                {
                    DisplayMachine = DisplayMachine.Where(m => m.Mark.Name == Filter_Combobox.SelectedItem).ToList();
                }
                NotFound_Label.Visibility = Visibility.Hidden;
                foreach(Machine machine in DisplayMachine)
                {
                    MachineView_ListView.Items.Add(new MachineControl(machine) { Width = GetNormalWidth() });
                }
            }
            else
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

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            foreach(MachineControl item in MachineView_ListView.Items)
            {
                item.Width = GetNormalWidth();
            }
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Sort_Combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateMachine();
        }

        private void Filter_Combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateMachine();
        }

        private void AddMachine_Button_Click(object sender, RoutedEventArgs e)
        {

            var aoem = new AddOrEditMachineWindow(null).ShowDialog();
            UpdateMachine();

        }

        private void MachineView_ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MachineView_ListView.SelectedItem != null)
            {
                currentMachine = ((MachineControl)MachineView_ListView.SelectedItem).Machine;
            }
        }

        private void MachineView_ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(currentMachine != null)
            {
                var aoem = new AddOrEditMachineWindow(currentMachine).ShowDialog();
                UpdateMachine();
            }
        }
    }
}
