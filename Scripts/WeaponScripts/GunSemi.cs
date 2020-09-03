using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSemi : GunMaster
{
    public bool isReady = true;
    public int burstFire = 1;
    bool spamFilter = false;

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
        if (isSniper)
            scope();
    }

    public void OnDisable()
    {
        StopCoroutine("shootGun");
        spamFilter = false;
    }

    public IEnumerator shootGun()
    {
        for (int i = 0; i < burstFire; i++)
        {
            isReady = false;
            Shoot();
            yield return new WaitForSecondsRealtime(fireRate / 10);
            isReady = true;
        }
        spamFilter = false;
    }
}
