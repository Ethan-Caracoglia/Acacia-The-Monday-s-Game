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
    private CollisionSorter collisionSorter;
    private ContactFilter2D contactFilter;
    #endregion

    #region Properties
    public WorldObject? HeldObj { get; private set; }
    public Vector3 MousePos { get; private set; }
    public bool[] MBPressed { get; private set; }
    public bool[] MBReleased { get; private set; }
    public bool[] MBHeld { get; private set; }

    public bool IsHoldingObj
    {
        get
        {
            if (HeldObj == null)
            {
                return false;
            }

            return true;
        }
    }
    #endregion

    #region Methods
    #region private
    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Confined;
        //Cursor.visible = false;

        collisionSorter = new CollisionSorter();
        HeldObj = null;
        MousePos = Vector2.zero;
        contactFilter = new ContactFilter2D();
        MBPressed = new bool[2];
        MBReleased = new bool[2];
        MBHeld = new bool[2];

        InputAction quitAction = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/q");
        quitAction.performed += QuitGame;
        quitAction.Enable();
    }

    /// <summary>
    /// Displays the player input
    /// </summary>
    private void DisplayInput()
    {
        Debug.Log($"");
    }

    /// <summary>
    /// Finds the top Z object to interact with, and ignores all others.
    /// </summary>
    /// <returns></returns>
    private WorldObject? GetTopCollision()
    {
        List<Collider2D> results = new List<Collider2D>();
        Physics2D.OverlapPoint(transform.position, contactFilter.NoFilter(), results);

        results.Sort(collisionSorter);

        foreach (Collider2D col in results)
        {
            if (col != null)
            {
                WorldObject obj = col.gameObject.GetComponent<WorldObject>();
                return obj;
            }
        }

        return null;
    }

    private void GrabObj(WorldObject obj)
    {
        HeldObj = obj;
        HeldObj.PickedUp(this);
    }

    // Probably not the optimal way to do this
    private void DropObj()
    {
        HeldObj?.SetDown();
        HeldObj = null;
    }
    #endregion

    #region public
    public void OnMouseMove(InputAction.CallbackContext ctx)
    {
        // Prevents the error from occuring when camera is null
        if (Camera.main == null)
        {
            return;
        }
        MousePos = Camera.main.ScreenToWorldPoint(ctx.ReadValue<Vector2>());
        transform.position = MousePos;

        // Update the location of a held object
        if (IsHoldingObj)
        {
            HeldObj!.Move(MousePos);
        }
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

        if (IsHoldingObj && ctx.performed)
        {
            DropObj();
        }
        else if (!IsHoldingObj && ctx.performed)
        {
            WorldObject? obj = GetTopCollision();
            if (obj != null)
            {
                GrabObj(obj);
            }
        }
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
        if (IsHoldingObj)
        {
            HeldObj.GetInput(this);
        }
    }

    public void QuitGame(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Debug.Log("Quitting the game...");
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