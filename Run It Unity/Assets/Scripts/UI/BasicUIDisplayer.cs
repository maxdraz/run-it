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
        [SerializeField] private TextMeshProUGUI textMesh;
        
        private void Awake()
        {
            textMesh = GetComponent<TextMeshProUGUI>();
            uiDisplayable = toDisplay.GetComponent<IUIDisplayable>();
        }

        private void OnEnable()
        {
            uiDisplayable.ValueChanged += OnValueChanged;
        }

        private void OnDisable()
        {
            uiDisplayable.ValueChanged -= OnValueChanged;
        }

        private void Start()
        {
            var value = uiDisplayable.GetValue();
            textMesh.text = text + value.ToString();
        }

        private void OnValueChanged(object value)
        {
            textMesh.text = text + value.ToString();
        }
    }
}