using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public Transform firePoint;
    public float damagePerPellet = 4f;
    public int pelletCount = 8;
    public float spreadAngle = 5f;
    public float fireRate = 1f;
    public float range = 50f;

    public ParticleSystem muzzleFlash;  // <-- assign this in Inspector

    private float nextFire = 0f;

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFire)
        {
            nextFire = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        

        

        // Force-reset muzzle flash
        if (muzzleFlash != null)
        {
            muzzleFlash.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            muzzleFlash.Play();
        }

        // Fire pellets
        for (int i = 0; i < pelletCount; i++)
        {
            Vector3 direction = SpreadDirection();
            RaycastHit hit;

            if (Physics.Raycast(firePoint.position, direction, out hit, range))
            {
                Enemy enemy = hit.transform.GetComponent<Enemy>();
                if (enemy != null)
                    enemy.TakeDamage(damagePerPellet);
            }
        }
    }


    Vector3 SpreadDirection()
    {
        float x = Random.Range(-spreadAngle, spreadAngle);
        float y = Random.Range(-spreadAngle, spreadAngle);

        return (firePoint.forward + new Vector3(x, y, 0) * 0.01f).normalized;
    }
}
