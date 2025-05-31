using OxyPlot.Wpf;
using OxyPlot;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Spectrophotometer.Models;

namespace Spectrophotometer.Service;

public class DialogService : IDialogService
{
    public void ShowMessage(string? message, string title = "Сообщение")
    {
        MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Warning);
    }

    public void PrintChart(PlotModel model, RatioMonomers? ratio, MixtureMonomers mixture)
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
        var originalFontSizeAxisLabel = model.Axes[0].FontSize;
        var originalFontSizeAxisTitle = model.Axes[0].TitleFontSize;

        model.Padding = new OxyThickness(30);
        model.Axes[0].FontSize = 16;
        model.Axes[1].FontSize = 16;
        model.Axes[0].TitleFontSize = 20;
        model.Axes[1].TitleFontSize = 20;
        model.Axes[0].Title = "Длина волны, нм";
        model.Axes[1].Title = "Сигнал";

        if (ratio != null) 
            model.Title = $"{mixture.NameFirstMonomer} - {ratio.VolumeFirstMonomer} мл; {mixture.NameSecondMonomer} - {ratio.VolumeSecondMonomer} мл";

        var bitmap = exporter.ExportToBitmap(model);

        model.Padding = originalPadding;
        model.Axes[0].FontSize = originalFontSizeAxisLabel;
        model.Axes[1].FontSize = originalFontSizeAxisLabel;
        model.Axes[0].TitleFontSize = originalFontSizeAxisTitle;
        model.Axes[1].TitleFontSize = originalFontSizeAxisTitle;
        model.Title = string.Empty;
        model.Axes[0].Title = string.Empty;
        model.Axes[1].Title = string.Empty;

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
