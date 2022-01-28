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
        
    }
    
    void Update()
    {
       /*if (IsActive && Lever.GetComponentInChildren<Lever>().GetCurrentState())
       {
   
       }*/
    }

    public void ChangeRoomActive(bool b)
    {
        GameObject.Find("Pedestal").GetComponent<Pedestal>().StartMovement(b);
        gameObject.SetActive(b);
        IsActive = b;
    }
}