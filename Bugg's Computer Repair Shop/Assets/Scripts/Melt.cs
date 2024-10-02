using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Melt : MonoBehaviour
{
    private bool isMouseDown;

    // This should NOT be how we do this
    public SpriteRenderer spr;

    [SerializeField] float meltRate = 0.4f;

    [SerializeField] float minSize = 0.3f;

    [SerializeField] BasicWin b;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x <= minSize || transform.localScale.y <= minSize)
        {
            b.updateIceCount();
            Destroy(gameObject);
        }
    }

    public void GetMouseDown(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            isMouseDown = true;
            spr.color = Color.red;

}
        if (ctx.canceled)
        {
            isMouseDown = false;
            spr.color = Color.white;
        }
    }



    //prebuilt method, Checks for hovering on the collider.
    private void OnMouseOver()
    {
        Debug.Log("MouseOver");
        //check if the left click is down
        if (isMouseDown)
        {
            Debug.Log("melt");
            //check how big the gameobject is, if its not too tiny to reasonably click on, then start scaling it down
                float meltAmount = meltRate * Time.deltaTime;
            transform.localScale -= new Vector3(meltAmount, meltAmount, 0);
        }

    }
}
