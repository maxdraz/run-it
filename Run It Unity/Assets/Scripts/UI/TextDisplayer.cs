using System;
using TMPro;
using UnityEngine;

namespace RunIt.UI
{
    public class TextDisplayer : Displayer
    {
        private TextMeshProUGUI textMesh;
        private ITextDisplayable toDisplay;
        [SerializeField] private string className;
        [SerializeField] private bool updateContinuously;
       
        private void Awake()
        {
            textMesh = GetComponent<TextMeshProUGUI>();
            var toDisplayArray = toDisplayGO.GetComponents<ITextDisplayable>();

            for (int i = 0; i < toDisplayArray.Length; i++)
            {
                var classNameRaw = toDisplayArray[i].ToString().Split('.');
                var className = classNameRaw[classNameRaw.Length - 1].Trim(')');
               
                if (className == this.className)
                {
                    toDisplay = toDisplayArray[i]; 
                    break;
                }
            }
            

        }
        private void Update()
        {
            if(!updateContinuously) return;
            
            Display();
        }

        private void OnEnable()
        {
            toDisplay.ValueChanged += Display;
        }

        private void OnDisable()
        {
           toDisplay.ValueChanged -= Display;
        }

        protected override void Display()
        {
            var text = toDisplay.GetDisplayText();
            textMesh.text = text;
        }
    }
}