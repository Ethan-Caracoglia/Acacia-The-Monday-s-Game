using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmovableObject : ObjInterface
{

    public override void ParentPositionChange(Vector3 newPos)
    {
        UpdatePosition(newPos);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }



}
