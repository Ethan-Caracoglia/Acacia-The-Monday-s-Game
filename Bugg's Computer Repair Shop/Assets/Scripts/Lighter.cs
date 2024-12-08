using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Lighter : WorldObject
{
    public GameObject flameParticle;
    // Make Array, melt has collider ref
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
        if (iceCollider != null)
        {
            if (flame.bounds.Intersects(iceCollider.bounds))
            {
                Debug.Log("Melting");
                ice.Melting();
            }
        }

        timeSinceLastAction += Time.deltaTime;
        if (timeSinceLastAction >= interval)
        {
            timeSinceLastAction = 0f;
            Instantiate(flameParticle).transform.position = new Vector3(0.05f, 0.24f, transform.position.z) + new Vector3(transform.position.x + Random.Range(-0.02f, 0.02f), transform.position.y + Random.Range(-0.02f, 0.02f), 0);
        }
    }
}
