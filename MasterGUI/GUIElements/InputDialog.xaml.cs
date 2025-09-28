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
    /// Interaction logic for InputDialog.xaml
    /// </summary>
    public partial class InputDialog : Window
    {
        public string ResponseText { get; private set; }
        private readonly Type type;

        public InputDialog(string question, Type type)
        {
            this.type= type;
            InitializeComponent();
            this.Title = question;
            InputBox.Text = "";
            InputBox.Focus();
            InputBox.SelectAll();
            
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            bool valid = TryConvert(InputBox.Text, type);

            if (!valid)
            {
                e.Handled = false;
                InputBox.BorderBrush = Brushes.Red;
                return;
            }
            ResponseText = InputBox.Text;
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }


        private bool TryConvert(string text, Type targetType)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(text))
                    return false;

                if (targetType == typeof(int))
                    return int.TryParse(text, out _);

                if (targetType == typeof(double))
                    return double.TryParse(text, out _);

                if (targetType == typeof(short))
                    return short.TryParse(text, out _);

                if (targetType == typeof(long))
                    return long.TryParse(text, out _);

                if (targetType == typeof(decimal))
                    return decimal.TryParse(text, out _);

                Convert.ChangeType(text, targetType);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
