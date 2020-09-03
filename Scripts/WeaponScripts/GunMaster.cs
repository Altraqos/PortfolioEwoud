using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GunMaster : MonoBehaviour
{
    public int damage = 10;
    public int ammoInGun = 30;
    public int maxAmmoInGun = 30;
    //1 laten tenzij het een shotgun is of multishot moet hebben
    public int pelletCount = 1;

    public float reloadTime = 1.5f;
    public float fireRate;
    public float spreadAmount = 3;
    public float weaponRange = 100;

    public Component[] pSystems;

    public Camera playerCam;

    [Header("Sniper Settings")]
    public bool isSniper;
    public bool isScoping;
    [Space(10)]
    public Texture sniperImage;
    public Canvas sniperCanvas;
    public RawImage sniperRaw;
    public KeyCode scopeKey;

    public void Start()
    {
        pSystems = GetComponentsInChildren(typeof(ParticleSystem));
        if (isSniper)
        {
            sniperCanvas = GetComponentInChildren<Canvas>();
            sniperRaw = sniperCanvas.GetComponentInChildren<RawImage>();
            sniperRaw.texture = sniperImage;
            sniperCanvas.enabled = false;
        }
    }

    public void Shoot()
    {
        if (ammoInGun > 0)
        {
            ammoInGun -= 1;
            for (int i = 0; i < pelletCount; i++)
            {
                foreach (ParticleSystem pSystem in pSystems)
                {
                    pSystem.Stop();
                    pSystem.Play();
                }

                //Bereken de spread van de kogel negatief en positief zodat het alle kanten opkan
                if (!isScoping)
                {
                    Vector3 forwardVector = Vector3.forward;
                    float deviation = Random.Range(-spreadAmount, spreadAmount);
                    float angle = Random.Range(-spreadAmount, spreadAmount);
                    forwardVector = Quaternion.AngleAxis(deviation, Vector3.up) * forwardVector;
                    forwardVector = Quaternion.AngleAxis(angle, Vector3.forward) * forwardVector;
                    forwardVector = playerCam.transform.rotation * forwardVector;

                    //Later de rest van de raycast hieronder neerzetten

                    //Teken de rayCast uit in de directie die de spread aangemaakt heeft
                    Debug.DrawRay(playerCam.transform.position, playerCam.transform.rotation * forwardVector * weaponRange, Color.green, 200);
                }
                else if(isSniper && isScoping)
                {
                    Debug.DrawRay(playerCam.transform.position, playerCam.transform.rotation * Vector3.forward * weaponRange, Color.green, 200);
                }
            }
            if (ammoInGun - 1 == 0)
                StartCoroutine("Reload");
        }
        else
            StartCoroutine("Reload");
    }

    public void scope()
    {
        isScoping = Input.GetKey(scopeKey);
        sniperCanvas.enabled = Input.GetKey(scopeKey);
    }

    public IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
            ammoInGun = maxAmmoInGun;
    }
}
