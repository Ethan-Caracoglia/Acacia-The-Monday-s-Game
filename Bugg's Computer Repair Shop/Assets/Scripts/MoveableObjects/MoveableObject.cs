using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Split into "snapping and non-snapping objects: would be nice to move objects on top of others. Not neccessary though.
public class MoveableObject : ObjInterface
{
    public Sprite sprite;
    public Sprite highlightSprite;

    protected bool dragging = false;

    public bool snapped = true;

    protected Vector3 offset;
    protected Vector3 snapPosition;

    public ObjInterface[] coveredObjs;
    [SerializeField] protected float snapDistance = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
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
            UpdatePosition(Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset);
        }
    }

    // Can pick up multiple objects at the same time: Lock an object to the MOUSE

    private void OnMouseDown()
    {
        if (covered && snapped)
        {
            return;
        }
            dragging = true;
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            snapped = false;
    }

    private void OnMouseUp()
    {

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
}
