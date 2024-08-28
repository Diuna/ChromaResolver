using ChromaResolver.Models.ECM;
using ChromaResolver.ViewModels.ECMViewModels;
using Wpf.Ui.Controls;

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
            if (sender is Button button)
            {
                if (button.DataContext is Sample sample)
                {
                    ViewModel.Edit(sample);
                }
            }
        }
    }
}
