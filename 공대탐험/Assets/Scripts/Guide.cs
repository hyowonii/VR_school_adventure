using UnityEngine;
using System.Collections;

public class Guide : MonoBehaviour
{

    public GameObject guideUI;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            guideUI.SetActive(false);
        }
    }
}
