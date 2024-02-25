using ChromaResolver.ViewModels;

namespace ChromaResolver.Views
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class ContentView
    {
        public ContentViewModel ViewModel
        {
            get;
        }

        public ContentView(ContentViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
            DataContext = ViewModel;
        }
    }
}
