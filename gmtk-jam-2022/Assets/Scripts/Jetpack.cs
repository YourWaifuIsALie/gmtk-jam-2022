using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jetpack : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            other.GetComponent<PlayerTurtle>().SetJetpack(true);
            gameObject.SetActive(false);
        }
    }
}
