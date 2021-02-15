using TMPro;
using UnityEngine;

namespace RunIt.UI
{
    public class FloatDisplayer : MonoBehaviour, IDisplayer, ITMPDisplayer
    {
        public void Display()
        {
            throw new System.NotImplementedException();
        }

        public TextMeshProUGUI TextMesh { get; set; }
        
    }
}