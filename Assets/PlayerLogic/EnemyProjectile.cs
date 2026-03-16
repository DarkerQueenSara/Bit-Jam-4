using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float lifeTime = 3.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        print("ENEMY PROJECTILE AAAAAAAAAAAAA");
        Destroy(this.gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("I HIT THE PLAYER HAHAHAAAAAAAAAA");
            Destroy(this.gameObject);
            collision.gameObject.GetComponent<Player>().Hit(); //hit player
        }
    }
}
