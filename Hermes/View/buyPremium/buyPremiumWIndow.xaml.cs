using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Hermes.View.buyPremium;
namespace Hermes.View.buyPremium
{
    /// <summary>
    /// Interaction logic for buyPremiumWIndow.xaml
    /// </summary>
    public partial class buyPremiumWIndow : Window, IbuyPremiumWindow
    {
        private  buyPremiumPresenter _presenter;
        Boolean returnOk = false;
        public buyPremiumWIndow()
        {
            _presenter = new buyPremiumPresenter(this);
            InitializeComponent();
            
        }

        public bool ReturnOk { get => returnOk; set => returnOk=value; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _presenter.addPremiumListings(comboboxPremiumOffers.SelectedIndex);
            returnOk = true;
            this.Close();
        }
        
        private void txtboxLetterValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txtboxNumberValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnTopClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
