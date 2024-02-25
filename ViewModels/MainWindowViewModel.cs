using CommunityToolkit.Mvvm.ComponentModel;
using ChromaResolver.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Wpf.Ui.Controls;

namespace ChromaResolver.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        public ICollection<object> menuItems;

        [ObservableProperty]
        public ICollection<object> footerItems;

        public MainWindowViewModel()
        {
            menuItems = new ObservableCollection<object>();
            footerItems = new ObservableCollection<object>();
            InitializeMenu();
        }

        private void InitializeMenu()
        {
            MenuItems.Add(new NavigationViewItem("Home", SymbolRegular.Home20, typeof(HomeView)));
            MenuItems.Add(new NavigationViewItem("ECM", SymbolRegular.Cube20, typeof(ECMView)));
            MenuItems.Add(new NavigationViewItem("LAB", SymbolRegular.Beaker20, typeof(LABView)));
            FooterItems.Add(new NavigationViewItemSeparator());
            FooterItems.Add(new NavigationViewItem("Settings", SymbolRegular.Settings20, typeof(SettingsView)));
        }
    }
}
