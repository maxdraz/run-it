using UnityEngine;

namespace RunIt.UI
{
    public abstract class Displayer : MonoBehaviour
    {
        [SerializeField] protected GameObject toDisplayGO;
        protected abstract void Display();
    }
}