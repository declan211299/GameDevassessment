using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int ID;
    public float maxHealth;
    public float health;
    public float speed;

    private Crop targetCrop;

    // Init MUST take EnemySummonData, not Enemy
    public void Init(EnemySummonData data)
    {
        ID = data.ID;
        maxHealth = data.maxHealth;
        health = maxHealth;
        speed = data.speed;

        targetCrop = CropManager.GetClosestCrop(transform.position);
    }

    void Update()
    {
        if (targetCrop == null)
        {
            Debug.Log("Enemy cannot find a crop!");
        }

        if (targetCrop == null)
            targetCrop = CropManager.GetClosestCrop(transform.position);

        if (targetCrop == null)
            return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetCrop.transform.position,
            speed * Time.deltaTime
        );

        float dist = Vector3.Distance(transform.position, targetCrop.transform.position);

        if (dist < 0.2f)
        {
            CropManager.DestroyCrop(targetCrop);
            EnemyManager.ReturnEnemy(this);
        }
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            EnemyManager.ReturnEnemy(this);
        }
    }
}
