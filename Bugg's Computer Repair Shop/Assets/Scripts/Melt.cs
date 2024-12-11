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
    public List<GameObject> iceCubes;
    #endregion

    #region private
    [SerializeField] BasicWin b;
    private bool isMouseDown;
    #endregion
    #endregion

    #region Methods

    #region private
    private void Update()
    {
        if (iceCubes.Count == 0)
        {
            b.updateIceCount();
        }
    }
    #endregion
    #endregion
}
