using ChromaResolver.ViewModels;
using Wpf.Ui.Controls;

namespace ChromaResolver.Views
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class HomeView : INavigableView<HomeViewModel>
    {
        public HomeViewModel ViewModel { get; set; }

        public HomeView(HomeViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
            DataContext = ViewModel;
        }

    }
}
