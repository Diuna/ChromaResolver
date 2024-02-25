using CommunityToolkit.Mvvm.ComponentModel;

namespace ChromaResolver.ViewModels
{
    public partial class LABViewModel : ObservableObject
    {
        [ObservableProperty]
        public string welcomeString = "HELLO FROM LAB VIEW!";
    }
}
