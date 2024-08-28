using System.Windows;

namespace ChromaResolver.Interfaces
{
    public interface IWindow
    {
        event RoutedEventHandler Loaded;

        void Show();
    }
}
