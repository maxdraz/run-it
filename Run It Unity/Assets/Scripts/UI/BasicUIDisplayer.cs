using System;
using TMPro;
using UnityEngine;

namespace RunIt.UI
{
    public class BasicUIDisplayer : MonoBehaviour
    {
        [SerializeField] private GameObject toDisplay;
        private IUIDisplayable uiDisplayable;
        [SerializeField] private string text;
        private TextMeshProUGUI textMesh;
        
        private void Awake()
        {
            textMesh = GetComponent<TextMeshProUGUI>();
            if(!toDisplay) return;
            uiDisplayable = toDisplay.GetComponent<IUIDisplayable>();
        }

        private void OnEnable()
        {
            if (uiDisplayable == null) return;
            uiDisplayable.ValueChanged += OnValueChanged;
        }

        private void OnDisable()
        {   if (uiDisplayable == null) return;
            uiDisplayable.ValueChanged -= OnValueChanged;
        }

        private void Start()
        {
            if (uiDisplayable == null) return;
            var value = uiDisplayable.GetValue();
            textMesh.text = text + value.ToString();
        }

        private void OnValueChanged(object value)
        {
            textMesh.text = text + value.ToString();
        }

        public void SetText(string txt)
        {
            textMesh.text = txt;
        }
    }
}