using System;
using TMPro;
using UnityEngine;

namespace RunIt.UI
{
    public class ValueDisplayer : MonoBehaviour
    {
        private TextMeshProUGUI tmp;
        [SerializeField] private Displayable toDisplay;

        private void Awake()
        {
            tmp = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            toDisplay.OnValueChanged += UpdateDisplay;
        }

        private void OnDisable()
        {
            toDisplay.OnValueChanged -= UpdateDisplay;
        }

        private void UpdateDisplay(string name, object value)
        {
            if(!tmp) return;
            
            tmp.text = name + ": " + value + "/6";
        }
    }
}