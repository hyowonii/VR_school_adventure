using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject[] playerPrefabs;
    public GameObject player;
    private GameObject me;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void Disconnect()
    {
        me = GameObject.CreatePrimitive(PrimitiveType.Cube);
        me.transform.localScale = new Vector3(0.2f, 1.5f, 0.2f);
        me.transform.SetParent(player.transform);

        player.transform.position = new Vector3(38, 1, -27);
        me.transform.localPosition = new Vector3(0, 0, 0);

        PhotonNetwork.Disconnect();

    }
}