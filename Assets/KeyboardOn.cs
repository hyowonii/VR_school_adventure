using System;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace VRKeys
{
    public class KeyboardOn : MonoBehaviour
    {
        public Keyboard keyboard;

        private void OnEnable()
        {
            keyboard.Enable();
            keyboard.SetPlaceholderMessage("Please enter your name");

            keyboard.OnUpdate.AddListener(HandleUpdate);
            
        }

        private void OnDisable()
        {
            keyboard.OnUpdate.RemoveListener(HandleUpdate);

            keyboard.Disable();
        }

        public void HandleUpdate(string text)
        {
            keyboard.HideValidationMessage();
        }
    }
}

