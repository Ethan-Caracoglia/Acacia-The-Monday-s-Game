using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Lighter : WorldObject
{
    public GameObject flameParticle;
    public Melt ice;
    public Collider2D iceCollider;
    public Collider2D flame;
    public float interval = 0.5f;
    private float timeSinceLastAction = 0f;
    private bool isMelting;
    private bool beingUsed = false;


    private void Update()
    {
        if (beingUsed)
        {
            HeldUse();
        }
        if (isMelting)
        {
            ice.Melting();
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
        if (beingUsed && flame.bounds.Intersects(iceCollider.bounds))
        {
            ice.Melting();
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        }
    }

    protected void HeldUse()
    {
        timeSinceLastAction += Time.deltaTime;
        if (timeSinceLastAction >= interval)
        {
            timeSinceLastAction = 0f;
            Instantiate(flameParticle).transform.position = new Vector3(0.05f, 0.24f, transform.position.z) + new Vector3(transform.position.x + Random.Range(-0.02f, 0.02f), transform.position.y + Random.Range(-0.02f, 0.02f), 0);
        }
    }
}
