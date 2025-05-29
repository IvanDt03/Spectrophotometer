namespace Spectrophotometer.Service;

public interface IDialogService
{
    void ShowMessage(string? message, string title = "Сообщение");
}
