using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifeTime = 3.0f;
    [SerializeField] private float damage = 10.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        print("AAAAAAAAAAAAAAAA");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            print("ENEMY, AAAAAAAAAAAAA");
            Destroy(this.gameObject);
        }
    }
}
