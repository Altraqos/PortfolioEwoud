using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceRecognition : MonoBehaviour
{
    public ParticleSystem pSystem;
    public ParticleSystem pSystemFailed;
    public float cRed;
    public float cGreen;
    public float cBlue;
    public float cAlpha;
    public float sDuration = 2;
    private KeywordRecognizer kRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    public bool hasFailed = false;

    public bool testSpell = false;

    public Light lumosLight;
    bool lumos = false;

    public void OnEnable()
    {
		pSystem.Stop();
        pSystemFailed.Stop();
        actions.Add("kedavra", AvadaKedavra);
        actions.Add("crucio", Crucio);
        actions.Add("aquamenti", Aguamenti);
        actions.Add("alohomora", Alohomora);
        actions.Add("reducto", Reducto);
        actions.Add("lumos", Lumos);
        kRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        kRecognizer.OnPhraseRecognized += recognizedSpeech;
        kRecognizer.Start();
    }

    private void recognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    public void Update()
    {
        if (testSpell)
        {
            testSpell = false;
            DefaultSpellFunction();
        }
    }

    private void DefaultSpellFunction()
    {
        pSystem.Stop();
        StopCoroutine("spellEffect");
        cRed = 0;
        cGreen = 255;
        cBlue = 0;
        cAlpha = 255;

        if (hasFailed)
            StartCoroutine("spellEffectFailed");

        if (!hasFailed)
        {
            StartCoroutine("spellEffect");
        }

        /*
         * Add the main spell functionality in here.
         * Set the colours above to whatever the spell needs to be, add the real spell underneath.
         * And finally start the spell coroutine.
         */

    }

    private void Lumos()
    {
        lumos = !lumos;
        if (lumos)
            lumosLight.enabled = true;
        if (!lumos)
            lumosLight.enabled = false;
    }
    
    private void AvadaKedavra()
    {
        pSystem.Stop();
        StopCoroutine("spellEffect");
        cRed = 0;
        cGreen = 255;
        cBlue = 0;
        cAlpha = 255;

        if (hasFailed)
            StartCoroutine("spellEffectFailed");

        if (!hasFailed)
        {
            StartCoroutine("spellEffect");
        }

    }

    private void Crucio()
    {
        pSystem.Stop();
        StopCoroutine("spellEffect");
        cRed = 210;
        cGreen = 0;
        cBlue = 0;
        cAlpha = 255;



        if (hasFailed)
            StartCoroutine("spellEffectFailed");

        if (!hasFailed)
        {
            StartCoroutine("spellEffect");
        }
    }

    private void Aguamenti()
    {
        pSystem.Stop();
        StopCoroutine("spellEffect");
        cRed = 0;
        cGreen = 50;
        cBlue = 210;
        cAlpha = 255;



        if (hasFailed)
            StartCoroutine("spellEffectFailed");

        if (!hasFailed)
        {
            StartCoroutine("spellEffect");
        }
    }

    private void Alohomora()
    {
        pSystem.Stop();
        StopCoroutine("spellEffect");
        cRed = 255;
        cGreen = 255;
        cBlue = 0;
        cAlpha = 255;



        if (hasFailed)
            StartCoroutine("spellEffectFailed");

        if (!hasFailed)
        {
            StartCoroutine("spellEffect");
        }
    }

    private void Reducto()
    {
        pSystem.Stop();
        StopCoroutine("spellEffect");
        cRed = 224;
        cGreen = 10;
        cBlue = 10;
        cAlpha = 255;


        if (hasFailed)
            StartCoroutine("spellEffectFailed");

        if (!hasFailed)
        {
            StartCoroutine("spellEffect");
        }
    }

    public IEnumerator spellEffect()
    {
        var main = pSystem.main;
        main.startColor = new Color(cRed, cGreen, cBlue, cAlpha);
        pSystem.Play();
        yield return new WaitForSecondsRealtime(sDuration);
        pSystem.Stop();
        StopCoroutine("spellEffect");
    }
    
    public IEnumerator spellEffectFailed()
    {
        var main = pSystem.main;
        main.startColor = new Color(cRed, cGreen, cBlue, cAlpha);
        pSystemFailed.Play();
        yield return new WaitForSecondsRealtime(sDuration);
        pSystemFailed.Stop();
        StopCoroutine("spellEffect");
    }
}
