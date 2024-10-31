using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjInterface : MonoBehaviour, IInteractable
{
    public const string EMPTY_OBJ_ID = "EMPTY";
    public string id;
    public bool covered = false;
    public ObjInterface[] childrenObjs;
    protected Dictionary<string, Vector3> offsets = new Dictionary<string, Vector3>();

    protected override void Move(Vector3 newPos)
    {
        // Potentially make this move to top Z value and drop down?
        Vector3 pos = transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);

        foreach (var mObj in childrenObjs)
        {
            mObj.ParentPositionChange(pos + offsets[mObj.id]);
        }
    }


    public abstract GetInput() 


    public abstract void ParentPositionChange(Vector3 newPos);


}
