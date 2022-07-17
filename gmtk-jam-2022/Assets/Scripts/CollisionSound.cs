using System.Collections.Generic;
using UnityEngine;

public class CollisionSound : MonoBehaviour
{
    void Start()
    {
    }

    /*
    private void OnTriggerEnter2D()
    {
        EventController.current.SoundEvent($"{ConfigController.SOUNDCOLLISION}", $"{EventController.PLAY}", 1);
    }
    */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        float audioLevel = Mathf.Clamp(collision.relativeVelocity.magnitude / 10.0f, 0.0f, 5.0f);
        EventController.current.SoundEvent($"{ConfigController.SOUNDCOLLISION}", $"{EventController.PLAY}", audioLevel, false);
    }
}