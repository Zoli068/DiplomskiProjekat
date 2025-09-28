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

namespace MasterGUI.GUIElements
{
    /// <summary>
    /// Interaction logic for InputTwoValuesDialog.xaml
    /// </summary>
    public partial class InputTwoValuesDialog : Window
    {
        public ushort Address { get; private set; }
        public ushort Number { get; private set; }

        public InputTwoValuesDialog(string title)
        {
            InitializeComponent();
            this.Title = title;
            AddressBox.Focus();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            bool validAddress = ushort.TryParse(AddressBox.Text, out ushort address);
            bool validNumber = ushort.TryParse(NumberBox.Text, out ushort number);

            AddressBox.BorderBrush = validAddress ? Brushes.Gray : Brushes.Red;
            NumberBox.BorderBrush = validNumber ? Brushes.Gray : Brushes.Red;

            if (!validAddress || !validNumber)
            {
                return; 
            }

            Address = address;
            Number = number;
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
