using SynthCustomControls;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;


namespace Spectrophotometer.Views;

public partial class KnobLambda : UserControl
{
    public static readonly DependencyProperty ValueLambdaProperty;

    static KnobLambda()
    {
        FrameworkPropertyMetadata metadata = new FrameworkPropertyMetadata(100.0);
        metadata.BindsTwoWayByDefault = true;
        
        ValueLambdaProperty = DependencyProperty.Register("ValueLambda", typeof(double), typeof(KnobLambda), metadata);
    }
    public KnobLambda()
    {
        InitializeComponent();
        knob.ValueChanged += ValueKnobChanged;
    }

    public double ValueLambda
    {
        get { return (double)GetValue(ValueLambdaProperty); }
        set { SetValue(ValueLambdaProperty, value); }
    }

    private void ValueKnobChanged(object? sender, double e)
    {
        var obj = sender as Knob;
        if (obj is not null)
        {
            ValueLambda = Math.Floor(e * 1000);
        }
    }
}
