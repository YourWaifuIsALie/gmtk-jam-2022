using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceTransporter : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> dice;

    [SerializeField]
    public GameObject[] respawnLocations;

    void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (dice.Contains(other.gameObject))
        {
            int index = dice.IndexOf(other.gameObject);
            other.transform.position = respawnLocations[index].transform.position;
            other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            other.GetComponent<Rigidbody2D>().AddTorque(360);
        }
    }
}
