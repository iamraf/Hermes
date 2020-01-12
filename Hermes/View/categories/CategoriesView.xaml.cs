using Hermes.View.listings;
using System.Windows;
using System.Windows.Controls;

namespace Hermes.View.categories
{
    public partial class CategoriesView : Page
    {
        public CategoriesView()
        {
            InitializeComponent();
        }

        private void btnCategory1_1_Click(object sender, RoutedEventArgs e)
        {
            //ListingsView a=new ListingsView("test", 2);
            this.NavigationService.Navigate(new ListingsView(1,1));
        }

        private void btnCategory1_2_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ListingsView(2, 1));
        }

        private void btnCategory1_3_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ListingsView(3, 1));
        }

        private void btnCategory2_1_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ListingsView(1, 2));
        }

        private void btnCategory2_2_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ListingsView(2, 2));
        }

        private void btnCategory2_3_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ListingsView(3, 2));
        }

        private void btnCategory3_1_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ListingsView(1, 3));
        }

        private void btnCategory3_2_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ListingsView(2, 3));
        }

        private void btnCategory3_3_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ListingsView(3, 3));
        }

        private void btnCategory4_1_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ListingsView(1, 4));
        }

        private void btnCategory4_2_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ListingsView(2,4));
        }

        private void btnCategory4_3_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ListingsView(3, 4));
        }

        private void btnCategory5_1_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ListingsView(1, 5));
        }

        private void btnCategory5_2_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ListingsView(2, 5));
        }

        private void btnCategory5_3_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ListingsView(3, 5));
        }

        private void btnCategory6_1_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ListingsView(1, 6));
        }

        private void btnCategory6_2_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ListingsView(2, 6));
        }

        private void btnCategory6_3_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ListingsView(3, 6));
        }

        private void btnCategory7_1_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ListingsView(1, 7));
        }

        private void btnCategory7_2_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ListingsView(2, 7));
        }

        private void btnCategory7_3_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ListingsView(3, 7));
        }

        private void btnCategory8_1_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ListingsView(1, 8));
        }

        private void btnCategory8_2_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ListingsView(2, 8));
        }

        private void btnCategory8_3_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ListingsView(3, 8));
        }

        private void btnCategory9_1_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ListingsView(1,9));
        }

        private void btnCategory9_2_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ListingsView(2, 9));
        }

        private void btnCategory9_3_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ListingsView(3, 9));
        }

        private void btnCategory10_1_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ListingsView(1, 10));
        }

        private void btnCategory10_2_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ListingsView(2, 10));
        }

        private void btnCategory10_3_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ListingsView(3, 10));
        }
    }
}
