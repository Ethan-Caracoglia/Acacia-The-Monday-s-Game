using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjInterface : MonoBehaviour
{
    public const string EMPTY_OBJ_ID = "EMPTY";
    public string id;
    public bool covered = false;
    public ObjInterface[] childrenObjs;
    protected Dictionary<string, Vector3> offsets = new Dictionary<string, Vector3>();

    /// <summary>
    /// CALL THIS INSTEAD OF SETTING THE POSITION.
    /// </summary>
    /// <param name="newPos"></param>
    public void UpdatePosition(Vector3 newPos)
    {
        Vector3 pos = transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);

        foreach (var mObj in childrenObjs)
        {
            mObj.ParentPositionChange(pos + offsets[mObj.id]);
        }
    }

    public abstract void ParentPositionChange(Vector3 newPos);
}
