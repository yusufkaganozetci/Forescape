using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleBullet : MonoBehaviour,IBullet
{
    [SerializeField] float speed;
    [SerializeField] float bulletDamage;
    private Rigidbody2D rb;
    private Vector2 bulletDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.velocity = speed * Time.deltaTime * (bulletDirection.normalized);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<IEnemy>() != null)
        {
            collision.GetComponent<IEnemy>().TakeDamage(bulletDamage);
            Destroy(gameObject);
        }
        else if (!collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

    }

    public void ChangeBulletDirection(Vector2 newDirection)
    {
        bulletDirection = newDirection;
    }
}
