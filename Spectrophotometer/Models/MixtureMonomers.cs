using Spectrophotometer.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Spectrophotometer.Models;

public class MixtureMonomers : Notifier
{
    private ObservableCollection<RatioMonomers> _listRatio;
    private ObservableCollection<DataPoint> _dataForCalibration;
    private string _title;
    private string _nameFirstMonomer;
    private string _nameSecondMonomer;

    public MixtureMonomers(string title, 
        string nameFirstMonomer,
        string nameSecondMonomer,
        double lambdaMin, 
        double lambdaMax, 
        double lambdaA, 
        double wFactor,
        double freeFactor,
        double angularFactor,
        IEnumerable<DataPoint> dataForCalibration)
    {
        _listRatio = new ObservableCollection<RatioMonomers>();
        _title = title;
        LambdaMin = lambdaMin;
        LambdaMax = lambdaMax;
        LambdaA = lambdaA;
        WFactor = wFactor;
        FreeFactor = freeFactor;
        AngularFactor = angularFactor;
        _nameFirstMonomer = nameFirstMonomer;
        _nameSecondMonomer = nameSecondMonomer;
        _dataForCalibration = new ObservableCollection<DataPoint>(dataForCalibration);
    }

    public ObservableCollection<RatioMonomers> ListRatio
    {
        get { return _listRatio; }
        set { SetValue(ref _listRatio, value, nameof(ListRatio)); }
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

    public ObservableCollection<DataPoint> DataForCalibration
    {
        get { return _dataForCalibration; }
        set { SetValue(ref _dataForCalibration, value, nameof(DataForCalibration)); }
    }

    public double LambdaMin { get; }
    public double LambdaMax { get; }
    public double LambdaA { get; }
    public double WFactor { get; }
    public double FreeFactor { get; set; }
    public double AngularFactor { get; set; }
}
