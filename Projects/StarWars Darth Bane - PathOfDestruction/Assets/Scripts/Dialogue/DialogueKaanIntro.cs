using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueKaanIntro : MonoBehaviour {

	public string[] DialogueText = new string[1];
    public float[] textDuration = new float[1];
    public GameObject[] CameraObjects = new GameObject[1];
    public int DialogueNumber;
    public int NPCNumber = 1;
	public int DialogueLastLineNumber = 4;
	public bool DialogueFinishedText = false;
	public string[] NameString = new string[1];
	public Text DialogueBox;
	public Text Name;
	public bool NextText = false;
    public bool NextAutoText = false;
    public int EndOfDialogueTime = 1;
    public CanvasGroup canvasGroup;
    public bool ConverSationTrigger = false;
    public bool hasEnabled = false;
    public GameObject ConvoTrigger;
    public WayPoints moveScript;
    public int NextWorldNumber = 3;

    void Start()
    {
        DialogueBox.enabled = false;
        Name.enabled = false;
        CameraObjects[0].SetActive(false);
        CameraObjects[1].SetActive(false);
        CameraObjects[2].SetActive(false);
        canvasGroup.alpha = 0;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Kaan")
        {
            Debug.Log("TEST");
            ConverSationTrigger = true;
            moveScript.enabled = false;
        }
    }

    void Update()
	{
        if (ConverSationTrigger == true)
        {
            DialogueStart();
            if (DialogueNumber == DialogueLastLineNumber)
            {
                NextText = false;
                DialogueFinishedText = true;
                StopCoroutine("NextLine");
                StartCoroutine("ExitDialogue");
            }

            if (Input.GetKeyDown(KeyCode.Space) && DialogueFinishedText == false)
            {
                Next();
                NextText = false;
                NextAutoText = false;
                StopCoroutine("NextLine");
            }
            if (NextAutoText == false && DialogueFinishedText == false)
            {
                NextAutoText = true;
                StartCoroutine("NextLine");
            }
        }
    }

	public void DialogueStart() 
	{
        if (hasEnabled == false)
        {
            hasEnabled = true;
            DialogueBox.enabled = true;
            Name.enabled = true;
            CameraObjects[0].SetActive(true);
            CameraObjects[1].SetActive(true);
            CameraObjects[2].SetActive(true);
        }

        if (DialogueNumber <= DialogueLastLineNumber) 
		{
			DialogueBox.text =  DialogueText[DialogueNumber];
			Name.text = "~" + NameString[DialogueNumber] + "~";
			NextText = false;
		}
	}

	public void Next()
	{
		if(DialogueFinishedText == false)
		{
			DialogueNumber++;
			NextText = true;
		}
	}

    public IEnumerator NextLine()
    {
        yield return new WaitForSeconds(textDuration[DialogueNumber]);
        NextAutoText = false;
        Next();
    }

    public IEnumerator ExitDialogue()
	{
		yield return new WaitForSeconds(EndOfDialogueTime);
        Name.enabled = false;
        DialogueBox.enabled = false;
        StartCoroutine("FadeIn");
        yield return new WaitForSeconds(3);
        Name.enabled = false;
        DialogueBox.enabled = false;
        SceneManager.LoadScene(NextWorldNumber);
    }

    public IEnumerator FadeIn()
    {
        while (canvasGroup.alpha >= 0)
        {
            canvasGroup.alpha += Time.deltaTime / 5;
            yield return null;
        }
    }
}
