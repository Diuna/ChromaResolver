using ChromaResolver.ViewModels.ECMViewModels;

namespace ChromaResolver.Views.ECMViews
{
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

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ViewModel.EditCommand.Execute(null);
        }
    }
}
