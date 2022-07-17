using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Farsight : MonoBehaviour
{
    [SerializeField]
    public CinemachineVirtualCamera camera;

    [SerializeField]
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            player.GetComponent<PlayerTurtle>().isCameraLocked = true;
            player.GetComponent<PlayerTurtle>().isCameraZoomed = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            player.GetComponent<PlayerTurtle>().isCameraLocked = false;
            player.GetComponent<PlayerTurtle>().isCameraZoomed = true;
        }
    }
}
