namespace Spectrophotometer.Models;

public struct DataPoint
{
    public double Lambda { get; set; }
    public double Signal { get; set; }

    public DataPoint(double lambda, double signal)
    {
        Lambda = lambda;
        Signal = signal;
    }
}
