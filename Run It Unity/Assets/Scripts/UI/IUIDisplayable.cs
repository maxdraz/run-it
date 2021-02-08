using System;
using RunIt.Player;

namespace RunIt.UI
{
    public interface IUIDisplayable
    {
        public object GetValue();
        public delegate void ValueChangedHandler(object value);
        public event ValueChangedHandler ValueChanged;
    }
}