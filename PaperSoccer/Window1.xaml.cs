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

namespace PaperSoccer
{
    public partial class SettingsWindow : Window
    {
        private MainWindow _mainForm = null;

        public SettingsWindow()
        {
            InitializeComponent();
        }

        public SettingsWindow(object sender)
        {
            _mainForm = sender as MainWindow;
            InitializeComponent();
        }

        private void PlayerNameEdit_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {

        }

        private void PlayerNameEdit_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
