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
    #endregion

    #region private
    [SerializeField] SpriteRenderer victory;
    [SerializeField] RectTransform fader;
    [SerializeField] float delay = 3;
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
        if (won)
        {
            delay -= 1 * Time.deltaTime;
            if (delay < 0)
            {
                if (!fader.gameObject.active)
                {
                    fader.gameObject.SetActive(true);
                    LeanTween.scale(fader, Vector3.zero, 0);
                    LeanTween.scale(fader, new Vector3(1, 1, 1), 0.5f).setEase(LeanTweenType.easeInOutExpo).setOnComplete(() =>
                    {
                        Invoke("LoadGame", 0.5f);
                    });
                }
            }
            return;
        }
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
