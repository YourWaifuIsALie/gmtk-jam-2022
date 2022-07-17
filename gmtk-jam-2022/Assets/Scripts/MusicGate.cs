using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicGate : MonoBehaviour
{
    [SerializeField]
    private int musicIndex;

    [SerializeField]
    private GameObject player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
            EventController.current.MusicEvent($"{musicIndex}", $"{EventController.PLAY}");
    }
}
