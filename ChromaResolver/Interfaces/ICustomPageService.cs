using System;
using Wpf.Ui;

namespace ChromaResolver.Interfaces
{
    public interface ICustomPageService : IPageService
    {
        event EventHandler<string> OnPageNavigate;
    }
}