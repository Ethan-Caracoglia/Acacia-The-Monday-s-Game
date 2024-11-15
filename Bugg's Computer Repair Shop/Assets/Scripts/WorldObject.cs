using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObject : MonoBehaviour
{
    #region Fields
    public bool covered = false;
    public WorldObject[] children;

    public Sprite sprite; // Base sprite 
    public Sprite highlightSprite;
    public bool snapped = true;
    public WorldObject[] coveredObjs;

    [SerializeField] protected float snapDistance = 0.01f;
    protected bool dragging = false;
    protected Vector3 offset;
    protected Vector3 snapPosition;
    [SerializeField] protected Collider2D objCollider;

    protected Dictionary<string, Vector3> offsets = new Dictionary<string, Vector3>();
    #endregion

    #region Properties
    public bool IsTool { get; private set; }
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
    public virtual void GetInput(Player player)
    {

    }

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
        offset = transform.position - player.MousePos;
    }

    /// <summary>
    /// Sets down an object
    /// </summary>
    public void SetDown()
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
    public void Move(Vector3 newPos)
    {
        // Potentially make this move to top Z value and drop down?
        transform.position = offset + new Vector3(newPos.x, newPos.y, transform.position.z);
    }

    /// <summary>
    /// When held, accepts mouse input. Override when needed.
    /// </summary>
    /// <param name="button">Button Pressed</param>
    protected virtual void HeldUse(Player player)
    {

    }
    #endregion
    #endregion
}
