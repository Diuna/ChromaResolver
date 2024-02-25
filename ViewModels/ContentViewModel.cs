using CommunityToolkit.Mvvm.ComponentModel;

namespace ChromaResolver.ViewModels
{
    public partial class ContentViewModel : ObservableObject
    {
        [ObservableProperty]
        public string welcomeString = "HELLO FROM CONTENT VIEW!";
    }
}
