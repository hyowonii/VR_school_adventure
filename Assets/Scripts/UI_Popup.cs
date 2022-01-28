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
        public InputField roomName;

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

            Debug.Log(roomName.text);
            roomName.text = gameObject.name;
            Debug.Log(gameObject.name);
            Debug.Log(roomName.text);
            Time.timeScale = 0;
            PhotonNetwork.MinimalTimeScaleToDispatchInFixedUpdate = 0;
        }
    }
}
