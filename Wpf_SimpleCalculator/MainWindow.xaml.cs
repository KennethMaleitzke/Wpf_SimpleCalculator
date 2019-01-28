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

namespace Wpf_SimpleCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string _units;

        public MainWindow()
        {
            InitializeComponent();
            InitializeWindowElements();
        }

        private void InitializeWindowElements()
        {
            _units = "feet";

            TextBox_Length.Focus();

            UpdateUnits();

        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Button_Calculate_Click(object sender, RoutedEventArgs e)
        {
            double volume;
            string shape;
            double volumeMultiplier = 1;
            
            if (validateInputs())
            {
                volume = double.Parse(TextBox_Length.Text) * double.Parse(TextBox_Width.Text) * double.Parse(TextBox_Height.Text);

                shape = ComboBox_Shape.SelectionBoxItem as string;

                switch (shape)
                {
                    case "Rectangular Prism":
                        volumeMultiplier = 1;
                        break;
                    case "Pyramidal Prism":
                        volumeMultiplier = 1.0 / 3.0;
                        break;
                    default:
                        break;
                }

                volume = volumeMultiplier * volume;

                TextBox_Volume.Text = volume.ToString();
            }
        }

        private bool validateInputs()
        {
            bool validInputs = true;

            if (
                !double.TryParse(TextBox_Length.Text, out double length) ||
                !double.TryParse(TextBox_Width.Text, out double width) ||
                !double.TryParse(TextBox_Height.Text, out double height)
                )
            {
                MessageBox.Show("Please enter numbers for each field");
                validInputs = false;
                ResetInputs();
            }
            return validInputs;
        }

        private void ResetInputs()
        {
            TextBox_Length.Text = "";
            TextBox_Width.Text = "";
            TextBox_Height.Text = "";
            TextBox_Length.Focus();
        }

        private void Button_Help_Click(object sender, RoutedEventArgs e)
        {
            HelpWindow helpWindow = new HelpWindow();

            helpWindow.ShowDialog();
        }

        private void Volume_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void RadioButton_Units_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                UpdateUnits();
            }
            
        }

        private void UpdateUnits()
        {
            if (RadioButton_Feet.IsChecked == true)
            {
                _units = "feet";
            }
            else if (RadioButton_Meters.IsChecked == true)
            {
                _units = "meters";
            }

            Label_Length.Content = "Length (" + _units + ")";
            Label_Width.Content = "Width (" + _units + ")";
            Label_Height.Content = "Height (" + _units + ")";
            Label_Volume.Content = "Volume (cubic " + _units + ")";
        }
    }
}
