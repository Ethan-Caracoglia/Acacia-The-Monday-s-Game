using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    public string id;
    private bool dragging = false;

    public bool snapped = true;

    private Vector3 offset;
    private Vector3 snapPosition;
    private float snapDistance = 0.05f;

    public MoveableObject[] moveChildren;
    private Dictionary<string, Vector3> offsets = new Dictionary<string, Vector3>();
    // Start is called before the first frame update
    void Start()
    {
        snapPosition = transform.position;
        foreach (var mObj in moveChildren)
        {
            offsets.Add(mObj.id, mObj.transform.position - transform.position);
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

    private void OnMouseDown()
    {
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
        }
    }

    /// <summary>
    /// CALL THIS INSTEAD OF SETTING THE POSITION.
    /// </summary>
    /// <param name="newPos"></param>
    public void UpdatePosition(Vector3 newPos)
    {
        transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);

        foreach (var mObj in moveChildren)
        {
            mObj.snapPosition = transform.position + offsets[mObj.id];
            if (mObj.snapped)
            {
                mObj.UpdatePosition(mObj.snapPosition);
            }
        }
    }
}
