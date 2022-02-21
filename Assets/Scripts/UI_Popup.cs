using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Realtime;
using UnityEngine.UI;

namespace Photon.Pun.Demo.PunBasics
{
    public class UI_Popup : MonoBehaviour
    {
        private GameObject UI;
        private InputField roomName;

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
            Debug.Log("start");

            UI = gameObject.transform.GetChild(0).gameObject;
            UI.SetActive(true);

            Debug.Log(UI);

            roomName = gameObject.transform.Find("UI_JoinRoom/UI controls/Room InputField").GetComponent<InputField>();
            roomName.text = gameObject.name;
            
            //Time.timeScale = 0;
            //PhotonNetwork.MinimalTimeScaleToDispatchInFixedUpdate = 0;
        }
    }
}
