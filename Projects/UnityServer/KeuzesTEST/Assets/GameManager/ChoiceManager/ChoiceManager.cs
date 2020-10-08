using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceManager : MonoBehaviour
{
    public Button Choice1Button;
    public Button Choice2Button;
    public Button Choice3Button;
    public Button Choice4Button;

    public Text Choice1Text;
    public Text Choice2Text;
    public Text Choice3Text;
    public Text Choice4Text;

    public Text QuestionText;

    public string[] QuestionArray;

    public string answerVal;

    void Update()
    {
        QuestionText.text = "Voer 1 in";

        if (answerVal == null)
        {
            Choice1Text.text = QuestionArray[0];
            Choice2Text.text = QuestionArray[1];
            Choice3Text.text = QuestionArray[2];
            Choice4Text.text = QuestionArray[3];
        }

        switch (answerVal)
        {
            case "A1":

                QuestionText.text = "Je hebt het goed";

                    break;

            case "A2":

                QuestionText.text = "Je hebt het fout";

                break;

            case "A3":

                QuestionText.text = "Je hebt het fout";

                break;

            case "A4":

                QuestionText.text = "Je hebt het fout";

                break;
        }
    }

    public void Choice1Func()
    {
        answerVal = "A1";
    }

    public void Choice2Func()
    {
        answerVal = "A2";
    }

    public void Choice3Func()
    {
        answerVal = "A3";
    }

    public void Choice4Func()
    {
        answerVal = "A4";
    }
}
