using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Realtime;
using UnityEngine.UI;

namespace Photon.Pun.Demo.PunBasics
{
    public class UI_Popup : MonoBehaviour
    {
        public GameObject UI;
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
            UI.transform.SetParent(gameObject.transform);
            UI.transform.localPosition = new Vector3(0f, 0.1f, 0f);
            UI.transform.localEulerAngles = new Vector3(0, 90, 0);
            UI.transform.localScale = new Vector3(0.001f, 0.0004f, 1);


            roomName = gameObject.transform.Find("UI_JoinRoom/UI controls/Room InputField").GetComponent<InputField>();
            roomName.text = gameObject.name;
            roomName.tag = gameObject.tag;

            UI.SetActive(true);

            //Time.timeScale = 0;
            //PhotonNetwork.MinimalTimeScaleToDispatchInFixedUpdate = 0;
        }
    }
}