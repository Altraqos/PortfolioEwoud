using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunProjectileLauncher : MonoBehaviour
{
    public Transform instantiatePoint;

    public GameObject projectilePrefab;

    public Component[] pSystems;

    public Camera playerCam;

    public float projectileSpeed;
    public float reloadTime = 1.5f;
    public float fireRate;

    public int projectileCount;
    public int damage = 10;
    public int ammoInGun = 30;
    public int maxAmmoInGun = 30;

    bool spamFilter = false;
    public bool isReady = true;

    public void Start()
    {
        pSystems = GetComponentsInChildren(typeof(ParticleSystem));
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!spamFilter)
            {
                spamFilter = true;
                StartCoroutine("shootGun");
            }
        }
    }

    public void Shoot()
    {
        if (ammoInGun > 0)
        {
            ammoInGun -= 1;
            foreach (ParticleSystem pSystem in pSystems)
            {
                pSystem.Stop();
                pSystem.Play();
            }
            GameObject projectileHolder;
            projectileHolder = Instantiate(projectilePrefab, instantiatePoint.position, Quaternion.identity);
            projectileHolder.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * projectileSpeed);
            if (ammoInGun - 1 == 0)
                StartCoroutine("Reload");
        }
        else
            StartCoroutine("Reload");
    }

    public IEnumerator shootGun()
    {
        isReady = false;
        Shoot();
        yield return new WaitForSecondsRealtime(fireRate / 10);
        isReady = true;
        spamFilter = false;
    }

    public IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        ammoInGun = maxAmmoInGun;
    }
}
