using System.Windows;

namespace Hermes.View.help
{
    public partial class HelpView : Window
    {
        public HelpView()
        {
            InitializeComponent();
        }

        private void btnHelpClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
