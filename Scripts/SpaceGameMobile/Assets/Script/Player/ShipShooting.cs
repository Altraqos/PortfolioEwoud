using UnityEngine;

public class ShipShooting : MonoBehaviour
{
    public GameObject bulletPref;
    public Transform[] weaponHardPoints;
    public AudioSource aSource;
    public AudioClip aClip;

    public int maxCount;
    public int currentCount = 0;
    public void Shoot()
    {
        maxCount = weaponHardPoints.Length -1;
        Instantiate(bulletPref, weaponHardPoints[currentCount].position, gameObject.transform.rotation);
        currentCount++;
        if (currentCount > maxCount)
            currentCount = 0;
        aSource.clip = aClip;
        aSource.Play();
    }
}
