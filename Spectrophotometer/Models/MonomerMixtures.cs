using Spectrophotometer.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Spectrophotometer.Models;

public class MonomerMixtures : Notifier
{
    private ObservableCollection<UnitMonomerMixture> _listUnitMixture;
    private string _title;

    public MonomerMixtures(string title, double lambdaMin, double lambdaMax, double lambdaA, double wFactor)
    {
        _listUnitMixture = new ObservableCollection<UnitMonomerMixture>();
        _title = title;
        LambdaMin = lambdaMin;
        LambdaMax = lambdaMax;
        LambdaA = lambdaA;
        WFactor = wFactor;
    }

    public UnitMonomerMixture this[int index]
    {
        get { return _listUnitMixture[index]; }
    }

    public ObservableCollection<UnitMonomerMixture> ListMonomerMixture
    {
        get { return _listUnitMixture; }
        set { SetValue(ref _listUnitMixture, value, nameof(ListMonomerMixture)); }
    }

    public string Title
    {
        get { return _title; }
        private set { SetValue(ref _title, value, nameof(Title)); }
    }

    public double LambdaMin { get; }
    public double LambdaMax { get; }
    public double LambdaA { get; }
    public double WFactor { get; }
}
