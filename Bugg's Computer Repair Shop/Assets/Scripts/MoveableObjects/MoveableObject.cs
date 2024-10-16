using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Split into "snapping and non-snapping objects: would be nice to move objects on top of others. Not neccessary though.
public class MoveableObject : ObjInterface
{
    public Sprite sprite;
    public Sprite highlightSprite;

    protected bool dragging = false;
    public MoveMouse Holder;

    public bool snapped = true;

    protected Vector3 offset;
    protected Vector3 snapPosition;

    public ObjInterface[] coveredObjs;
    [SerializeField] protected float snapDistance = 0.01f;


    protected Collider2D objCollider;
    // Start is called before the first frame update
    void Start()
    {
        objCollider = GetComponent<Collider2D>();

        snapPosition = transform.position;
        foreach (var mObj in childrenObjs)
        {
            offsets.Add(mObj.id, mObj.transform.position - transform.position);
        }
        foreach (var mObj in coveredObjs)
        {
            mObj.covered = true;
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
    void OnMouseOver()
    {
        transform.GetComponent<SpriteRenderer>().sprite = highlightSprite;
    }

    void OnMouseExit()
    {
        transform.GetComponent<SpriteRenderer>().sprite = sprite;
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
        if (covered) return false;

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
    protected void SetDownObject()
    {
        if (dragging == false)
            return;


        objCollider.enabled = true;
        Holder.SetDownObj();
        dragging = false;

        if ((transform.position - snapPosition).sqrMagnitude <= snapDistance)
        {
            UpdatePosition(snapPosition);
            snapped = true;
            foreach (var mObj in coveredObjs)
            {
                mObj.covered = true;
            }
        }
        else
        {
            foreach (var mObj in coveredObjs)
            {
                mObj.covered = false;
            }
        }
    }


    /// <summary>
    /// When held, accepts mouse input. Override when needed.
    /// </summary>
    /// <param name="button">Button Pressed</param>
    public virtual void HeldUse(MouseButton button, bool down)
    {
        if(!down)
            SetDownObject();
    }


}
