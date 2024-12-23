using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObject : MonoBehaviour
{
    #region Fields
    public bool covered = false;
    public Sprite sprite; // Base sprite 
    public Sprite highlightSprite;
    public bool snapped = true;
    
    protected bool dragging = false;
    protected Vector3 offset = Vector3.zero;
    protected Vector3 snapPosition;
    [SerializeField] protected WorldObject[] snapChildren;
    [SerializeField] protected float snapDistance = 0.03f;
    [SerializeField] protected Collider2D objCollider;
    #endregion

    #region Properties
    public bool IsMoveable { get; private set; }
    #endregion

    #region Methods
    #region private
    private void Start()
    {
        //objCollider = GetComponent<Collider2D>();

        snapPosition = transform.position;
    }

    private void Update()
    {
        if (dragging)
        {
            // Add wiggle, if wanted

        }
    }

    private void OnMouseOver()
    {
        transform.GetComponent<SpriteRenderer>().sprite = highlightSprite;
    }

    private void OnMouseExit()
    {
        transform.GetComponent<SpriteRenderer>().sprite = sprite;
    }
    #endregion

    #region public
    /// <summary>
    /// Grabs Playe
    /// </summary>
    /// <param name="player"></param>
    public virtual void GetInput(Player player) { Debug.Log("WorldObject GetInput()"); }

    /// <summary>
    /// Picks up an object
    /// </summary>
    /// <param name="player">The state of the player</param>
    public void PickedUp(Player player)
    {
        // Disable collider when picked up so the click propegates properly
        objCollider.enabled = false;
        dragging = true;
        snapped = false;
        offset = new Vector3(transform.position.x, transform.position.y, 0) - new Vector3(player.MousePos.x, player.MousePos.y, 0);
    }

    /// <summary>
    /// Sets down an object
    /// </summary>
    public virtual void SetDown()
    {
        Debug.Log("Drop Obj");
        if (dragging == false)
            return;

        objCollider.enabled = true;
        dragging = false;

        if ((transform.position - snapPosition).sqrMagnitude <= snapDistance)
        {
            Move(snapPosition - offset);
            snapped = true;
        }

        foreach (WorldObject w in snapChildren)
        {
            w.UpdateSnap(transform.position);
        }
        offset = Vector3.zero;
    }
    #endregion

    #region protected
    public virtual void Move(Vector3 newPos)
    {
        // Potentially make this move to top Z value and drop down?
        transform.position = offset + new Vector3(newPos.x, newPos.y, transform.position.z);
        foreach (WorldObject w in snapChildren)
        {
            w.UpdateSnap(transform.position);
        }
    }

    public virtual void UpdateSnap(Vector3 newPos)
    {
        newPos =  new Vector3(newPos.x, newPos.y, transform.position.z);
        snapPosition = newPos;
        if(snapped)
            Move(newPos);
    }

    //public virtual void Update
    #endregion
    #endregion
}
