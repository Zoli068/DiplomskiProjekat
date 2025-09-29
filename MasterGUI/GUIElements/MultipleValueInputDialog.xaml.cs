using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MasterGUI.GUIElements
{
    /// <summary>
    /// Interaction logic for MultipleValueInputDialog.xaml
    /// </summary>
    public partial class MultipleValueInputDialog : Window
    {
        public ushort Address { get; private set; }
        public ushort Quantity { get; private set; }
        public List<short> Values { get; private set; } = new();

        private List<ValueEntry> valueEntries = new();

        public MultipleValueInputDialog()
        {
            InitializeComponent();
        }

        private void QuantityBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !ushort.TryParse(((TextBox)sender).Text + e.Text, out _);
        }

        private void SetQuantity_Click(object sender, RoutedEventArgs e)
        {
            if (!ushort.TryParse(AddressBox.Text, out ushort addr))
            {
                MessageBox.Show("Enter valid address First.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!ushort.TryParse(QuantityBox.Text, out ushort qty) || qty == 0)
            {
                MessageBox.Show("Enter valid quantity.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Quantity = qty;
            valueEntries = Enumerable.Range(0, qty)
                                     .Select(i => new ValueEntry { Index = addr+i, Value = 0 })
                                     .ToList();

            ValuesGrid.ItemsSource = valueEntries;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (!ushort.TryParse(AddressBox.Text, out ushort addr))
            {
                MessageBox.Show("Enter valid address.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Address = addr;

            try
            {
                Values = valueEntries.Select(v =>
                {
                    if (!short.TryParse(v.Value.ToString(), out short val))
                        throw new Exception($"Invalid value at Address {v.Index}");
                    return val;
                }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        public class ValueEntry : INotifyPropertyChanged
        {
            public int Index { get; set; }

            private short _value;
            public short Value
            {
                get => _value;
                set
                {
                    _value = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;
        }
    }
}