using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevel : MonoBehaviour
{
    PlayerMaster pMaster;
    PlayerHealth pHealth;
    public Slider ExpSlider;
    public Text ExpText;
    public Text LevelText;
    public Text LevelUpText;
    Image[] NumberPosArray;
    public Image NumberPosHolder;
    public GameObject numberPos;
    public CanvasGroup LevelUpCanvas;
    public float LevelUpCanvasVisible = 45;
    bool levelUpCanvasSpamFilter = false;

    private void OnEnable()
    {
        SetInitialReferences();
        SetUI();
    }

    public void SetInitialReferences()
    {
        pMaster = GetComponent<PlayerMaster>();
        pHealth = GetComponent<PlayerHealth>();
        LevelUpCanvas.alpha = 0;
        pMaster.BaseExp = pMaster.ExpRequired;
        NumberPosArray = new Image[pMaster.MaxLevel.ToString().Length];
        for (int i = 0; i < pMaster.MaxLevel.ToString().Length; i++)
            NumberPosArray[i] = Instantiate(NumberPosHolder, new Vector3((NumberPosHolder.sprite.rect.width * i) + numberPos.transform.position.x, numberPos.transform.position.y, numberPos.transform.position.z), Quaternion.identity, pMaster.LevelCanvas.transform) as Image;
    }

    public void AddExp(int expToAdd)
    {
        if (pMaster.CurrentLevel < pMaster.MaxLevel)
        {
            pMaster.CurrentExp += expToAdd;
            if (pMaster.CurrentExp >= pMaster.ExpRequired)
                LevelUp();
        }
        else
            pMaster.CurrentExp = 0;
        SetUI();
    }

    public IEnumerator LevelUpScreen()
    {
        LevelUpText.text = "You have reached level: ";
        while (LevelUpCanvas.alpha < 1)
        {
            if (LevelUpCanvas.alpha < 0.9f)
            {
                LevelUpCanvas.alpha += Time.deltaTime / 0.5f;
                yield return null;
            }
            else
            {
                LevelUpCanvas.alpha = 1;
            }
        }
        yield return new WaitForSecondsRealtime(LevelUpCanvasVisible / 10);
        while (LevelUpCanvas.alpha > 0)
        {
            if (LevelUpCanvas.alpha > 0.01f)
            {
                LevelUpCanvas.alpha -= Time.deltaTime / pMaster.CanvasDelay;
                yield return null;
            }
            else
            {
                levelUpCanvasSpamFilter = false;
                LevelUpCanvas.alpha = 0;
            }
        }
        yield return null;
    }

    public void LevelUp()
    {
    LevelUpGT:
        StopCoroutine("LevelUpScreen");
        levelUpCanvasSpamFilter = false;
        if (pMaster.CurrentLevel != pMaster.MaxLevel)
        {
            pMaster.CurrentExp -= pMaster.ExpRequired;
            pMaster.MaxHealth += 50 + (pMaster.CurrentLevel * pMaster.BaseHealth / 2);
            pMaster.MaxStamina += 25 + (pMaster.CurrentLevel * pMaster.BaseStamina / 2);
            pMaster.ExpRequired += 120 + (pMaster.CurrentLevel * pMaster.BaseExp / 5);
            pMaster.CurrentHealth = pMaster.MaxHealth;
            pMaster.CurrentStamina = pMaster.MaxStamina;
            pMaster.CurrentLevel++;
            if (!levelUpCanvasSpamFilter)
            {
                ShowLevelSprite();
                StartCoroutine("LevelUpScreen");
            }
            if (pMaster.CurrentExp >= pMaster.ExpRequired)
                goto LevelUpGT;
        }
        else
            pMaster.CurrentExp = 0;
        SetUI();
    }

    public void SetUI()
    {
        ExpText.text = pMaster.CurrentExp.ToString() + " / " + pMaster.ExpRequired.ToString();
        LevelText.text = pMaster.CurrentLevel.ToString();
        ExpSlider.maxValue = pMaster.ExpRequired;
        ExpSlider.value = pMaster.CurrentExp;
    }

    private void ShowLevelSprite()
    {
        for (int i = 0; i < pMaster.MaxLevel.ToString().Length; i++)
            NumberPosArray[i].sprite = pMaster.Numbers[int.Parse(pMaster.CurrentLevel.ToString().PadLeft(pMaster.MaxLevel.ToString().Length, '0')[i].ToString())];       
    }
}
