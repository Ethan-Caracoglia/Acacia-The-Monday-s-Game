using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveMouse : MonoBehaviour
{
    private const int PIXELS_TO_UNITY_UNITY_UNITS_RATIO = 100;
    [SerializeField] Camera cam;
    float height;
    float width;

    private static CollisionSorter collisionSorter = new CollisionSorter();
    // TODO: Store current mouse state

    private MoveableObject? currentHeldObj;
    private string heldObjId
    {
        get
        {
            if (currentHeldObj == null)
                return MoveableObject.EMPTY_OBJ_ID;
            return currentHeldObj.id;
        }
    }
    private bool holdingObj
    {
        get
        {
            return currentHeldObj != null;
        }
    }


    private ContactFilter2D c = new ContactFilter2D();
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        height = 2f * cam.orthographicSize;
        width = height * cam.aspect;
    }

    private void Update()
    {
        
    }

    public void UpdateMouse(InputAction.CallbackContext ctx)
    {
        Vector2 mousePos = ctx.ReadValue<Vector2>();

        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10));
        if (holdingObj)
        {
            currentHeldObj.UpdateMousePosition(transform.position);
            // How update about held down
            IInteractable topObj = GetTopCollision();
            if (topObj != null)
            {
                //TODO : Figure out the CURRENT MOUSE STATE
                InteractionState state = new InteractionState(heldObjId,  MouseButton.MouseMovement, MouseState.Held, this);
                topObj.TryMouseInput(state);
            }
        }
    }

    // Finds the top Z object to interact with, and ignores all others.
    private IInteractable GetTopCollision()
    {
        Collider2D[] results = new Collider2D[8];
        Physics2D.OverlapPoint(transform.position, c.NoFilter(), results);
        List<Collider2D> resultList = new List<Collider2D>(results);
        resultList.Sort(collisionSorter);
        foreach (Collider2D col in resultList)
        {
            if (col == null) continue;

            IInteractable obj = col.gameObject.GetComponent<IInteractable>();
            if (obj == null) continue;
            return obj;
        }
        return null;
    }

    // TDOO: Rewrite to be clearer, 
    public void GetMouseDown(InputAction.CallbackContext ctx)
    {
        if (holdingObj)
        {
            currentHeldObj.HeldUse(MouseButton.MouseLeft, ctx.performed);
        }
        IInteractable topObj = GetTopCollision();
        if (topObj != null)
        {
            // Change MouseState to up / down
            InteractionState state = new InteractionState(heldObjId, MouseButton.MouseLeft, MouseState.MouseDown, this);
            topObj.TryMouseInput(state);
        }
    }

    // Methods callable by the held objects
    #region HeldObjectCallbacks
    public bool TrySetCurrentHeldObj(MoveableObject obj)
    {
        if (holdingObj) return false;

        currentHeldObj = obj;
        obj.Holder = this;
        return true;
    }

    // Probably not the optimal way to do this
    public void SetDownObj()
    {
        currentHeldObj = null;
    }

    #endregion
}
