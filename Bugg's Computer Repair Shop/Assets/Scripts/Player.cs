using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Stores conversion ratios for pixels to Unity units
    #region Constants
    private const int PIXELS_TO_UNITY_UNITY_UNITS_RATIO = 100;
    #endregion

    #region Fields
    [SerializeField] private Camera mainCamera;
    private CollisionSorter collisionSorter = new CollisionSorter();
    private PlayerState player;
    private bool[] MBPressed;
    private bool[] MBReleased;
    private Vector2 mousePos;
    private ContactFilter2D contactFilter = new ContactFilter2D();
    // This makes a compiler warning go away because we are allowing this variable to accept a
    // value of null with the '?'
    #nullable enable
    private MoveableObject? heldObj;
    #nullable disable
    // ^^^ Don't worry about this
    #endregion

    #region Internal Methods
    private void Start()
    {
        // Lock the mouse to the inside of the window
        Cursor.lockState = CursorLockMode.Confined;
        // Hide the cursor
        Cursor.visible = false;
        //Setup each array to allow for the correct number of buttons
        MBPressed = new bool[2];
        MBReleased = new bool[2];
        // Zero the vector to start out
        mousePos = Vector2.zero;
        // Set the current state of the mouse
        mouseState = new MState();  
    }

    // Finds the top Z object to interact with, and ignores all others.
    private IInteractable GetTopCollision()
    {
        Collider2D[] results = new Collider2D[8];
        Physics2D.OverlapPoint(transform.position, contactFilter.NoFilter(), results);
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
    #endregion

    #region External Methods
    public void OnMouseMove(InputAction.CallbackContext ctx)
    {
        // Get the screen coordinate
        mousePos = ctx.ReadValue<Vector2>();
        // Convert it to world coordinate
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        // Set the position of the player to that
        transform.position = mousePos;

        // Update the location of a held object
        if (holdingObj)
        {
            currentHeldObj.UpdateMousePosition(mousePos);
            // How update about held down
            IInteractable topObj = GetTopCollision();
            if (topObj != null)
            {
                topObj.TryMouseInput(mouseState);
            }
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

        // Use the object being held
        if (holdingObj)
        {
            currentHeldObj.HeldUse(mouseState);
        }
        IInteractable topObj = GetTopCollision();
        if (topObj != null)
        {
            // Change MouseState to up / down
            topObj.TryMouseInput(mouseState);
        }

        if (ctx.canceled && currentHeldObj != null)
        {
            currentHeldObj.SetDownObject();
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

    #region Getters
    public PlayerState GetPlayerState()
    {
        return player;
    }

    public MoveableObject GetMoveableObj()
    {
        return heldObj;
    }

    public bool IsHoldingObj()
    {
        if (heldObj == null)
        {
            return false;
        }

        return true;
    }
    #endregion

    #region Setters
    public PlayerState SetPlayerState()
    {
        return new PlayerState(MBPressed, MBReleased, mousePos, heldObj);
    }
    #endregion

    // Quits game on button press
    public void QuitGame(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Application.Quit();
        }
    }

    // Reloads scene on button press
    public void RestartGame(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    #endregion
}