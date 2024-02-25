using ChromaResolver.ViewModels.ECMViewModels;

namespace ChromaResolver.Views
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class ECMSamplesView
    {
        public ECMSamplesViewModel ViewModel
        {
            get;
        }

        public ECMSamplesView(ECMSamplesViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
            DataContext = ViewModel;
        }
    }
}
