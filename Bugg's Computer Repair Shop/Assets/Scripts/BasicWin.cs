using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicWin : MonoBehaviour
{
    #region Fields
    #region public
    public WorldObject[] parts;
    public int iceCount = 1;
    public bool won = false;
    public bool charged;
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

    private void LoadGame()
    {
        SceneManager.LoadScene(3);
    }
    #endregion
    #endregion
}
