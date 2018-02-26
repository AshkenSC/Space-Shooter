using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SimpleFireButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    private bool touched;
    private int pointerID;
    private bool canFire;

    void Awake()
    {
        touched = false;
    }

    public void OnPointerDown(PointerEventData data)
    // Set our start point
    // There is no certain position of joystick
    // the first place player touches is the center of the joystick
    {
        if (!touched)
        {
            touched = true;
            pointerID = data.pointerId;
            canFire = true;
        }
    }

    public void OnPointerUp(PointerEventData data)
    // Reset everything
    {
        if (data.pointerId == pointerID)
        {
            canFire = false;
            touched = false;
        }
    }

    public bool CanFire()
    {
        return canFire;
    }
}
