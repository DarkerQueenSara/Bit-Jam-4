using System;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Player player;

    [SerializeField] private GameObject enemyProjectileGO;
    [SerializeField] private GameObject enemyProjectileGOStartPos;
    //
    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private float rateOfFire = 2.0f;
    private float shootingTimer = 0.0f;
    [SerializeField] private float projectileSpeed = 1.0f;
    //Enemy behavior type
    enum EnemyType { CHASE, SHOOT, BOTH};
    private EnemyType enemyType = EnemyType.CHASE;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DefineRandomBehavior();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            transform.LookAt(player.transform);
        }

        switch (enemyType)
        {
            case EnemyType.CHASE:
                print("CHASE");
                ChasePlayer();
                break;
            case EnemyType.SHOOT:
                
                shootingTimer += Time.deltaTime;
                if (shootingTimer > rateOfFire)
                {
                    ShootPlayer();
                    print("SHOOT PLAYER");
                    shootingTimer = 0.0f;
                }
                break;
            case EnemyType.BOTH:
                shootingTimer += Time.deltaTime;
                if (shootingTimer > rateOfFire)
                {
                    ShootPlayer();
                    print("SHOOT AND CHASE");
                    shootingTimer = 0.0f;
                }
                ChasePlayer();
                break;
        }
    }

    private void DefineRandomBehavior()
    {
        int random_behavior = UnityEngine.Random.Range(0, Enum.GetValues(typeof(EnemyType)).Length);
        enemyType = (EnemyType)random_behavior;
        print("MY TYPE IS " + enemyType);
        switch (enemyType)
        {
            case EnemyType.CHASE:
                this.name = "ChasingEnemy";
                break;
            case EnemyType.SHOOT:
                this.name = "ShootingEnemy";
                break;
            case EnemyType.BOTH:
                this.name = "ShootAndChaseEnemy";
                break;
        }
    }

    private void ChasePlayer()
    {
        if (player != null)
        {
            transform.position += moveSpeed * Time.deltaTime * transform.forward;
        }
    }

    void ShootPlayer()
    {
        if (enemyProjectileGO != null)
        {
            GameObject enemyProjectile = Instantiate(enemyProjectileGO, enemyProjectileGOStartPos.transform.position, enemyProjectileGOStartPos.transform.rotation);
            Rigidbody enemyProjectileRB = enemyProjectile.GetComponent<Rigidbody>();
            enemyProjectileRB.linearVelocity = (player.transform.position - this.transform.position) * projectileSpeed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("I, AN ENEMY, HAVE JUST TACKLED THE PLAYER");
            Destroy(this.gameObject);
            collision.gameObject.GetComponent<Player>().Hit();
        }
    }
}
