using System;
using System.Collections;
using System.Collections.Generic;
using _OpenXR.Scripts;
using UnityEngine;

public class RoomFunctions : MonoBehaviour
{
    public GameObject Lever;

    public bool IsActive = true;

    // Start is called before the first frame update
    void Start()
    {
        Lever.transform.eulerAngles = new(40, 0, 0);
        //gameObject.SetActive(IsActive);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsActive && Math.Abs(Lever.transform.GetChild(0).position.y - 1.2) < 0.01f)
        {
            Debug.Log("DISABLE ROOOOOM");
            GameObject.Find("Pedestal").GetComponent<Pedestal>().StartMovement();
            gameObject.SetActive(false);
            IsActive = false;
        }
    }
}