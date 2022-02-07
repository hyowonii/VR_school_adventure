using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace VRKeys
{
    public class Test_input : MonoBehaviour
    {
        
        public TextMeshProUGUI displayText;


        public void printLog()
        {
            Debug.Log(displayText.text);
        }
    }
}

