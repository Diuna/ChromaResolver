using ChromaResolver.ViewModels;

namespace ChromaResolver.Views
{
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
