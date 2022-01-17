using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject[] playerPrefabs;
    private GameObject player;
    public GameObject camera;

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
        player = GameObject.CreatePrimitive(PrimitiveType.Cube);
        player.transform.position = new Vector3(38, 1, -27);
        player.transform.localScale = new Vector3(0.2f, 1.5f, 0.2f);

        player.AddComponent<Rigidbody>();
        player.GetComponent<Rigidbody>().isKinematic = true;

        Camera.main.transform.SetParent(player.transform);
        player.AddComponent<Player_control>();

        PhotonNetwork.Disconnect();

    }
}
