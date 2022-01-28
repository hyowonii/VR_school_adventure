using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using Photon.Pun;

public class Player_control : MonoBehaviour
{
    public float speed = 5;
    float rx;
    float ry;
    Vector3 dir;
    public float rotSpeed = 500;
    public Camera myCamera;
    public PhotonView photonView;
    public GameObject playerUI;
  
    void Start()
    {
        myCamera = Camera.main;
        float scale = gameObject.transform.lossyScale.y;

        Debug.Log(scale);

        photonView = PhotonView.Get(this);

        if (PhotonNetwork.IsConnected)
        {    
            myCamera.transform.localPosition = new Vector3(0, 1/scale, 0);
        }
       
        gameObject.layer = 3;
        foreach (Transform child in transform)
        {
            child.gameObject.layer = 3;
        }

    }
   
    void Update()
    {
        if (PhotonNetwork.IsConnected && !photonView.IsMine) return;
        
        // 1. 사용자의 입력에 따라
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // 2. 앞뒤 좌우로 방향을 만든다.
        dir = Vector3.right * h + Vector3.forward * v;


        //카메라가 보고있는 방향을 앞 방향으로 변경한다.
        //dir = Camera.main.transform.TransformDirection(dir);
        //로컬스페이스에서 월드스페이스로 변환 해준다. (트렌스폼 기준으로 결과를 바꾼다.)
        //dir = Camera.main.transform.forward;
        //대각선 이동으로 하면서 루트2로 길이가 늘어나기에 1로 만들어준다. (정규화:Normalize)
        dir.Normalize();

        // 3. 그 방향으로 이동한다.
        // P = P0 + vt

        transform.forward = myCamera.transform.forward;
        this.transform.Translate(dir * speed * Time.deltaTime);

        //1. 마우스 입력 값을 이용한다.
        float mx = Input.GetAxis("Mouse X"); //게임창에서 마우스를 왼쪽 오른쪽으로 이동할때 마다 (왼 -음수 : 오른 +양수)
        float my = Input.GetAxis("Mouse Y"); //게임창에서 마우스를 위 아래로 이동할때 마다 (아래 -음수 : 위 +양수)

        rx += rotSpeed * my * Time.deltaTime;
        ry += rotSpeed * mx * Time.deltaTime;

        //rx 회전 각을 제한 (화면 밖으로 마우스가 나갔을때 x축 회전 덤블링 하듯 계속 도는 것을 방지)
        rx = Mathf.Clamp(rx, -40, 40);
        //x을 돌리는 이유 x축이 이동이 아니라 x축을 회전 해서 위아래 보는 방향은 x축이여야 한다.

        //2. 회전을 한다.
        transform.rotation = Quaternion.Euler(0, ry, 0);
        myCamera.transform.localRotation = Quaternion.Euler(-rx, 0, 0);
        //transform.eulerAngles = new Vector3(-rx, ry, 0);
        //X축의 회전은 양수가 증가되면 아래, 음수가 증가되면 위로 돌아간다. (그래서 x축을 -를 넣었다)

    }
}