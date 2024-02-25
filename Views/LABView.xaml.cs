using ChromaResolver.ViewModels;

namespace ChromaResolver.Views
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class LABView
    {
        public LABViewModel ViewModel
        {
            get;
        }

        public LABView(LABViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
            DataContext = ViewModel;
        }
    }
}
