using System.Collections.Generic;
using UnityEngine;

public class CollisionTracker : MonoBehaviour
{
    private List<GameObject> collidingObjects;

    private void Start()
    {
        collidingObjects = new List<GameObject>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.layer);
        if (other.gameObject != gameObject.transform.root && other.gameObject.layer == LayerMask.NameToLayer("PlayerCollision"))
            collidingObjects.Add(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        collidingObjects.Remove(other.gameObject);
    }

    public bool isColliding()
    {
        return collidingObjects.Count > 0 ? true : false;
    }
}