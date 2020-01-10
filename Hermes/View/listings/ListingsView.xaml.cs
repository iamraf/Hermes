using Hermes.Model.Models;
using System.Collections.Generic;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using Hermes.View.listings;
using System.Windows.Input;

namespace Hermes.View
{
    public partial class ListingsView : Page, IListingsView
    {
        private readonly ListingsPresenter _presenter;
        private List<String> _checkedBoxes;

        public ListingsView()
        {
            InitializeComponent();

            ButtonEnable(false);

            _presenter = new ListingsPresenter(this);

            _presenter.GetListings();

            _checkedBoxes = new List<string>();

            comboxCategories.SelectedIndex = 0;
        }

        public ListingsView(string search)
        {
            InitializeComponent();

            ButtonEnable(false);

            _presenter = new ListingsPresenter(this);

            _presenter.GetSearchResults(search);
        }

        public ListingsView(int subCategory,int category)
        {
            InitializeComponent();

            ButtonEnable(false);

            _presenter = new ListingsPresenter(this);
            _checkedBoxes = new List<string>();

            comboxCategories.SelectedIndex=category;
            _changeComboBox(subCategory);

        }

        public List<Listing> Listings
        {
            set 
            {
                listviewListings.ItemsSource = null; // Needed to reset any attached items
                listviewListings.ItemsSource = value;
            }
        }

        public bool Navigate
        {
            set
            {
                if (value)
                {
                    this.NavigationService.Navigate(new LoginPage());
                }
            }
        }

        private void comboxListingsSortBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(_presenter != null)
            {
                _presenter.SortListing(comboxListingsSortBy.SelectedIndex);
            }
        }

        private void BtnListingSelectedFavorite_Click(object sender, RoutedEventArgs e)
        {
            _presenter.AddToFavourites(((Listing) listviewListings.SelectedItem).Id);
        }

        private void btnListingSelectedContact_Click(object sender, RoutedEventArgs e)
        {
            Listing listing = (Listing)listviewListings.SelectedItem;
            User uploader = _presenter.GetUploader(listing.Id);

            if (listing != null && uploader != null)
            {
                var url = "mailto:" + (string) uploader.Email + "?Subject=Intrested on this item: " + listing.Name;
                Process.Start(url);
            }
        }

        private void ButtonEnable(bool action)
        {
            btnListingSelectedFavorite.IsEnabled = action;
            btnListingSelectedContact.IsEnabled = action;
        }

        //Categories Filtering
        private void chboxListingsCategory(object sender, RoutedEventArgs e)
        {
            _checkedBoxes.Add(((CheckBox)sender).Uid);
            if(labelCancelPriceRanges.IsVisible & labelCancelDateRanges.IsVisible)
            {
                _presenter.DateAndPriceFilteredListings(_checkedBoxes, comboxListingsPricePick.SelectedIndex, comboxListingsDatePick.SelectedIndex, getCatId());
            }
            else if(labelCancelPriceRanges.IsVisible & !labelCancelDateRanges.IsVisible)
            {
                comboxListingsPricePick_SelectionChanged(null, null);
            }
            else if(!labelCancelPriceRanges.IsVisible & labelCancelDateRanges.IsVisible)
            {
                comboxListingsDatePick_SelectionChanged(null, null);
            }
            else
            {
                _presenter.GetFilteredListings(_checkedBoxes, getCatId());
            }
        }

        private void unChboxListingsCategory(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("triggers");
            _checkedBoxes.Remove(((CheckBox)sender).Uid);
            if (labelCancelPriceRanges.IsVisible & labelCancelDateRanges.IsVisible)
            {
                _presenter.DateAndPriceFilteredListings(_checkedBoxes, comboxListingsPricePick.SelectedIndex, comboxListingsDatePick.SelectedIndex, getCatId());
            }
            else if (labelCancelPriceRanges.IsVisible & !labelCancelDateRanges.IsVisible)
            {
                comboxListingsPricePick_SelectionChanged(null, null);
            }
            else if (!labelCancelPriceRanges.IsVisible & labelCancelDateRanges.IsVisible)
            {
                comboxListingsDatePick_SelectionChanged(null, null);
            }
            else
            {
                _presenter.GetFilteredListings(_checkedBoxes, getCatId());
            }
        }

        //Price Range Combobox
        private void comboxListingsPricePick_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int priceOption = comboxListingsPricePick.SelectedIndex;
            if(labelCancelDateRanges.IsVisible)
            {
                _presenter.DateAndPriceFilteredListings(_checkedBoxes, priceOption, comboxListingsDatePick.SelectedIndex, getCatId());
            }
            else
            {
                _presenter.PriceFilteredListings(_checkedBoxes, priceOption, getCatId());
            }
        }

        //Price Range Slider
        private void slidListingsPriceCustom_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            float price = (float)slidListingsPriceCustom.Value;
            _presenter.DynamicPriceFilteredListings(_checkedBoxes, price, getCatId());
        }

        //Price Radio Buttons Checked/Unchecked
        private void radbtnListingsPricePick_Checked(object sender, RoutedEventArgs e)
        {
            comboxListingsPricePick.IsEnabled = true;
            labelCancelPriceRanges.Visibility = Visibility.Visible;
        }

        private void radbtnListingsPriceCustom_Checked(object sender, RoutedEventArgs e)
        {
            slidListingsPriceCustom.IsEnabled = true;
            labelCancelPriceRanges.Visibility = Visibility.Visible;
        }

        private void radbtnListingsPricePick_Unchecked(object sender, RoutedEventArgs e)
        {
            comboxListingsPricePick.IsEnabled = false;
            comboxListingsPricePick.SelectedIndex = -1;
        }
        private void radbtnListingsPriceCustom_Unchecked(object sender, RoutedEventArgs e)
        {
            slidListingsPriceCustom.IsEnabled = false;
            slidListingsPriceCustom.Value = 0;
        }

        // Price Range Cancel
        private void labelCancelPriceRanges_PreviewDragOver(object sender, MouseButtonEventArgs e)
        {

            resetPriceRanges();
            if (labelCancelDateRanges.IsVisible)
            {
                comboxListingsDatePick_SelectionChanged(null, null);
            }
            else
            {
                _presenter.GetFilteredListings(_checkedBoxes, getCatId());
            }
        }

        // Reset Price Range Combobox & Slider
        private void resetPriceRanges()
        {
            labelCancelPriceRanges.Visibility = Visibility.Hidden;
            radbtnListingsPricePick.IsChecked = false;
            radbtnListingsPriceCustom.IsChecked = false;
            comboxListingsPricePick.IsEnabled = false;
            comboxListingsPricePick.SelectedIndex = -1;
            slidListingsPriceCustom.IsEnabled = false;
            slidListingsPriceCustom.Value = 0;
        }
        
        // Reset Date Combobox
        private void resetDateRanges()
        {
            labelCancelDateRanges.Visibility = Visibility.Hidden;
            radbtnListingsDatePick.IsChecked = false;
            radbtnListingsDatePick2.IsChecked = false;
            comboxListingsDatePick.IsEnabled = false;
            comboxListingsDatePick.SelectedIndex = -1;
        }

        private void comboxListingsDatePick_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int dateOption = comboxListingsDatePick.SelectedIndex;
            if (labelCancelPriceRanges.IsVisible)
            {
                _presenter.DateAndPriceFilteredListings(_checkedBoxes, comboxListingsPricePick.SelectedIndex, dateOption, getCatId());
            }
            else
            {
                _presenter.DateFilteredListings(_checkedBoxes, dateOption, getCatId());
            }
        }

        // Date Cancel
        private void labelCancelDateRanges_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            resetDateRanges();
            if(labelCancelPriceRanges.IsVisible)
            {
                comboxListingsPricePick_SelectionChanged(null, null);
            }
            else
            {
                _presenter.GetFilteredListings(_checkedBoxes, getCatId());
            }
        }
        
        //Date Radio Buttons Checked/Unchecked
        private void radbtnListingsDatePick_Checked(object sender, RoutedEventArgs e)
        {
            comboxListingsDatePick.IsEnabled = true;
            labelCancelDateRanges.Visibility = Visibility.Visible;
        }

        private void radbtnListingsDatePick2_Checked(object sender, RoutedEventArgs e)
        {
            datePicker.IsEnabled = true;
            labelCancelDateRanges.Visibility = Visibility.Visible;
        }

        private void radbtnListingsDatePick_Unchecked(object sender, RoutedEventArgs e)
        {
            comboxListingsDatePick.IsEnabled = false;
            comboxListingsDatePick.SelectedIndex = -1;
        }

        private void radbtnListingsDatePick2_Unchecked(object sender, RoutedEventArgs e)
        {
            datePicker.IsEnabled = false;
            //
        }

        // Change Category
        private void comboxCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            resetPriceRanges();
            resetDateRanges();
            _presenter.ChangeCategory(getCatId());
            resetCategoriesCheckboxes();
        }

        // Get Category ID from combobox
        private int getCatId()
        {
            return Int32.Parse(((ComboBoxItem)comboxCategories.SelectedItem).Uid);
        }

        private void _changeComboBox(int subCategory) {
            switch (subCategory)
            {
                case 1:
                    chboxListingsCategory0.IsChecked=true;
                    break;
                case 2:
                    chboxListingsCategory1.IsChecked = true;
                    break;
                case 3:
                    chboxListingsCategory2.IsChecked = true;
                    break;
                default:
                    break;
            }
                
        }

        private void resetCategoriesCheckboxes()
        {
            if(getCatId() != 0)
            {
                List<SubCategory> subCategories = _presenter.GetSubcategoriesFromSpecificCategory(getCatId());
                int i = 0;
                
                foreach (object child in LogicalTreeHelper.GetChildren(canvasListingsFilters))
                {
                    if(i<subCategories.Count)
                    {
                        if (child is CheckBox)
                        {
                            ((CheckBox)child).IsChecked = false;
                            ((CheckBox)child).Content = subCategories[i].Name;
                            ((CheckBox)child).Uid = "" + subCategories[i].Id;
                            i++;
                        }
                    }
                }
            }
        }

        private void listviewListings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtonEnable(true);

            Listing listing = (Listing)listviewListings.SelectedItem;

            if (listing != null)
            {
                User uploader = _presenter.GetUploader(listing.Id);

                lblListingSelectedTitle.Content = listing.Name;
                tbListingSelectedDescription.Text = listing.Description;

                if (uploader != null)
                {
                    lblListingSelectedUploader.Content = uploader.Name + " " + uploader.Surname;
                    lblListingSelectedContactInfoEmail1.Content = "Telephone: " + uploader.Telephone;
                    lblListingSelectedContactInfoEmail.Content = "Email: " + uploader.Email;
                }

                _presenter.IncreaseView(listing.Id);
                _presenter.AddToHistory(listing.Id);
            }
        }
    }
}
