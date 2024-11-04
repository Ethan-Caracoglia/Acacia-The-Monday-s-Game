using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Split into "snapping and non-snapping objects: would be nice to move objects on top of others. Not neccessary though.
/// </summary>
public class MoveableObj : ObjInterface
{
    #region Fields
    #region Public Fields
    public Sprite sprite; // Base sprite 
    public Sprite highlightSprite;
    public PlayerState player;
    public bool snapped = true;
    public ObjInterface[] coveredObjs;
    #endregion

    #region Protected Fields
    [SerializeField] protected float snapDistance = 0.01f;
    protected bool dragging = false;
    protected Vector3 offset;
    protected Vector3 snapPosition;
    protected Collider2D objCollider;
    #endregion
    #endregion

    #region Internal Methods
    /// <summary>
    /// Setup the 
    /// </summary>
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
    // Ethan: Don't change this for now since it just works 
    void OnMouseOver()
    {
        transform.GetComponent<SpriteRenderer>().sprite = highlightSprite;
    }

    void OnMouseExit()
    {
        transform.GetComponent<SpriteRenderer>().sprite = sprite;
    }
    #endregion

    #region External Methods
    public override void ParentPositionChange(Vector3 newPos)
    {
        snapPosition = newPos;
        if (snapped)
        {
            Move(snapPosition);
        }
    }

    public void UpdateMousePosition(Vector3 MousePos)
    {
        Move(MousePos + offset);
    }

    public override void GetInput(PlayerState player)
    {
    
    }

    /// <summary>
    /// Picks up an object when the mouse is at v
    /// </summary>
    /// <param name="v">PickUpObject</param>
    /// <param name="m">PickUpObject</param>
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


    /// <summary>
    /// When held, accepts mouse input. Override when needed.
    /// </summary>
    /// <param name="button">Button Pressed</param>
    protected virtual void HeldUse(PlayerState player)
    {
        if(dragging)
            SetDownObject();
    }

    #region Getters
    #endregion

    #region Setters
    #endregion
    #endregion
}
