using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public PlayerMaster pMaster;
    public Slider StaminaSlider;
    public Slider HealthSlider;
    public CanvasGroup DamageCanvas;
    public CanvasGroup BloodSplatter;
    public Text StaminaText;
    public Text HealthText;
    bool DamageCanvasCoroutineSpamFilter = false;
    bool BloodSplatterCanvasCoroutineSpamFilter = false;

    private void OnEnable()
    {
        SetInitialReferences();
        SetUI();
    }

    public void SetInitialReferences()
    {
        pMaster = GetComponentInParent<PlayerMaster>();
        DamageCanvas.alpha = 0;
        BloodSplatter.alpha = 0;
        pMaster.MaxHealth = pMaster.BaseHealth;
        pMaster.MaxStamina = pMaster.BaseStamina;
        pMaster.CurrentHealth = pMaster.MaxHealth;
        pMaster.CurrentStamina = pMaster.MaxStamina;
    }

    public void LoadBloodSplatter()
    {
        if (!BloodSplatterCanvasCoroutineSpamFilter)
        {
            BloodSplatterCanvasCoroutineSpamFilter = true;
            StartCoroutine("BloodSplatterFade");
        }
    }

    public void IncreaseHealth(int amount)
    {
        pMaster.CurrentHealth += amount;
        if (pMaster.CurrentHealth > pMaster.MaxHealth)
        {
            pMaster.CurrentHealth = pMaster.MaxHealth;
        }
        SetUI();
    }

    public void DeductHealth(int amount)
    {
        BloodSplatter.enabled = true;
        StopCoroutine("DamageCanvasFade");
        StopCoroutine("DamageCanvasIncreaseHealth");
        DamageCanvasCoroutineSpamFilter = false;
        BloodSplatterCanvasCoroutineSpamFilter = false;
        pMaster.CurrentHealth -= amount;
        SetUI();
        if (pMaster.CurrentHealth <= 0)
        {
            pMaster.CurrentHealth = 0;
            if (pMaster.CurrentHealth > pMaster.MaxHealth / 100 * 20 && pMaster.CurrentHealth > 0)
            {
                if (!DamageCanvasCoroutineSpamFilter)
                {
                    DamageCanvas.enabled = true;
                    DamageCanvasCoroutineSpamFilter = true;
                    StartCoroutine("DamageCanvasFade");
                }
            }
            else
            {
                if (!DamageCanvasCoroutineSpamFilter && pMaster.CurrentHealth > 0)
                {
                    DamageCanvasCoroutineSpamFilter = true;
                    StartCoroutine("DamageCanvasIncreaseHealth");
                    BloodSplatter.alpha = 1;
                }
            }
        }
    }

    public IEnumerator DamageCanvasIncreaseHealth()
    {
        DamageCanvas.alpha = 1;
        while (DamageCanvas.alpha > 0)
        {
            SetUI();
            if (DamageCanvas.alpha > 0.01f)
            {
                if (pMaster.CurrentHealth < pMaster.MaxHealth / 100 * 20)
                {
                    DamageCanvas.alpha -= Time.deltaTime / pMaster.CanvasDelay;
                    pMaster.CurrentHealth += 1;
                    yield return new WaitForSecondsRealtime(0.2f);
                }
                else
                {
                    DamageCanvas.alpha -= Time.deltaTime / pMaster.CanvasDelay;
                    yield return null;
                }
            }
            else
            {
                BloodSplatterCanvasCoroutineSpamFilter = false;
                DamageCanvas.alpha = 0;
            }
        }
        SetUI();
        yield return null;
    }

    public IEnumerator DamageCanvasFade()
    {
        DamageCanvas.alpha = 1;
        while (DamageCanvas.alpha > 0)
        {
            if (DamageCanvas.alpha > 0.01f)
            {
                DamageCanvas.alpha -= Time.deltaTime / pMaster.CanvasDelay;
                yield return null;
            }
            else
            {
                DamageCanvasCoroutineSpamFilter = false;
                DamageCanvas.alpha = 0;
            }
        }
        yield return null;
    }

    public IEnumerator BloodSplatterFade()
    {
        yield return new WaitForSecondsRealtime(4.5f);
        while (BloodSplatter.alpha > 0)
        {
            if (BloodSplatter.alpha > 0.01f)
            {
                BloodSplatter.alpha -= Time.deltaTime / pMaster.CanvasDelay;
                yield return null;
            }
            else
            {
                BloodSplatterCanvasCoroutineSpamFilter = false;
                BloodSplatter.alpha = 0;
            }
        }
        yield return null;
    }

    public void SetUI()
    {
        HealthText.text = pMaster.CurrentHealth.ToString();
        HealthSlider.maxValue = pMaster.MaxHealth;
        HealthSlider.value = pMaster.CurrentHealth;
        StaminaText.text = pMaster.CurrentStamina.ToString();
        StaminaSlider.maxValue = pMaster.MaxStamina;
        StaminaSlider.value = pMaster.CurrentStamina;
    }
}
