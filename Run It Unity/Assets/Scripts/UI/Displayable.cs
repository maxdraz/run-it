using UnityEngine;

namespace RunIt.UI
{
    public abstract class Displayable: MonoBehaviour
    {
        public delegate void OnValueChangedHandler(string name, object value);
        public event OnValueChangedHandler OnValueChanged;

        protected void InvokeOnValueChanged(string name, object value)
        {
            OnValueChanged?.Invoke(name,value);
        }
    }
}