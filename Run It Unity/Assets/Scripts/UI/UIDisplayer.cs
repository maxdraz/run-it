using TMPro;
using UnityEngine;

namespace RunIt.UI
{
    public abstract class UIDisplayer : MonoBehaviour
    {
        protected virtual void Display()
        {
        }
        protected virtual void Display(object value)
        {
        }
    }
}