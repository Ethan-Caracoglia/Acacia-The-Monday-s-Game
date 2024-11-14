using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    #nullable enable
    #region Fields
    #region private
    private CollisionSorter collisionSorter;
    private MoveableObj? heldObj;
    private PlayerState player;
    private Vector2 mousePos;
    private ContactFilter2D contactFilter;
    private bool[] MBPressed;
    private bool[] MBReleased;
    #endregion
    #endregion

    #region Methods
    #region private
    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Confined;
        //Cursor.visible = false;

        collisionSorter = new CollisionSorter();
        heldObj = null;
        mousePos = Vector2.zero;
        contactFilter = new ContactFilter2D();
        MBPressed = new bool[2];
        MBReleased = new bool[2];

        player = new PlayerState(MBPressed, MBReleased, mousePos, heldObj);  
    }

    // Finds the top Z object to interact with, and ignores all others.
    private ObjInterface? GetTopCollision()
    {
        List<Collider2D> results = new();
        Physics2D.OverlapPoint(transform.position, contactFilter.NoFilter(), results);
        results.Sort(collisionSorter);
        foreach (Collider2D col in results)
        {
            if (col == null) continue;

            ObjInterface obj = col.gameObject.GetComponent<ObjInterface>();
            if (obj == null) continue;
            return obj;
        }
        return null;
    }

    private bool IsHoldingObj()
    {
        if (heldObj == null)
        {
            return false;
        }

        return true;
    }

    // Methods callable by the held objects
    private bool GrabObj(ObjInterface? obj)
    {
        MoveableObj? mObj = obj as MoveableObj;
        if (mObj == null)
        {
            return false;
        }
        
        heldObj = mObj;
        heldObj.PickUpObj(player);
        return true;
    }

    // Probably not the optimal way to do this
    private void DropObj()
    {
        heldObj?.SetDownObject();
        heldObj = null;
    }
    #endregion

    #region public
    public void OnMouseMove(InputAction.CallbackContext ctx)
    {
        mousePos = Camera.main.ScreenToWorldPoint(ctx.ReadValue<Vector2>());
        transform.position = mousePos;

        // Update the location of a held object
        if (IsHoldingObj())
        {
            heldObj?.UpdateMousePosition(mousePos);
        }

        player = new PlayerState(MBPressed, MBReleased, mousePos, heldObj);
    }

    public void OnMousePrimary(InputAction.CallbackContext ctx)
    {
        // Adjust all of the buttons accordingly
        if (ctx.performed)
        {
            MBPressed[0] = true;
            MBReleased[0] = false;
        }
        else if (ctx.canceled)
        {
            MBPressed[0] = false;
            MBReleased[0] = true;
        }
        else
        {
            MBPressed[0] = false;
            MBReleased[0] = false;
        }

        if (IsHoldingObj() && ctx.canceled)
        {
            DropObj();
        }
        else if (!IsHoldingObj() && ctx.performed)
        {
            ObjInterface? obj = GetTopCollision();
            GrabObj(obj);
        }

        player = new PlayerState(MBPressed, MBReleased, mousePos, heldObj);
    }

    public void OnMouseSecondary(InputAction.CallbackContext ctx)
    {
        // Adjust all of the buttons accordingly
        if (ctx.performed)
        {
            MBPressed[1] = true;
            MBReleased[1] = false;
        }
        else if (ctx.canceled)
        {
            MBPressed[1] = false;
            MBReleased[1] = true;
        }
        else
        {
            MBPressed[1] = false;
            MBReleased[1] = false;
        }

        // Use the object being held
        if (IsHoldingObj())
        {
            heldObj?.GetInput(player);
        }

        player = new PlayerState(MBPressed, MBReleased, mousePos, heldObj);
    }

    public void QuitGame(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Application.Quit();
        }
    }

    public void RestartGame(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    #endregion
    #endregion
}