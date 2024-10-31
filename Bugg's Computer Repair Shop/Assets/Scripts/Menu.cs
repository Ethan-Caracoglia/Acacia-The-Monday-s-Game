using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] RectTransform fader;

    private void Start()
    {
        fader.gameObject.SetActive(true);

        LeanTween.scale(fader, new Vector3(1, 1, 1), 0);
        LeanTween.scale(fader, Vector3.zero, 0.5f).setEase(LeanTweenType.easeInOutExpo).setOnComplete(() =>
        {
            fader.gameObject.SetActive(false);
        });
    }
    public void OnPlayButton()
    {
        fader.gameObject.SetActive(true) ;
        LeanTween.scale(fader, Vector3.zero, 0);
        LeanTween.scale(fader, new Vector3(1, 1, 1), 0.5f).setEase(LeanTweenType.easeInOutExpo).setOnComplete(() =>
        {
            Invoke("LoadGame", 0.5f);
        });
        
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }

    private void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
}
