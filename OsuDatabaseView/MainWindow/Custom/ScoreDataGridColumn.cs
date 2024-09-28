using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using OsuDatabaseView.Utils.Converters;
using Binding = System.Windows.Data.Binding;

namespace OsuDatabaseView.MainWindow.Custom;

internal class ScoreDataGridColumn : DataGridTextColumn
{
    private string _visibilityParameter;

    public string VisibilityParameter
    {
        get => _visibilityParameter;
        set
        {
            if (_visibilityParameter != value)
            {
                _visibilityParameter = value;
                BindingVisibility();
            }
        }
    }
    
    private object _sourceParameter;
    public object SourceParameter
    {
        get => _sourceParameter;
        set
        {
            if (_sourceParameter != value)
            {
                _sourceParameter = value;
                BindingVisibility();
            }
        }
    }
    private void BindingVisibility()
    {
        if (_sourceParameter is null && _visibilityParameter is null) return;
        var binding = new Binding("DataContext.MainColumnVisibility")
        {
            Converter = new MainColumnVisibilityConverter(),
            ConverterParameter = VisibilityParameter,
            Source = _sourceParameter,
            Mode = BindingMode.OneWay
        };
        BindingOperations.ClearBinding(this, VisibilityProperty);
        BindingOperations.SetBinding(this, VisibilityProperty, binding);
    }
}