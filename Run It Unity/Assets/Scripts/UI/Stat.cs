using UnityEngine;

namespace RunIt.UI
{
    public abstract class Stat : MonoBehaviour
    {
        public delegate void StatChangeHandler(object value);
        public abstract event StatChangeHandler StatChanged;

        public abstract object GetValue();

        
    }
}