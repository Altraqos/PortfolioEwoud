using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCameraScripts : MonoBehaviour {

    DialogueKaanIntro dialogueScript;
    public Camera[] DialogueCamera = new Camera[1];

    void Start ()
    {
        SetInitialReferences();
        //DialogueCamera[dialogueScript.DialogueNumber].enabled = false;
        DialogueCamera[0].enabled = true;
    }

    void SetInitialReferences()
    {
        dialogueScript = GetComponent<DialogueKaanIntro>();
    }


	void Update ()
    {
        if (dialogueScript.DialogueNumber >= 0)
        {
            NextCamera();
        }
	}

    public void NextCamera()
    {
        if (dialogueScript.DialogueNumber < dialogueScript.DialogueLastLineNumber)
        {
            DialogueCamera[dialogueScript.DialogueNumber].enabled = false;
            DialogueCamera[dialogueScript.DialogueNumber].enabled = true;
        }
        if (dialogueScript.DialogueNumber == dialogueScript.DialogueLastLineNumber)
        {
            DialogueCamera[dialogueScript.DialogueNumber].enabled = false;
            DialogueCamera[dialogueScript.DialogueNumber].enabled = true;
        }
    }
}
