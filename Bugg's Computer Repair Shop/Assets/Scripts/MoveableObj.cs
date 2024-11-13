using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Split into "snapping and non-snapping objects: would be nice to move objects on top of others. Not neccessary though.
/// </summary>
public class MoveableObj : ObjInterface
{
    #region Fields
    #region public
    public Sprite sprite; // Base sprite 
    public Sprite highlightSprite;
    public PlayerState player;
    public bool snapped = true;
    public ObjInterface[] coveredObjs;
    #endregion

    #region protected
    [SerializeField] protected float snapDistance = 0.01f;
    protected bool dragging = false;
    protected Vector3 offset;
    protected Vector3 snapPosition;
    protected Collider2D objCollider;
    #endregion
    #endregion

    #region Methods
    #region public
    public override void ParentPositionChange(Vector3 newPos)
    {
        snapPosition = newPos;
        if (snapped)
        {
            Move(snapPosition);
        }
    }

    public void UpdateMousePosition(Vector3 newPos)
    {
        Move(newPos + offset);
    }

    public override void GetInput(PlayerState player)
    {
        return;
    }

    /// <summary>
    /// Picks up an object
    /// </summary>
    /// <param name="player">The state of the player</param>
    public void PickUpObj(PlayerState player)
    {
        // Disable collider when picked up so the click propegates properly
        objCollider.enabled = false;

        dragging = true;
        offset = transform.position - new Vector3(player.GetMousePos().x, player.GetMousePos().y, 0);
        snapped = false;
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
        dragging = false;

        if ((transform.position - snapPosition).sqrMagnitude <= snapDistance)
        {
            Move(snapPosition);
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
    #endregion

    #region protected
    /// <summary>
    /// When held, accepts mouse input. Override when needed.
    /// </summary>
    /// <param name="button">Button Pressed</param>
    protected virtual void HeldUse(PlayerState player)
    {
        if (dragging)
            SetDownObject();
    }
    #endregion

    #region private
    private void Start()
    {
        objCollider = GetComponent<Collider2D>();

        snapPosition = transform.position;
        foreach (var mObj in childObjs)
        {
            offsets.Add(mObj.id, mObj.transform.position - transform.position);
        }
        foreach (var mObj in coveredObjs)
        {
            mObj.covered = true;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (dragging)
        {
            // Add wiggle, if wanted

        }
    }

    // Might need to be changed
    // Ethan: Don't change this for now since it just works 
    private void OnMouseOver()
    {
        transform.GetComponent<SpriteRenderer>().sprite = highlightSprite;
    }

    private void OnMouseExit()
    {
        transform.GetComponent<SpriteRenderer>().sprite = sprite;
    }
    #endregion
    #endregion
}
