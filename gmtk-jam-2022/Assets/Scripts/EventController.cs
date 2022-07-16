using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    public static EventController current;
    public static string LEFT = "left";
    public static string RIGHT = "right";
    public static string UP = "up";
    public static string RESET = "reset";
    public static string RELEASE = "release";
    public static string PRESS = "press";

    private void Awake()
    {
        current = this;
    }


    public event Action<string> onClickEvent; // The object to add the callback method to
    public void ClickEvent(string id) // The method to call to invoke the event
    {
        if (onClickEvent != null)
        {
            onClickEvent(id);
        }
    }

    public event Action<string, string> onKeyEvent;
    public void KeyEvent(string id, string action)
    {
        if (onKeyEvent != null)
        {
            onKeyEvent(id, action);
        }
    }
}
