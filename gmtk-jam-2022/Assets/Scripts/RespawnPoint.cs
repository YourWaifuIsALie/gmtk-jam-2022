using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    [SerializeField]
    private ParticleSystem sfx;

    private void Start()
    {
        EventController.current.onKeyEvent += KeyEventHandler;
    }

    private void OnDestroy()
    {
        EventController.current.onKeyEvent -= KeyEventHandler;
    }

    private void KeyEventHandler(string id, string action)
    {
        // Debug.Log($"{id}, {action}");
        if (id == EventController.RESET && action == EventController.PRESS)
        {
            target.transform.position = gameObject.transform.position;
            target.GetComponent<Rigidbody2D>().AddTorque(180);  // Spin for the juice?
            sfx.Play();
        }
    }
}
