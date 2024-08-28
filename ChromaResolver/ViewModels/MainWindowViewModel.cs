using ChromaResolver.Views;
using ChromaResolver.Views.ECMViews;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
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
            menuItems = [];
            footerItems = [];
            InitializeMenu();
        }

        private void InitializeMenu()
        {
            MenuItems.Add(new NavigationViewItem("Home", SymbolRegular.Home20, typeof(HomeView)));
            MenuItems.Add(new NavigationViewItem("ECM", SymbolRegular.Cube20, typeof(ECMSamplesView)));
            MenuItems.Add(new NavigationViewItem("LAB", SymbolRegular.Beaker20, typeof(LABView)));
            FooterItems.Add(new NavigationViewItemSeparator());
            FooterItems.Add(new NavigationViewItem("Settings", SymbolRegular.Settings20, typeof(SettingsView)));
        }

        [RelayCommand]
        private void CornerClicked(string parameter)
        {
            switch (parameter)
            {
                case "Maximize":
                    App.Current.MainWindow.WindowState = System.Windows.WindowState.Maximized;
                    break;
                case "Minimize":
                    App.Current.MainWindow.WindowState = System.Windows.WindowState.Minimized;
                    break;
                case "Normalize":
                    App.Current.MainWindow.WindowState = System.Windows.WindowState.Normal;
                    break;
                case "Close":
                    App.Current.Shutdown();
                    break;
            }
        }
    }
}
