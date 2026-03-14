using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public InputAction shoot;

    [SerializeField] private GameObject projectileGO;
    [SerializeField] private GameObject projectileGOStartPos;
    [SerializeField] private Camera playerCamera;
    private float isShooting = 0;
    [SerializeField] private float rateOfFire = 2.0f;
    private float shootingTimer = 2.0f;
    [SerializeField] private float projectileSpeed = 10.0f;
    [SerializeField] private int playerScore = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
        GameObject projectile = Instantiate(projectileGO, projectileGOStartPos.transform.position, projectileGOStartPos.transform.rotation);
        Rigidbody projectileRB = projectile.gameObject.GetComponent<Rigidbody>();
        projectileRB.linearVelocity = playerCamera.transform.forward * projectileSpeed;

    }
}
