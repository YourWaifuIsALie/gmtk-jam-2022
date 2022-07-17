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
    public static string FLY = "fly";
    public static string RESET = "reset";
    public static string RELEASE = "release";
    public static string PRESS = "press";
    public static string PLAY = "play";
    public static string STOP = "stop";

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

    public event Action<string, string, float, bool> onSoundEvent;
    /// <summary>
    /// Send sound event. level is audio level. If wait, audio won't start if already playing
    /// </summary>
    public void SoundEvent(string id, string action, float level, bool wait)
    {
        if (onSoundEvent != null)
        {
            onSoundEvent(id, action, level, wait);
        }
    }

    public event Action<string, string> onMusicEvent;
    public void MusicEvent(string id, string action)
    {
        if (onMusicEvent != null)
        {
            onMusicEvent(id, action);
        }
    }
}
