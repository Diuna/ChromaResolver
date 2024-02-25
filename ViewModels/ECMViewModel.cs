using CommunityToolkit.Mvvm.ComponentModel;

namespace ChromaResolver.ViewModels
{
    public partial class ECMViewModel : ObservableObject
    {
        [ObservableProperty]
        public string welcomeString = "HELLO FROM ECM VIEW!";
    }
}
