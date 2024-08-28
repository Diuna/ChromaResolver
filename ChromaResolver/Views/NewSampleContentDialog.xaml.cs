using ChromaResolver.ViewModels;
using System.Text.RegularExpressions;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace ChromaResolver.Views
{
    /// <summary>
    /// Interaction logic for NewSampleContentDialog.xaml
    /// </summary>
    public partial class NewSampleContentDialog : ContentDialog
    {
        private readonly Regex _regex = new(@"^[0-9.]+$");

        public NewSampleContentDialogViewModel ViewModel { get; }

        public NewSampleContentDialog(NewSampleContentDialogViewModel viewModel, IContentDialogService contentDialogService)
            : base(contentDialogService.GetContentPresenter())
        {
            ViewModel = viewModel;
            InitializeComponent();
            DataContext = ViewModel;
            ViewModel.UpdateControl += UpdatePrimaryButtonEnabled;
        }

        private void UpdatePrimaryButtonEnabled(bool enabled)
        {
            IsPrimaryButtonEnabled = enabled;
        }

        private void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !_regex.IsMatch(e.Text);
        }

        private void ContentDialog_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            ViewModel.ClearDataOnLoaded();
        }
    }
}
