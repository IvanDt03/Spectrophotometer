using Spectrophotometer.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Spectrophotometer.Models;

public class MixtureMonomers : Notifier
{
    private ObservableCollection<RatioMonomers> _listUnitMixture;
    private string _title;
    private string _nameFirstMonomer;
    private string _nameSecondMonomer;

    public MixtureMonomers(string title, string nameFirstMonomer, string nameSecondMonomer, double lambdaMin, double lambdaMax, double lambdaA, double wFactor)
    {
        _listUnitMixture = new ObservableCollection<RatioMonomers>();
        _title = title;
        LambdaMin = lambdaMin;
        LambdaMax = lambdaMax;
        LambdaA = lambdaA;
        WFactor = wFactor;
        _nameFirstMonomer = nameFirstMonomer;
        _nameSecondMonomer = nameSecondMonomer;
    }

    public ObservableCollection<RatioMonomers> ListMonomerMixture
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
        set { SetValue(ref _nameFirstMonomer, value, nameof(NameFirstMonomer)); }
    }

    public string NameSecondMonomer
    {
        get { return _nameSecondMonomer; }
        set { SetValue(ref _nameSecondMonomer, value, nameof(NameSecondMonomer)); }
    }

    public double LambdaMin { get; }
    public double LambdaMax { get; }
    public double LambdaA { get; }
    public double WFactor { get; }
}
