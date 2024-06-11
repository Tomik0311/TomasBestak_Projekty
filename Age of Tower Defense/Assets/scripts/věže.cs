using System.Collections;
using UnityEngine;


public class věže : MonoBehaviour

    
{
    public Transform target;
    private EnemyAi targetEnemy;

    [Header("Attributes")]

    public float range = 15f;
    public GameObject bulletPrefab; 
    public float fireRate = 1f;
    public float fireCountDown = 0f;

    [Header("Unity Setup Fields")]

    public string EnemyTag = "Enemy";

    public Transform PartToRotate;
    public float TurnSpeed = 10f;
   
    public Transform firePoint;

   
    
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);  
    }
    private void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(EnemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies)
        {
            float distanceEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceEnemy < shortestDistance)
            {
                shortestDistance = distanceEnemy;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null && shortestDistance <= range) 
        {
            target = nearestEnemy.transform;

        }
        else
        {
            target = null;
        }
    }

    
    void Update()
    {
        if(target == null)
        
           
                return;


                Vector3 dir = target.position - transform.position;
                Quaternion lookRotation = Quaternion.LookRotation(dir);
                Vector3 rotation = lookRotation.eulerAngles;
                PartToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCountDown <= 0f)
        {
            Debug.Log("Firing!");
            Shoot();
            fireCountDown = 1f / fireRate;
        }
    
            fireCountDown -= Time.deltaTime;
        
                    
        

    }

     void Shoot()
    {
        GameObject bulletGo = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGo.GetComponent<Bullet>();

        if(bullet != null)
        {
            bullet.Seek(target);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }


}
