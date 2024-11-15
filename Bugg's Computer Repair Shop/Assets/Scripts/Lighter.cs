using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighter : WorldObject
{
    public GameObject flameParticle;
    public float interval = 0.5f;
    private float timeSinceLastAction = 0f;

    public override void GetInput(Player player) 
    {
        Console.WriteLine("GAGGABOO");
        if (player.MBHeld[1])
        {
            HeldUse(player);
        }
    }

    protected override void HeldUse(Player player)
    {
        Console.WriteLine("GAGGABOO");
        timeSinceLastAction += Time.deltaTime;
        if (timeSinceLastAction >= interval)
        {
            timeSinceLastAction = 0f;
            Instantiate(flameParticle);
        }
    }
}
