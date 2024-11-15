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
        Debug.Log("Lighter GetInput()");

        if (!player.MBReleased[1])
        {
            Debug.Log("Did it make it to HeldUse()?");
            HeldUse(player);
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        }
    }

    protected override void HeldUse(Player player)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = highlightSprite;
        timeSinceLastAction += Time.deltaTime;
        if (timeSinceLastAction >= interval)
        {
            timeSinceLastAction = 0f;
            Instantiate(flameParticle).transform.position = new Vector3(0.03f, 0.2f, 0f) + new Vector3(transform.position.x + Random.Range(-0.02f, 0.02f), transform.position.y + Random.Range(-0.02f, 0.02f), 0);
        }
    }
}
