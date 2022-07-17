using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnGate : MonoBehaviour
{
    [SerializeField]
    private GameObject respawnObject;
    [SerializeField]
    private GameObject respawnLocation;

    [SerializeField]
    public GameObject player;

    [SerializeField]
    private GameObject transporter = null;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player && respawnObject.transform.position != respawnLocation.transform.position)
            respawnObject.transform.position = respawnLocation.transform.position;

        if (transporter)
            transporter.SetActive(true);
    }
}
