using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_control : MonoBehaviour
{
    public float speed = 5;
    CharacterController mainplayer;

    void Start()
    {
        mainplayer = GetComponent<CharacterController>();
    }

    void Update()
    {
        // 1. ������� �Է¿� ����
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // 2. �յ� �¿�� ������ �����.
        Vector3 dir = Vector3.right * h + Vector3.forward * v;

        //ī�޶� �����ִ� ������ �� �������� �����Ѵ�.
        dir = Camera.main.transform.TransformDirection(dir);
        //���ý����̽����� ���彺���̽��� ��ȯ ���ش�. (Ʈ������ �������� ����� �ٲ۴�.)

        //�밢�� �̵����� �ϸ鼭 ��Ʈ2�� ���̰� �þ�⿡ 1�� ������ش�. (����ȭ:Normalize)
        //dir.Normalize();

        // 3. �� �������� �̵��Ѵ�.
        // P = P0 + vt
        mainplayer.Move(dir * speed * Time.deltaTime);
    }
}