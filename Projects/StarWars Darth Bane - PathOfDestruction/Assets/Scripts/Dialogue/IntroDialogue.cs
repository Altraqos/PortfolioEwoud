using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroDialogue : MonoBehaviour
{

    public string[] DialogueText = new string[1];
    public int[] textDuration = new int[1];
    public int DialogueNumber;
    public int DialogueLastLineNumber = 4;
    public bool DialogueFinishedText = false;
    public Text DialogueBox;
    public bool NextText = false;
    public bool NextAutoText = false;
    public bool isFading = false;
    public bool isFadedText = false;
    public CanvasGroup canvasGroup;
    public CanvasGroup ButtonGroup;
    public Canvas TextCanvas;
    public bool LoadNextLevel = false;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        ButtonGroup.alpha = 0;
        ButtonGroup.interactable = false;
        StartCoroutine("FadeIn");
        DialogueBox.text = DialogueText[DialogueNumber];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && DialogueFinishedText == false)
        {
            NextText = false;
            NextAutoText = false;
            StartCoroutine("FadeText");
        }
        if (NextAutoText == false && DialogueFinishedText == false)
        {
            NextAutoText = true;
            StartCoroutine("NextLine");
        }

        if (LoadNextLevel == true)
        {
            SceneManager.LoadScene(2);
        }
	}

    public IEnumerator NextLine()
    {
        yield return new WaitForSeconds(textDuration[DialogueNumber]);
        NextAutoText = false;
        StartCoroutine("FadeText");
    }

    public IEnumerator FadeOut()
    {
        StopCoroutine("FadeText");
        StopCoroutine("NextLine");
        yield return new WaitForSeconds(4);
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime / 0.5f;
            yield return null;
        }
        yield return new WaitForSeconds(3);
        TextCanvas.enabled = false;
        LoadNextLevel = true;
    }
    public IEnumerator NextLevelLoad()
    {
        yield return new WaitForSeconds(6);
        TextCanvas.enabled = false;
        LoadNextLevel = true;
    }

    public IEnumerator FadeIn()
    {
        while (canvasGroup.alpha >= 0)
        {
            canvasGroup.alpha += Time.deltaTime / 2;
            yield return null;
        }
    }

    public IEnumerator FadeText()
    {
        while (canvasGroup.alpha > 0 && isFadedText == false)
        {
            canvasGroup.alpha -= Time.deltaTime / 2;
            yield return null;
            if (canvasGroup.alpha == 0)
            {
                DialogueNumber++;
                isFadedText = true;
            }
        }
        if (DialogueNumber <= DialogueLastLineNumber)
        {
            DialogueBox.text = DialogueText[DialogueNumber];
            NextText = false;
        }
        while (canvasGroup.alpha >= 0 && isFadedText == true)
        {
            canvasGroup.alpha += Time.deltaTime / 2;
            yield return null;
            if (canvasGroup.alpha == 1)
            {
                isFadedText = false;
            }
        }
        if (DialogueNumber == DialogueLastLineNumber)
        {
            isFadedText = true;
            NextText = false;
            DialogueFinishedText = true;
            if (DialogueFinishedText == true && isFading == false)
            {
                StopCoroutine("FadeText");
                StopCoroutine("NextLine");
                isFading = true;
                StartCoroutine("FadeOut");
                StartCoroutine("NextLevelLoad");
            }
        }
    }
}
