using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Melt : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        //if the gameObject is too msall to reasonably make the player click on, "melt" it the rest of the way and then delete it
        if (transform.localScale.x <= .7 || transform.localScale.y <= .7)
        {
            transform.localScale -= new Vector3(.05f, .05f, .05f);
            if (transform.localScale.x <= .3 || transform.localScale.y <= .3)
            {
                Destroy(gameObject);
            }
        }
    }

    //prebuilt method, Checks for hovering on the collider.
    private void OnMouseOver()
    {
        //check if the left click is down
        if (Input.GetMouseButton(0))
        {
            //check how big the gameobject is, if its not too tiny to reasonably click on, then start scaling it down
            if (transform.localScale.x >= .7 && transform.localScale.y >= .7)
            {
                transform.localScale -= new Vector3(.05f, .05f, .05f);

            }

       
        }

    }
}
