using System.Collections;
using UnityEngine;

public class GunAuto : GunMaster
{
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
        while (Input.GetKey(KeyCode.Space))
        {
            Shoot();
            yield return new WaitForSecondsRealtime(fireRate);
        }
        spamFilter = false;
    }
}