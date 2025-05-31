using OxyPlot.Wpf;
using OxyPlot;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Spectrophotometer.Service;

public class DialogService : IDialogService
{
    public void ShowMessage(string? message, string title = "Сообщение")
    {
        MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Warning);
    }

    public void PrintChart(PlotModel model)
    {
        var printDialog = new PrintDialog();
        printDialog.PrintTicket.PageOrientation = System.Printing.PageOrientation.Landscape;
        if (printDialog.ShowDialog() != true) return;

        double paddingFactor = 1.1;
        int exportWidth = (int)(printDialog.PrintableAreaWidth * 96 / 100 * paddingFactor);
        int exportHeight = (int)(printDialog.PrintableAreaHeight * 96 / 100 * paddingFactor);

        var exporter = new PngExporter
        {
            Width = exportWidth,
            Height = exportHeight,
        };

        var originalPadding = model.Padding;
        model.Padding = new OxyThickness(30);

        var bitmap = exporter.ExportToBitmap(model);

        model.Padding = originalPadding;

        var image = new System.Windows.Controls.Image
        {
            Source = bitmap,
            Stretch = Stretch.Uniform,
            Width = printDialog.PrintableAreaWidth * 0.95,
            Height = printDialog.PrintableAreaHeight * 0.95
        };

        var printCanvas = new Canvas
        {
            Width = printDialog.PrintableAreaWidth,
            Height = printDialog.PrintableAreaHeight,
            Background = Brushes.White
        };

        printCanvas.Children.Add(image);
        Canvas.SetLeft(image, (printDialog.PrintableAreaWidth - image.Width) / 2);
        Canvas.SetTop(image, (printDialog.PrintableAreaHeight - image.Height) / 2);

        printDialog.PrintVisual(printCanvas, "Печать грфика");
    }
}
