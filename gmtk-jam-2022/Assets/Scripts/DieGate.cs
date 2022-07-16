using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieGate : MonoBehaviour
{
    private bool isOpen;
    private Vector2 closePosition;
    private Vector2 openPosition;
    private float speed;
    private float openTime;
    void Start()
    {
        isOpen = false;
        speed = 1;
        closePosition = gameObject.transform.position;
    }

    void Update()
    {
        if (isOpen)
        {
            var distance = (Time.time - openTime) * speed;
            var fraction = distance / (closePosition - openPosition).magnitude;
            gameObject.transform.position = Vector2.Lerp(closePosition, openPosition, fraction);
        }
    }

    public void Open()
    {
        isOpen = true;
        openPosition = closePosition + new Vector2(0, 10);
        openTime = Time.time;
    }
}
