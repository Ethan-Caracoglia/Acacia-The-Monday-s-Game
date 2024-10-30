using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Split into "snapping and non-snapping objects: would be nice to move objects on top of others. Not neccessary though.
public class MvObject : ObjIntrf
{
    // Public Fields
    [SerializeField] protected SpriteRenderer objSprite;
    public Sprite sprite;
    public Sprite highlightSprite;
    public MoveMouse Holder;
    public bool snapped = true;

    // Protected Fields
    protected bool dragging = false;
    protected Vector3 offset;
    protected Vector3 snapPosition;
    protected Collider2D objCollider;
    [SerializeField] protected float snapDistance = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        objCollider = GetComponent<Collider2D>();

        snapPosition = transform.position;
        foreach (var mObj in childrenObjs)
        {
            offsets.Add(mObj.id, mObj.transform.position - transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (dragging)
        {
            // Add wiggle, if wanted

        }
    }


    // Might need to be changed
    // Ethan: Don't change this for now since it just works 
    void OnMouseOver()
    {
        objSprite.sprite = highlightSprite;
    }

    void OnMouseExit()
    {
        objSprite.sprite = sprite;
    }

    public override void ParentPositionChange(Vector3 newPos)
    {
        snapPosition = newPos;
        if (snapped)
        {
            UpdatePosition(snapPosition);
        }
    }

    public override void UpdateMousePosition(Vector3 MousePos)
    {
        UpdatePosition(MousePos + offset);
    }

    public override void TryMouseInput(InteractionState state)
    {
        MoveObj(state);
    }

    /// <summary>
    /// Responsible for the default movement of moveable Objects
    /// </summary>
    /// <param name="state">Mouse Input State</param>
    private void MoveObj(InteractionState state)
    {
        if (state.Button == MouseButton.MouseLeft && state.ObjId == EMPTY_OBJ_ID)
        {
            // Pick up Object
            if (state.MouseAction == MouseState.MouseDown)
            {
                PickUpObject(state.MousePos, state.Sender);
            }
        }
    }

    /// <summary>
    /// Picks up an object when the mouse is at v
    /// </summary>
    /// <param name="v">PickUpObject</param>
    /// <param name="m">PickUpObject</param>
    protected bool PickUpObject(Vector3 v, MoveMouse sender)
    {

        if (!sender.TrySetCurrentHeldObj(this)) return false;

        // Disable collider when picked up so the click propegates properly
        objCollider.enabled = false;

        dragging = true;
        offset = transform.position - v;
        snapped = false;
        return true;
    }

    /// <summary>
    /// Sets down an object
    /// </summary>
    public void SetDownObject()
    {
        Debug.Log("Drop Obj");
        if (dragging == false)
            return;


        objCollider.enabled = true;
        Holder.SetDownObj();
        dragging = false;

        if ((transform.position - snapPosition).sqrMagnitude <= snapDistance)
        {
            UpdatePosition(snapPosition);
            snapped = true;
        }

    }


    /// <summary>
    /// When held, accepts mouse input. Override when needed.
    /// </summary>
    /// <param name="button">Button Pressed</param>
    public virtual void HeldUse(MouseButton button, bool drag)
    {
        if (drag)
            SetDownObject();
    }


}
