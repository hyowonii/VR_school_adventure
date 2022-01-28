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
        
        // 1. ������� �Է¿� ����
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // 2. �յ� �¿�� ������ �����.
        dir = Vector3.right * h + Vector3.forward * v;


        //ī�޶� �����ִ� ������ �� �������� �����Ѵ�.
        //dir = Camera.main.transform.TransformDirection(dir);
        //���ý����̽����� ���彺���̽��� ��ȯ ���ش�. (Ʈ������ �������� ����� �ٲ۴�.)
        //dir = Camera.main.transform.forward;
        //�밢�� �̵����� �ϸ鼭 ��Ʈ2�� ���̰� �þ�⿡ 1�� ������ش�. (����ȭ:Normalize)
        dir.Normalize();

        // 3. �� �������� �̵��Ѵ�.
        // P = P0 + vt

        transform.forward = myCamera.transform.forward;
        this.transform.Translate(dir * speed * Time.deltaTime);

        //1. ���콺 �Է� ���� �̿��Ѵ�.
        float mx = Input.GetAxis("Mouse X"); //����â���� ���콺�� ���� ���������� �̵��Ҷ� ���� (�� -���� : ���� +���)
        float my = Input.GetAxis("Mouse Y"); //����â���� ���콺�� �� �Ʒ��� �̵��Ҷ� ���� (�Ʒ� -���� : �� +���)

        rx += rotSpeed * my * Time.deltaTime;
        ry += rotSpeed * mx * Time.deltaTime;

        //rx ȸ�� ���� ���� (ȭ�� ������ ���콺�� �������� x�� ȸ�� ���� �ϵ� ��� ���� ���� ����)
        rx = Mathf.Clamp(rx, -40, 40);
        //x�� ������ ���� x���� �̵��� �ƴ϶� x���� ȸ�� �ؼ� ���Ʒ� ���� ������ x���̿��� �Ѵ�.

        //2. ȸ���� �Ѵ�.
        transform.rotation = Quaternion.Euler(0, ry, 0);
        myCamera.transform.localRotation = Quaternion.Euler(-rx, 0, 0);
        //transform.eulerAngles = new Vector3(-rx, ry, 0);
        //X���� ȸ���� ����� �����Ǹ� �Ʒ�, ������ �����Ǹ� ���� ���ư���. (�׷��� x���� -�� �־���)

    }
}