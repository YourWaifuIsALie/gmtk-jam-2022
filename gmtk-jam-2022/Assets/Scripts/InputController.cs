using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public PlayerInput playerInput;

    void Start()
    {
        playerInput.actions["Left"].started += InputLeft;
        playerInput.actions["Left"].canceled += InputLeft;
        playerInput.actions["Right"].started += InputRight;
        playerInput.actions["Right"].canceled += InputRight;
        playerInput.actions["Up"].started += InputUp;
        playerInput.actions["Up"].canceled += InputUp;
        playerInput.actions["Reset"].started += InputReset;
        playerInput.actions["Reset"].canceled += InputReset;
        playerInput.actions["Click"].performed += InputClick;

    }

    private void OnDestroy()
    {
        playerInput.actions["Left"].started -= InputLeft;
        playerInput.actions["Left"].canceled -= InputLeft;
        playerInput.actions["Right"].started -= InputRight;
        playerInput.actions["Right"].canceled -= InputRight;
        playerInput.actions["Up"].started -= InputUp;
        playerInput.actions["Up"].canceled -= InputUp;
        playerInput.actions["Reset"].started -= InputReset;
        playerInput.actions["Reset"].canceled -= InputReset;
        playerInput.actions["Click"].performed -= InputClick;
    }

    private void InputLeft(InputAction.CallbackContext context)
    {
        // Debug.Log($"LEFT");
        if (context.started)
            EventController.current.KeyEvent($"{EventController.LEFT}", $"{EventController.PRESS}");
        else
            EventController.current.KeyEvent($"{EventController.LEFT}", $"{EventController.RELEASE}");
    }

    private void InputRight(InputAction.CallbackContext context)
    {
        // Debug.Log($"RIGHT");
        if (context.started)
            EventController.current.KeyEvent($"{EventController.RIGHT}", $"{EventController.PRESS}");
        else
            EventController.current.KeyEvent($"{EventController.RIGHT}", $"{EventController.RELEASE}");
    }
    private void InputUp(InputAction.CallbackContext context)
    {
        // Debug.Log($"UP");
        if (context.started)
            EventController.current.KeyEvent($"{EventController.UP}", $"{EventController.PRESS}");
        else
            EventController.current.KeyEvent($"{EventController.UP}", $"{EventController.RELEASE}");
    }
    private void InputReset(InputAction.CallbackContext context)
    {
        // Debug.Log($"RESET");
        if (context.started)
            EventController.current.KeyEvent($"{EventController.RESET}", $"{EventController.PRESS}");
        else
            EventController.current.KeyEvent($"{EventController.RESET}", $"{EventController.RELEASE}");
    }

    private void InputClick(InputAction.CallbackContext context)
    {
        // Debug.Log($"InputClick");
        var collidedObject = CastMouseRay();
        if (collidedObject)
            EventController.current.ClickEvent($"{collidedObject.name}");
    }

    private GameObject CastMouseRay()
    {
        RaycastHit collisionResult;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()), out collisionResult, Mathf.Infinity, LayerMask.GetMask("Clickable")))
        {
            return collisionResult.transform.gameObject.transform.root.gameObject;
        }

        return null;
    }
}
