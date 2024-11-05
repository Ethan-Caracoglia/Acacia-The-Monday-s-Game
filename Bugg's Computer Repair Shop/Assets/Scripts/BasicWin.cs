using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWin : MonoBehaviour
{
    #region Fields
    #region public
    public MoveableObj[] parts;
    public int iceCount = 1;
    public bool won = false;
    #endregion

    #region private
    [SerializeField] SpriteRenderer victory;
    #endregion
    #endregion

    #region Methods
    #region public
    public void updateIceCount()
    {
        iceCount -= 1;
    }
    #endregion

    #region private
    // Update is called once per frame
    private void Update()
    {
        if (won) return;
        if (iceCount <= 0)
        {
            foreach (var part in parts)
            {
                if (!part.snapped)
                {
                    return;
                }
            }
            won = true;

        }
        if (won)
        {
            victory.enabled = true;
        }
    }
    #endregion
    #endregion
}
