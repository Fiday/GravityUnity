using System;
using System.Collections;
using System.Collections.Generic;
using _OpenXR.Scripts;
using UnityEngine;

public class RoomFunctions : MonoBehaviour
{
    public bool IsActive = true;

    public void ChangeRoomActive(bool b)
    {
        GameObject.Find("Pedestal").GetComponent<Pedestal>().StartMovement(b);
        gameObject.SetActive(b);
        IsActive = b;
    }
}