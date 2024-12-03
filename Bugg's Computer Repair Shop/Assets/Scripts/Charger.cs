using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Charger : WorldObject
{
    // Make Array, has collider ref
    public Collider2D charger;
    public Collider2D chargedPart;
    public float interval = 0.5f;
    private bool beingUsed = false;


    private void Update()
    {
        if (beingUsed)
        {
            HeldUse();
        }
    }

    public override void SetDown()
    {
        base.SetDown();

        beingUsed = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public override void GetInput(Player player)
    {
        Debug.Log("Lighter GetInput()");
        beingUsed = !player.MBReleased[1];
    }

    protected void HeldUse()
    {
        //Check if on Computer
        //foreach
        if (chargedPart != null)
        {
            if (charger.bounds.Intersects(chargedPart.bounds))
            {
                Debug.Log("Charging");
            }
        }
    }
}
