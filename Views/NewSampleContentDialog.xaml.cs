using System.Windows.Controls;
using Wpf.Ui.Controls;

namespace ChromaResolver.Views
{
    /// <summary>
    /// Interaction logic for NewSampleContentDialog.xaml
    /// </summary>
    public partial class NewSampleContentDialog : ContentDialog
    {
        public NewSampleContentDialog(ContentPresenter contentPresenter)
            : base(contentPresenter)
        {
            InitializeComponent();
        }
    }
}
