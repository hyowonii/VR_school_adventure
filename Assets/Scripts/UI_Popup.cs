using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Realtime;


namespace Photon.Pun.Demo.PunBasics
{
    public class UI_Popup : MonoBehaviour
    {
        public GameObject UI;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerEnter()
        {
            UI.SetActive(true);
            //Time.timeScale = 0;
        }
    }
}
