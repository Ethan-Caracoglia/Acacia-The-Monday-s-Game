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
    [SerializeField] BasicWin b;
    private bool isMouseDown;
    #endregion
    #endregion

    #region Methods
    #region public
    public void Melting()
    {
        Debug.Log("melt");
        //check how big the gameobject is, if its not too tiny to reasonably click on, then start scaling it down
        float meltAmount = meltRate * Time.deltaTime;
        transform.localScale -= new Vector3(meltAmount, meltAmount, 0);
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
    #endregion
    #endregion
}
