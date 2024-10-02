using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    private int index;
    private bool isMouseDown = false;

    // Start is called before the first frame update
    void Start()
    {
        // Begins with an empty text box before startin dialouge
        textComponent.text = string.Empty;
        StartDialogue();
    }

    public void GetMouseDown(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            isMouseDown = true;
        }
        if (ctx.canceled)
        {
            isMouseDown = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isMouseDown) // Need to conver
        {
            if(index >= lines.Length)
            {
                SceneManager.LoadScene(1);
            }
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            SceneManager.LoadScene(1);
            gameObject.SetActive(false);
        }
    }
}