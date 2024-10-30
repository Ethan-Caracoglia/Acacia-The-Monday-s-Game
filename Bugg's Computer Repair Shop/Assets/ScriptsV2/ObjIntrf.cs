using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjIntrf : MonoBehaviour, IInteractable
{
    public const string EMPTY_OBJ_ID = "EMPTY";
    public string id;
    public ObjInterface[] childrenObjs;
    protected Dictionary<string, Vector3> offsets = new Dictionary<string, Vector3>();

    /// <summary>
    /// CALL THIS INSTEAD OF SETTING THE POSITION.
    /// </summary>
    /// <param name="newPos"></param>
    public void UpdatePosition(Vector3 newPos)
    {
        // Potentially make this move to top Z value and drop down?
        Vector3 pos = transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);

        foreach (var mObj in childrenObjs)
        {
            mObj.ParentPositionChange(pos + offsets[mObj.id]);
        }
    }

    public abstract void ParentPositionChange(Vector3 newPos);

    public abstract void TryMouseInput(InteractionState state);

    public abstract void UpdateMousePosition(Vector3 MousePos);
}
