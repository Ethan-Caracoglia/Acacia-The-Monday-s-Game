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
    [SerializeField] float meltRate = 0.1f;
    [SerializeField] float minSize = 0.1f;


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
        for (int i = 0; i < ice.iceCubes.Count; i++)
        {
            if (flame.bounds.Intersects(ice.iceCubes[i].GetComponent<Collider2D>().bounds)) 
            {
                float meltAmount = meltRate * Time.deltaTime;
                ice.iceCubes[i].transform.localScale -= new Vector3(meltAmount, meltAmount, 0);
            }

            if (ice.iceCubes[i].transform.localScale.x <= minSize || ice.iceCubes[i].transform.localScale.y <= minSize)
            {
                Debug.Log("melted");
                //check how big the gameobject is, if its not too tiny to reasonably click on, then start scaling it down
                Destroy(ice.iceCubes[i]);
                ice.iceCubes.RemoveAt(i);
                i--;
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
