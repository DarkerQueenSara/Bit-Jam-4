using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public InputAction shoot;

    [SerializeField] private GameObject projectileGO;
    [SerializeField] private GameObject projectileGOStartPos;
    [SerializeField] private Camera playerCamera;
    //
    private float isShooting = 0;
    [SerializeField] private float rateOfFire = 2.0f;
    private float shootingTimer = 2.0f;
    [SerializeField] private float projectileSpeed = 10.0f;
    //
    [SerializeField] private int playerScore = 0;
    [SerializeField] private int playerHp = 5;

    private LayerMask layerMask;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        layerMask = LayerMask.GetMask("Enemy");
        shoot = InputSystem.actions.FindAction("Attack");
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void FixedUpdate()
    {
        shootingTimer += Time.deltaTime;
        if (shootingTimer >= rateOfFire)
        {
            shootingTimer = rateOfFire + 0.5f;
        }

        isShooting = shoot.ReadValue<float>();
        if (isShooting != 0 && shootingTimer >= rateOfFire)
        {
            Shoot();
            shootingTimer = 0.0f;
        }
    }

    private void Shoot()
    {
        
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.SphereCast(transform.position, 1.5f, playerCamera.transform.forward, out hit, Mathf.Infinity, layerMask))

        {
            Debug.DrawRay(transform.position, playerCamera.transform.forward * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
            Destroy(hit.collider.gameObject);
            AddScore(50);
        }
        else
        {
            Debug.DrawRay(transform.position, playerCamera.transform.forward * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
        
        /*GameObject projectile = Instantiate(projectileGO, projectileGOStartPos.transform.position, projectileGOStartPos.transform.rotation);
        projectile.GetComponent<Projectile>().SetPlayer(this);
        Rigidbody projectileRB = projectile.GetComponent<Rigidbody>();
        projectileRB.linearVelocity = playerCamera.transform.forward * projectileSpeed;*/
    }

    public void Hit()
    {
        playerHp -= 1;
        print("OWIE!! PLAYER HP: " + playerHp);
        if (playerHp <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        print("GAME OVER I DEAD LMAO KEKW");
    }

    public float GetScore()
    {
        return playerScore;
    }

    public void AddScore(int newScore)
    {
        playerScore += newScore;
        print("NEW SCORE: " + newScore);
    }
}
