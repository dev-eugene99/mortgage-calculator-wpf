using MortgageCalculator.Desktop.ViewModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace MortgageCalculator.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly Regex _regexNumbersOnly = new Regex("[^0-9]+"); //regex that matches disallowed text
        private static readonly Regex _regexDecimal = new Regex("[^.]+"); //regex that matches disallowed text
        private Mortgage mortgage;

        public MainWindow()
        {
            InitializeComponent();
            mortgage = new Mortgage();
            DataContext = mortgage;
        }

        private void Integer_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsNumberAllowed(e.Text);
        }

        private void Float_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(IsNumberAllowed(e.Text) || IsDecimalAllowed(e.Text));
        }

        private static bool IsNumberAllowed(string text)
        {
            return !_regexNumbersOnly.IsMatch(text);
        }

        private static bool IsDecimalAllowed(string text)
        {
            return !_regexDecimal.IsMatch(text);
        }
    }
}
