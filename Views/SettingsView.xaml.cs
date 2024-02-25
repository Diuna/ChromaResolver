using ChromaResolver.ViewModels;
using Wpf.Ui.Controls;


namespace ChromaResolver.Views
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class SettingsView : INavigableView<SettingsViewModel>
    {
        public SettingsViewModel ViewModel { get; set; }


        public SettingsView(SettingsViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
            DataContext = ViewModel;
        }
    }
}
