using System.Windows;

namespace Spectrophotometer.Service;

public class MessageBoxDialogService : IDialogService
{
    public void ShowMessage(string? message, string title = "Сообщение")
    {
        MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Warning);
    }
}
