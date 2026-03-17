using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public InputAction shoot;
    [SerializeField] private Camera playerCamera;
    //
    private float isShooting = 0;
    [SerializeField] private float bpm = 120.0f; //shots per minute
    private float shootingTimer = 0.0f;
    //
    public int playerScore = 0;
     public int playerHp = 5;

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
        shootingTimer += (Time.fixedDeltaTime);

        isShooting = shoot.ReadValue<float>();
        if (isShooting != 0 && shootingTimer >= 60.0f / bpm)
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
            Debug.Log("HIT AN ENEMY");
            Destroy(hit.collider.gameObject);
            AddScore(50);
        }
        else
        {
            Debug.Log("Did not Hit");
        }
    }

    public void Hit()
    {
        playerHp -= 1;
        print("PLAYER GOT HIT! PLAYER HP: " + playerHp);
        if (playerHp <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        GameManager.Instance.lastScore = playerScore;
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1));
    }

    public float GetScore()
    {
        return playerScore;
    }

    public void AddScore(int newScore)
    {
        playerScore += newScore;
        print("ADDED SCORE: " + newScore + ". NEW SCORE: " + playerScore);
    }
}
