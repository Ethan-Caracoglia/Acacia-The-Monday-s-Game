using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Melt : MonoBehaviour
{
    #region Fields
    #region public
    // This should NOT be how we do this
    public SpriteRenderer spr;
    #endregion

    #region private
    [SerializeField] float meltRate = 0.4f;
    [SerializeField] float minSize = 0.3f;
    [SerializeField] Sprite lighterOff;
    [SerializeField] Sprite lighterOn;
    [SerializeField] BasicWin b;
    private bool isMouseDown;
    #endregion
    #endregion

    #region Methods
    #region public
    public void GetMouseDown(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            isMouseDown = true;
            spr.sprite = lighterOn;

        }
        if (ctx.canceled)
        {
            isMouseDown = false;
            spr.sprite = lighterOff;
        }
    }
    #endregion

    #region private
    private void Update()
    {
        if (transform.localScale.x <= minSize || transform.localScale.y <= minSize)
        {
            b.updateIceCount();
            Destroy(gameObject);
        }
    }

    //prebuilt method, Checks for hovering on the collider.
    private void OnMouseOver()
    {
        //check if the left click is down
        if (isMouseDown)
        {
            Debug.Log("melt");
            //check how big the gameobject is, if its not too tiny to reasonably click on, then start scaling it down
            float meltAmount = meltRate * Time.deltaTime;
            transform.localScale -= new Vector3(meltAmount, meltAmount, 0);
        }
    }
    #endregion
    #endregion
}
