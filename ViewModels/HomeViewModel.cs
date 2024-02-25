using CommunityToolkit.Mvvm.ComponentModel;

namespace ChromaResolver.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        [ObservableProperty]
        public string welcomeString = "HELLO FROM HOME!";
    }
}