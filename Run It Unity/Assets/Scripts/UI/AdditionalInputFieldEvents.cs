using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace RunIt.UI
{
    public class AdditionalInputFieldEvents : MonoBehaviour
    {
        private TMP_InputField field;
        [SerializeField] private int minCharacterCount = 3;
        [SerializeField] private UnityEvent OnMinInput;
        [SerializeField] private UnityEvent OnBelowMinInput;

        private void Awake()
        {
            field = GetComponent<TMP_InputField>();
        }

        private void OnEnable()
        {
          //  CheckIfMinCharacterCount(field.text);
        }

        public void CheckIfMinCharacterCount(string txt)
        {
            if (txt.Length < minCharacterCount) 
                OnBelowMinInput.Invoke();
            else
                OnMinInput.Invoke();
        }
    }
}