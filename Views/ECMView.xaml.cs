using ChromaResolver.ViewModels;

namespace ChromaResolver.Views
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class ECMView
    {
        public ECMViewModel ViewModel
        {
            get;
        }

        public ECMView(ECMViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
            DataContext = ViewModel;
        }
    }
}
