using OxyPlot;
using Spectrophotometer.Models;
using System;

namespace Spectrophotometer.Service;

public interface IDialogService
{
    void ShowMessage(string? message, string title = "Сообщение");
    void PrintChart(PlotModel model, RatioMonomers ratio, MixtureMonomers mixture);
}
