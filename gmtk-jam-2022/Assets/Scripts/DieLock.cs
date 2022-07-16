using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DieLock : MonoBehaviour
{
    [SerializeField]
    public int value;

    [SerializeField]
    private TextMeshPro textGraphic;

    [SerializeField]
    private DieGate gate;

    private bool isLocked;
    void Start()
    {
        isLocked = true;
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collided)
    {
        var collidedKey = collided.GetComponent<DieKey>();
        if (collidedKey)
        {
            if (collidedKey.value == this.value)
            {
                isLocked = false;
                textGraphic.text = "UNLOCKED";
                gate.Open();
            }
        }
    }
}
