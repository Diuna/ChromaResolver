using ChromaResolver.ViewModels.ECMViewModels;

namespace ChromaResolver.Views.ECMViews
{
    public partial class ECMSampleView
    {
        public ECMSampleViewModel ViewModel
        {
            get;
        }

        public ECMSampleView(ECMSampleViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
            DataContext = ViewModel;
        }
    }
}
