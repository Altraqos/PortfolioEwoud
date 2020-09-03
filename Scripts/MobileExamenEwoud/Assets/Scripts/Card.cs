using UnityEngine;

public class Card : MonoBehaviour
{
    Transform endPos;
    [HideInInspector] public int cardVal;
    [HideInInspector] public bool isClicked = false;
    [HideInInspector] public bool toEnd = false;
    [HideInInspector] public bool isFlipping = false;

    public void OnEnable()
    {
        endPos = GameObject.Find("EndPos").transform;
    }

    public void AssignValues(Color cardColor)
    {
        transform.gameObject.transform.Find("cardFace").GetComponent<Renderer>().material.SetColor("_Color", cardColor) ;
        cardVal = GameManager.instance.cardColors.IndexOf(cardColor);
    }

    public void FlipCard(bool flip)
    {
        if (flip)
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, 180, 0), 10 * Time.deltaTime);
        if(!flip)
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, 0, 0), 10 * Time.deltaTime);
    }

    void Update()
    {
        FlipCard(isFlipping);
        if (toEnd)
        {
            transform.position = Vector2.Lerp(transform.position, endPos.position, Time.deltaTime * 1);
            GameManager.instance.gameObject.GetComponent<CardManager>().RemoveMatch(cardVal);
        }
    }

    void OnMouseDown()
    {
        if (!isClicked && GameManager.instance.secondChoice == null && !GameManager.instance.inGameMenuOpen && !isFlipping)
        {
            isFlipping = true;
            isClicked = true;
            GameManager.instance.isChoosing = true;
            if (GameManager.instance.firstChoice == null)
                GameManager.instance.firstChoice = this;
            else
            {
                GameManager.instance.secondChoice = this;
                GameManager.instance.StartCoroutine("CompareCards");
            }
        }
    }
}