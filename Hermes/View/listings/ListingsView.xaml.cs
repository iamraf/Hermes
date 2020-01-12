using Hermes.Model.Models;
using System.Collections.Generic;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using System.Windows.Input;
using Hermes.View.login;

namespace Hermes.View.listings
{
    public partial class ListingsView : Page, IListingsView
    {
        private readonly ListingsPresenter _presenter;

        private List<String> _checkedBoxes;
        private List<Listing> _favorites;

        private int selectedListing = -1;

        public ListingsView()
        {
            InitializeComponent();

            _presenter = new ListingsPresenter(this);

            _presenter.GetListings();

            _presenter.GetFavorites();

            _checkedBoxes = new List<string>();

            comboxCategories.SelectedIndex = 0;

            ButtonEnable(false);
        }

        public ListingsView(int selected)
        {
            InitializeComponent();
        

            selectedListing = selected;

            _presenter = new ListingsPresenter(this);

            comboxCategories.SelectedIndex = 0;

            _presenter.GetListings();

            _presenter.GetFavorites();

            _checkedBoxes = new List<string>();

            ButtonEnable(false);
        }

        public ListingsView(string search)
        {
            InitializeComponent();

            ButtonEnable(false);

            _presenter = new ListingsPresenter(this);

            _presenter.GetSearchResults(search);

            _checkedBoxes = new List<string>();

            ResetPriceRanges();

            ResetDateRanges();

            ResetCategoriesCheckboxes();

            radbtnListingsDatePick.IsEnabled = false;
            radbtnListingsDatePick2.IsEnabled = false;
            radbtnListingsPricePick.IsEnabled = false;
            radbtnListingsPriceCustom.IsEnabled = false;
        }

        public ListingsView(int subCategory, int category)
        {
            InitializeComponent();

            ButtonEnable(false);

            _presenter = new ListingsPresenter(this);

            _checkedBoxes = new List<string>();

            comboxCategories.SelectedIndex=category;

            ChangeComboBox(subCategory);
        }

        public List<Listing> Listings
        {
            set 
            {
                listviewListings.ItemsSource = null; // Needed to reset any attached items
                listviewListings.ItemsSource = value;

                if(selectedListing != -1)
                {
                    Console.WriteLine("SELECTED " + selectedListing);

                    for (int i = 0; i < listviewListings.Items.Count; i++)
                    {
                        Listing tmp = (Listing)listviewListings.Items.GetItemAt(i);

                        if (tmp.Id == selectedListing)
                        {
                            listviewListings.SelectedItem = tmp;
                            listviewListings.ScrollIntoView(tmp);
                            selectedListing = -1;
                            break;
                        }
                    }
                }
            }
        }

        public bool Navigate
        {
            set
            {
                if (value)
                {
                    this.NavigationService.Navigate(new LoginView());
                }
            }
        }

        public List<Listing> Favorites
        {
            set
            {
                _favorites = value;
            }
        }

        private void comboxListingsSortBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(_presenter != null)
            {
                if (labelCancelPriceRanges.IsVisible & labelCancelDateRanges.IsVisible)
                {
                    DateAndPriceFilteredListings();
                }
                else if (labelCancelPriceRanges.IsVisible & !labelCancelDateRanges.IsVisible)
                {
                    PriceFilteredListings();
                }
                else if (!labelCancelPriceRanges.IsVisible & labelCancelDateRanges.IsVisible)
                {
                    comboxListingsDatePick_SelectionChanged(null, null);
                }
                else
                {
                    _presenter.GetFilteredListings(_checkedBoxes, GetCatId(), comboxListingsSortBy.SelectedIndex);
                }
            }
        }

        private void BtnListingSelectedFavorite_Click(object sender, RoutedEventArgs e)
        {
            _presenter.AddToFavourites(((Listing) listviewListings.SelectedItem).Id);
            btnListingSelectedFavorite.IsEnabled = false;
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
            if (_presenter != null && _presenter.GetCurrentUser() != null)
            {
                btnListingSelectedFavorite.IsEnabled = action;
            }
            else
            {
                btnListingSelectedFavorite.IsEnabled = false;
            }

            btnListingSelectedContact.IsEnabled = action;
        }

        private void chboxListingsCategory(object sender, RoutedEventArgs e)
        {
            _checkedBoxes.Add(((CheckBox)sender).Uid);

            if(labelCancelPriceRanges.IsVisible & labelCancelDateRanges.IsVisible)
            {
                DateAndPriceFilteredListings();
            }
            else if(labelCancelPriceRanges.IsVisible & !labelCancelDateRanges.IsVisible)
            {
                PriceFilteredListings();
            }
            else if(!labelCancelPriceRanges.IsVisible & labelCancelDateRanges.IsVisible)
            {
                comboxListingsDatePick_SelectionChanged(null, null);
            }
            else
            {
                _presenter.GetFilteredListings(_checkedBoxes, GetCatId(), comboxListingsSortBy.SelectedIndex);
            }
        }

        private void unChboxListingsCategory(object sender, RoutedEventArgs e)
        {
            _checkedBoxes.Remove(((CheckBox)sender).Uid);

            if (labelCancelPriceRanges.IsVisible & labelCancelDateRanges.IsVisible)
            {
                DateAndPriceFilteredListings();
            }
            else if (labelCancelPriceRanges.IsVisible & !labelCancelDateRanges.IsVisible)
            {
                PriceFilteredListings();
            }
            else if (!labelCancelPriceRanges.IsVisible & labelCancelDateRanges.IsVisible)
            {
                comboxListingsDatePick_SelectionChanged(null, null);
            }
            else
            {
                _presenter.GetFilteredListings(_checkedBoxes, GetCatId(), comboxListingsSortBy.SelectedIndex);
            }
        }

        private void comboxListingsPricePick_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int priceOption = comboxListingsPricePick.SelectedIndex;

            if(labelCancelDateRanges.IsVisible)
            {
                _presenter.DateAndPriceFilteredListings(_checkedBoxes, priceOption, true,comboxListingsDatePick.SelectedIndex, GetCatId(), comboxListingsSortBy.SelectedIndex);
            }
            else
            {
                _presenter.PriceFilteredListings(_checkedBoxes, priceOption, GetCatId(), comboxListingsSortBy.SelectedIndex);
            }
        }

        private void btnGoSlider_Click(object sender, RoutedEventArgs e)
        {
            int priceOption = (int)slidListingsPriceCustom.Value;

            if (labelCancelDateRanges.IsVisible)
            {
                _presenter.DateAndPriceFilteredListings(_checkedBoxes, priceOption, false, comboxListingsDatePick.SelectedIndex, GetCatId(), comboxListingsSortBy.SelectedIndex);
            }
            else
            {
                _presenter.DynamicPriceFilteredListings(_checkedBoxes, priceOption, GetCatId(), comboxListingsSortBy.SelectedIndex);
            }
        }

        private void radbtnListingsPricePick_Checked(object sender, RoutedEventArgs e)
        {
            comboxListingsPricePick.IsEnabled = true;
            labelCancelPriceRanges.Visibility = Visibility.Visible;
        }

        private void radbtnListingsPriceCustom_Checked(object sender, RoutedEventArgs e)
        {
            slidListingsPriceCustom.IsEnabled = true;
            labelCancelPriceRanges.Visibility = Visibility.Visible;
            btnGoSlider.IsEnabled = true;
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
            btnGoSlider.IsEnabled = false;
        }

        private void labelCancelPriceRanges_PreviewDragOver(object sender, MouseButtonEventArgs e)
        {
            ResetPriceRanges();

            if (labelCancelDateRanges.IsVisible)
            {
                comboxListingsDatePick_SelectionChanged(null, null);
            }
            else
            {
                _presenter.GetFilteredListings(_checkedBoxes, GetCatId(), comboxListingsSortBy.SelectedIndex);
            }
        }

        private void ResetPriceRanges()
        {
            labelCancelPriceRanges.Visibility = Visibility.Hidden;
            radbtnListingsPricePick.IsChecked = false;
            radbtnListingsPriceCustom.IsChecked = false;
            comboxListingsPricePick.IsEnabled = false;
            comboxListingsPricePick.SelectedIndex = -1;
            slidListingsPriceCustom.IsEnabled = false;
            slidListingsPriceCustom.Value = 0;
            btnGoSlider.IsEnabled = false;
        }

        private void ResetDateRanges()
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
                DateAndPriceFilteredListings();
            }
            else
            {
                _presenter.DateFilteredListings(_checkedBoxes, dateOption, GetCatId(), comboxListingsSortBy.SelectedIndex);
            }
        }

        private void labelCancelDateRanges_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ResetDateRanges();

            if(labelCancelPriceRanges.IsVisible)
            {
                PriceFilteredListings();
            }
            else
            {
                _presenter.GetFilteredListings(_checkedBoxes, GetCatId(), comboxListingsSortBy.SelectedIndex);
            }
        }
        
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
        }

        private void comboxCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ResetPriceRanges();
            ResetDateRanges();
            _presenter.ChangeCategory(GetCatId(), comboxListingsSortBy.SelectedIndex);
            UpdateCategoriesCheckboxes();
            radbtnListingsDatePick.IsEnabled = true;
            radbtnListingsDatePick2.IsEnabled = true;
            radbtnListingsPricePick.IsEnabled = true;
            radbtnListingsPriceCustom.IsEnabled = true;
        }

        private int GetCatId()
        {
            return Int32.Parse(((ComboBoxItem)comboxCategories.SelectedItem).Uid);
        }

        private void ChangeComboBox(int subCategory) 
        {
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

        private void UpdateCategoriesCheckboxes()
        {
            ResetCategoriesCheckboxes();

            if (GetCatId() != 0)
            {
                List<SubCategory> subCategories = _presenter.GetSubcategoriesFromSpecificCategory(GetCatId());

                int i = 0;
                
                foreach (object child in LogicalTreeHelper.GetChildren(canvasListingsFilters))
                {
                    if (i < subCategories.Count)
                    {
                        if (child is CheckBox)
                        {
                            ((CheckBox)child).IsEnabled = true;
                            ((CheckBox)child).Content = subCategories[i].Name;
                            ((CheckBox)child).Uid = "" + subCategories[i].Id;
                            i++;
                        }
                    } 
                    else
                    {
                        if (child is CheckBox)
                        {
                            ((CheckBox)child).Visibility = Visibility.Hidden;
                        }
                    }
                }
            }
        }

        private void ResetCategoriesCheckboxes()
        {
            foreach (object child in LogicalTreeHelper.GetChildren(canvasListingsFilters))
            {
                if (child is CheckBox)
                {
                    ((CheckBox)child).Visibility = Visibility.Visible;
                    ((CheckBox)child).Content = "";
                    ((CheckBox)child).IsChecked = false;
                    ((CheckBox)child).IsEnabled = false;
                }
            }
        }

        private void listviewListings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtonEnable(true);

            if (_presenter.GetCurrentUser() != null)
            {
                _presenter.GetFavorites();

                foreach (Listing lis in _favorites)
                {
                    if(listviewListings.SelectedItem != null && (listviewListings.SelectedItem as Listing).Id == lis.Id)
                    {
                        btnListingSelectedFavorite.IsEnabled = false;
                    }
                }
            }

            Listing listing = (Listing)listviewListings.SelectedItem;

            if (listing != null)
            {
                User uploader = _presenter.GetUploader(listing.Id);

                lblListingSelectedTitle.Content = listing.Name;
                tbListingSelectedDescription.Text = listing.Description;
                imgListingsSelected.Source = listing.Image;

                if (uploader != null)
                {
                    lblListingSelectedUploader.Content = uploader.Name + " " + uploader.Surname;
                    lblListingSelectedContactInfoEmail1.Content = "Telephone: " + uploader.Telephone;
                    lblListingSelectedContactInfoEmail.Content = "Email: " + uploader.Email;
                }
                else
                {
                    lblListingSelectedUploader.Content = "-";
                    lblListingSelectedContactInfoEmail1.Content = "Telephone: - ";
                    lblListingSelectedContactInfoEmail.Content = "Email: - ";
                    btnListingSelectedContact.IsEnabled = false;
                }

                _presenter.IncreaseView(listing.Id);
                _presenter.AddToHistory(listing.Id);
            }
        }

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (comboxCategories.SelectedIndex == 0)
            {
                comboxCategories_SelectionChanged(null, null);
            }
            else
            {
                comboxCategories.SelectedIndex = 0;
            }
        }

        private void DateAndPriceFilteredListings()
        {
            if ((bool)radbtnListingsPricePick.IsChecked)
            {
                _presenter.DateAndPriceFilteredListings(_checkedBoxes, comboxListingsPricePick.SelectedIndex, true, comboxListingsDatePick.SelectedIndex, GetCatId(), comboxListingsSortBy.SelectedIndex);
            }
            else
            {
                _presenter.DateAndPriceFilteredListings(_checkedBoxes, (int)slidListingsPriceCustom.Value, false, comboxListingsDatePick.SelectedIndex, GetCatId(), comboxListingsSortBy.SelectedIndex);
            }
        }

        private void PriceFilteredListings()
        {
            if ((bool)radbtnListingsPricePick.IsChecked)
            {
                comboxListingsPricePick_SelectionChanged(null, null);
            }
            else
            {
                btnGoSlider_Click(null, null);
            }
        }
    }
}
