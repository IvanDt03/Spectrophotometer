using Spectrophotometer.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Spectrophotometer.Models;

public class MonomerMixtures : Notifier
{
    private ObservableCollection<UnitMonomerMixture> _listUnitMixture;
    private string _name;

    public MonomerMixtures(string name, double lambdaMin, double lambdaMax, double lambdaA, double wFactor)
    {
        _listUnitMixture = new ObservableCollection<UnitMonomerMixture>();
        _name = name;
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

    public string Name
    {
        get { return _name; }
        private set { SetValue(ref _name, value, nameof(Name)); }
    }

    public double LambdaMin { get; }
    public double LambdaMax { get; }
    public double LambdaA { get; }
    public double WFactor { get; }
}
