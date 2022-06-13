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

namespace appWPF
{
    /// <summary>
    /// Lógica interna para FormsPokemon.xaml
    /// </summary>
    public partial class FormsPokemon : Window
    {
        public FormsPokemon()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void bntSalvar(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
