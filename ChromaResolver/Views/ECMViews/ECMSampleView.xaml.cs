using ChromaResolver.ViewModels.ECMViewModels;
using System.Text.RegularExpressions;

namespace ChromaResolver.Views.ECMViews
{
    public partial class ECMSampleView
    {
        private readonly Regex _regex = new(@"^[0-9.]+$");

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
        private void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !_regex.IsMatch(e.Text);
        }
    }
}
