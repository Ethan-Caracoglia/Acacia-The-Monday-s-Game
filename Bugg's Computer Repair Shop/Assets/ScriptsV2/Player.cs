using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // Stores conversion ratios for pixels to Unity units
    #region Constants
    private const int PIXELS_TO_UNITY_UNITY_UNITS_RATIO = 100;
    #endregion

    #region Fields
    #region Static Fields
    // Do we even need this to be static if we only have one player?
    private static CollisionSorter collisionSorter = new CollisionSorter();
    #endregion

    #region Non-Static Fields
    [SerializeField] private Camera mainCamera;
    private MState mouseState;
    private bool[] MBPressed;
    private bool[] MBReleased;
    private Vector2 mousePos;
    private ContactFilter2D contactFilter = new ContactFilter2D();
    // This makes a compiler warning go away because we are allowing this variable to accept a
    // value of null with the '?'
    #nullable enable
    private MoveableObject? currentHeldObj;
    #nullable disable
    // ^^^ Don't worry about this
    #endregion
    #endregion

    #region Properties
    // Returns the id of the held object
    private string heldObjId
    {
        get
        {
            if (currentHeldObj == null)
            {
                return MoveableObject.EMPTY_OBJ_ID;
            }

            return currentHeldObj.id;
        }
    }

    // Checks whether the player is holding an object or not
    private bool holdingObj
    {
        get
        {
            return currentHeldObj != null;
        }
    }
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
    // To be called on every update so that the reciever is getting up to date information
    public MState GetMouseState()
    {
        mouseState = new MState(MBPressed, MBReleased, mousePos);
        return mouseState;
    }

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
                //topObj.TryMouseInput(mouseState);
            }
        }
    }

    // TDOO: Rewrite to be clearer, 
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
            currentHeldObj.HeldUse(MouseButton.MouseLeft, ctx.performed);
        }
        IInteractable topObj = GetTopCollision();
        if (topObj != null)
        {
            // Change MouseState to up / down
            InteractionState state = new InteractionState(heldObjId, MouseButton.MouseLeft, MouseState.MouseDown, this);
            topObj.TryMouseInput(state);
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

    public void QuitGame(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) Application.Quit();
    }
    #endregion
}
