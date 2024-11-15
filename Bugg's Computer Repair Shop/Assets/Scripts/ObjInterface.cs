using System.Collections.Generic;
using UnityEngine;

public abstract class ObjInterface : MonoBehaviour
{
    #region Fields
    public const string EMPTY_OBJ_ID = "EMPTY";
    public string id;
    public bool covered = false;
    public ObjInterface[] childObjs;
    
    protected Dictionary<string, Vector3> offsets = new Dictionary<string, Vector3>();
    #endregion

    #region Methods
    #region public
    public abstract void GetInput(PlayerState player);

    public abstract void ParentPositionChange(Vector3 newPos);
    #endregion

    #region protected
    protected void Move(Vector3 newPos)
    {
        // Potentially make this move to top Z value and drop down?
        Vector3 pos = transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);

        foreach (var mObj in childObjs)
        {
            mObj.ParentPositionChange(pos + offsets[mObj.id]);
        }
    }
    #endregion
    #endregion
}