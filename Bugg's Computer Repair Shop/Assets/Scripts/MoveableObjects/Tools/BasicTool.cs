using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasicTool : abTool
{


    [SerializeField] Sprite active;

    public override void StartBaseUse()
    {
        objSprite.sprite = active;
    }

    public override void EndBaseUse()
    {
        objSprite.sprite = sprite;
    }



}
