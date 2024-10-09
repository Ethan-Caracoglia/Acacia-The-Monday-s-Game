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

    private bool holdingTool = false;
    private IInteractable currentHeldObj;

    private ContactFilter2D c = new ContactFilter2D();
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        height = 2f * cam.orthographicSize;
        width = height * cam.aspect;
    }

    public void UpdateMouse(InputAction.CallbackContext ctx)
    {
        Vector2 mousePos = ctx.ReadValue<Vector2>();

        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10));
    }

    public void GetMouseDown(InputAction.CallbackContext ctx)
    {
        Collider2D[] results = new Collider2D[] { };
        Physics2D.OverlapPoint(transform.position, c.NoFilter(), results);

        List<Collider2D> resultList = new List<Collider2D>(results);
        resultList.Sort(collisionSorter);

        foreach (Collider2D col in resultList)
        {
            IInteractable? obj = col.gameObject.GetComponent<IInteractable>();
            if (obj == null)
                continue;
            InteractionState state = new InteractionState(currentHeldObj., currentHeldObj.)
            obj.TryInteract();
        }

    }


    public bool TrySetCurrentHeldObj(IInteractable obj)
    {
        if (holdingObj) return false;

        currentHeldObj = obj;
        holdingObj = true;
        return true;
    }

    // Probably not the optimal way to do this
    public void SetDownObj()
    {
        holdingObj = false;
    }

}
