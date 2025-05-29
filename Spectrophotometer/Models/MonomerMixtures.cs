using Spectrophotometer.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Spectrophotometer.Models;

public class MonomerMixtures : Notifier
{
    private ObservableCollection<UnitMonomerMixture> _listUnitMixture;
    private string _title;
    private string _nameFirstMonomer;
    private string _nameSecondMonomer;

    public MonomerMixtures(string title, double lambdaMin, double lambdaMax, double lambdaA, double wFactor, string nameFirstMonomer, string nameSecondMonomer)
    {
        _listUnitMixture = new ObservableCollection<UnitMonomerMixture>();
        _title = title;
        LambdaMin = lambdaMin;
        LambdaMax = lambdaMax;
        LambdaA = lambdaA;
        WFactor = wFactor;
        _nameFirstMonomer = nameFirstMonomer;
        _nameSecondMonomer = nameSecondMonomer;
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

    public string NameFirstMonomer
    {
        get { return _nameFirstMonomer; }
        private set { SetValue(ref _nameFirstMonomer, value, nameof(_nameFirstMonomer)); }
    }

    public string NameSecondMonomer
    {
        get { return _nameSecondMonomer; }
        private set { SetValue(ref _nameSecondMonomer, value, nameof(NameSecondMonomer)); }
    }

    public double LambdaMin { get; }
    public double LambdaMax { get; }
    public double LambdaA { get; }
    public double WFactor { get; }
}
