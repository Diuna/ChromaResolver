using ChromaResolver.Interfaces;
using ChromaResolver.ViewModels;
using ChromaResolver.Views;
using System;
using System.Windows;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace ChromaResolver
{
    public partial class MainWindow : IWindow
    {
        public MainWindowViewModel ViewModel { get; set; }

        public MainWindow(
            MainWindowViewModel viewModel,
            INavigationService navigationService,
            IContentDialogService contentDialogService,
            IServiceProvider serviceProvider,
            ISnackbarService snackbarService,
            IThemeService themeService)
        {
            ViewModel = viewModel;
            DataContext = this;

            Wpf.Ui.Appearance.SystemThemeWatcher.Watch(this);

            InitializeComponent();

            snackbarService.SetSnackbarPresenter(RootSnackbar);
            navigationService.SetNavigationControl(NavigationView);
            contentDialogService.SetContentPresenter(RootContentDialogPresenter);
            themeService.SetTheme(Wpf.Ui.Appearance.ApplicationTheme.Dark);
            themeService.SetSystemAccent();

            NavigationView.SetServiceProvider(serviceProvider);
            Wpf.Ui.Appearance.ApplicationThemeManager.Apply(Wpf.Ui.Appearance.ApplicationTheme.Dark);
        }
        private bool _isUserClosedPane;

        private bool _isPaneOpenedOrClosedFromCode;

        private void OnNavigationSelectionChanged(object sender, RoutedEventArgs e)
        {
            if (sender is not NavigationView navigationView)
            {
                return;
            }

            NavigationView.HeaderVisibility =
                navigationView.SelectedItem?.TargetPageType != typeof(HomeView)
                    ? Visibility.Visible
                    : Visibility.Collapsed;
        }

        private void MainWindow_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_isUserClosedPane)
            {
                return;
            }

            _isPaneOpenedOrClosedFromCode = true;
            NavigationView.IsPaneOpen = !(e.NewSize.Width <= 1200);
            _isPaneOpenedOrClosedFromCode = false;
        }

        private void NavigationView_OnPaneOpened(NavigationView sender, RoutedEventArgs args)
        {
            if (_isPaneOpenedOrClosedFromCode)
            {
                return;
            }

            _isUserClosedPane = false;
        }

        private void NavigationView_OnPaneClosed(NavigationView sender, RoutedEventArgs args)
        {
            if (_isPaneOpenedOrClosedFromCode)
            {
                return;
            }

            _isUserClosedPane = true;
        }
    }
}
