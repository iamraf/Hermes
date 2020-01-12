using Hermes.Model.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Globalization;
using Hermes.View.Upload;
using System.Text.RegularExpressions;

namespace Hermes.View.upload
{
    public partial class UploadView : Page, IUploadView
    {
        private UploadPresenter _presenter;
        private string ImagePathSrc = null;

        public UploadView()
        {
            InitializeComponent();

            //IF USER IS NOT LOGGED IN REDIRECT HIM TO LOG IN

            _presenter = new UploadPresenter(this);

            //---------------------------------------

            comboxUploadCategory.ItemsSource = _presenter.GetCategoryNames();
            comboxUploadCategory.SelectedIndex = 0;

            //---------------------------------------

            comboxUploadLocation.ItemsSource = _presenter.GetLocationNames();
            comboxUploadLocation.SelectedIndex = 0;

            //---------------------------------------

            radbtnUploadSell.IsChecked = true;
        }

        public string name
        {
            get
            {
                return txtboxUploadTitle.Text;
            }
        }

        public float price
        {
            get
            {
                try
                {
                    return float.Parse(txtboxUploadPrice.Text, CultureInfo.InvariantCulture);
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        public Location location
        {
            get
            {
                return _presenter.GetSelectedLocation((int)comboxUploadLocationTK.SelectedItem);
            }
        }

        public string description
        {
            get
            {
                return txtboxUploadDescription.Text;
            }
        }

        public SubCategory subcategory
        {
            get
            {
                return _presenter.GetSelectedSubCategory((string)comboxUploadSubcategory.SelectedItem);
            }
        }

        public string GetImagePath
        {
            get
            {
                return ImagePathSrc;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            txtboxUploadPrice.IsEnabled = !(bool)(checkBoxFreePrice.IsChecked);
            txtboxUploadPrice.Text = "0";
        }

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            Location myLocation = _presenter.GetMyHomeLocation();
            comboxUploadLocation.SelectedItem = myLocation.Name;
            comboxUploadLocationTK.SelectedItem = myLocation.Tk.ToString();
            comboxUploadLocation.IsEnabled = !(bool)(checkBoxMyHome.IsChecked);
            comboxUploadLocationTK.IsEnabled = comboxUploadLocation.IsEnabled;
        }

        private void comboxUploadCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comboxUploadSubcategory.ItemsSource = _presenter.GetSubCategoryNames(comboxUploadCategory.SelectedIndex);
            comboxUploadSubcategory.IsEnabled = true;
            comboxUploadSubcategory.SelectedIndex = 0;
        }

        private void comboxUploadLocation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comboxUploadLocationTK.ItemsSource = _presenter.GetLocationTK((string)comboxUploadLocation.SelectedItem);
            comboxUploadLocationTK.SelectedIndex = 0;
        }

        private void btnUploadUpload_Click(object sender, RoutedEventArgs e)
        { //new addition
            int remaining = _presenter.GetAvailablePremiumListings();
            if (remaining != 0 && checkboxPremium.IsChecked == true)
            {
                //decrease remaining
                _presenter.DecreasePremiumListings();
                bool uploadResult = _presenter.UploadListing(GetTaggedListingName(), price, location.Id, description, subcategory.Id, GetListingType(), (bool)checkboxPremium.IsChecked);
                if (uploadResult)
                {
                    System.Windows.MessageBox.Show("Listing uploaded succesfully", "Upload Complete", MessageBoxButton.OK);
                    MessageBox.Show("You now have " + _presenter.GetAvailablePremiumListings() + "  available(s) premium listings", "Confirmation", MessageBoxButton.OK);

                    this.NavigationService.Navigate(new Uri("View/mylistings/MyListingsView.xaml", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    System.Windows.MessageBox.Show("Could not upload listing.\nSQL Error.", "Error", MessageBoxButton.OK);
                }
              
            }
            else
            {
                //Old working code
                MessageBoxResult result = MessageBoxResult.Yes;
                if (checkboxPremium.IsChecked == true)
                {
                    //starting popupwindow
                    //paymentCard.acti;
                    paymentCard win2 = new paymentCard();
                    win2.ShowDialog();
                    if (win2.returnOk == true)
                        result = System.Windows.MessageBox.Show("Are you sure you want to activate premium?", "Warning", MessageBoxButton.YesNo);
                    else
                        return;
                }
                
                if (result == MessageBoxResult.Yes )
                {
                    bool uploadResult = _presenter.UploadListing(GetTaggedListingName(), price, location.Id, description, subcategory.Id, GetListingType(), (bool)checkboxPremium.IsChecked);
                    if (uploadResult)
                    {
                        System.Windows.MessageBox.Show("Listing uploaded succesfully", "Upload Complete", MessageBoxButton.OK);
                        this.NavigationService.Navigate(new Uri("View/mylistings/MyListingsView.xaml", UriKind.RelativeOrAbsolute));
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Could not upload listing.\nSQL Error.", "Error", MessageBoxButton.OK);
                    }
                }
            }
        }

                private string GetTaggedListingName()
        {
            if (radbtnUploadSell.IsChecked == true)
            {
                return "[Selling] " + name;
            }
            else
            {
                return "[Looking for] " + name;
            }
        }

        private bool GetListingType()
        {
            if (radbtnUploadSell.IsChecked == true)
            {
                return false;
            }
            else
                return true;
        }

        private void btnUploadImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SetImagePath();
                UploadImage.Source = new BitmapImage(new Uri(GetImagePath));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void SetImagePath()
        {
            Microsoft.Win32.OpenFileDialog openFileDialog1 = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Image files | *.jpg"
            };

            if (openFileDialog1.ShowDialog() == true)
            {
                ImagePathSrc = openFileDialog1.FileName;
            }
            else
            {
                ImagePathSrc = null;
            }
        }

        //Limits textboxes to letters only
        private void txtboxLetterValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        //Limits textboxes to numbers only
        private void txtboxNumberValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}