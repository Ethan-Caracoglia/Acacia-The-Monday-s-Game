using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Dialogue : MonoBehaviour
{

    [SerializeField] RectTransform fader;

    public TextMeshProUGUI textComponent;
    public string[] lines;  // Make sure this is populated in the Inspector!
    public float textSpeed;

    private int index = 0;
    private bool isTyping = false;  // Track if the current line is typing
    private bool dialogueEnded = false;  // Ensure no interaction after dialogue ends

    void Start()
    {
        // Start with an empty text box
        textComponent.text = string.Empty;

        if (lines.Length > 0)
        {
            StartDialogue();
        } 
        else
        {
            Debug.LogWarning("No dialogue lines assigned.");
        }
    }

    public void GetMouseDown(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && !dialogueEnded)
        {
            OnDialogueClick();
        }
    }

    void OnDialogueClick()
    {
        if (isTyping)
        {
            // Stop the coroutine and finish the current line instantly
            StopAllCoroutines();
            textComponent.text = lines[index];  // Display the complete line
            isTyping = false;
        }
        else
        {
            // Move to the next line or end the dialogue
            NextLine();
        }
    }

    void StartDialogue()
    {
        index = 0;
        dialogueEnded = false;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        textComponent.text = string.Empty;

        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false;  // Finished typing the current line
    }

    void NextLine()
    {
        index++;

        if (index < lines.Length)
        {
            StartCoroutine(TypeLine());
        }
        else
        {
            // If no more lines, end the dialogue
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        dialogueEnded = true;
        Debug.Log("Dialogue ended.");
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, Vector3.zero, 0);
        LeanTween.scale(fader, new Vector3(1, 1, 1), 0.5f).setEase(LeanTweenType.easeInOutExpo).setOnComplete(() =>
        {
            Invoke("LoadGame", 0.5f);
        });
    }

    private void LoadGame()
    {
        SceneManager.LoadScene(2);
    }
}
