using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;

    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.CompareTag("Enemy"))
        {
            Destroy(hitInfo.gameObject);
            Destroy(gameObject);
        }
        else if (hitInfo.gameObject.CompareTag("Wall") || hitInfo.gameObject.CompareTag("Ground"))
        {
            //Bounce bullet off walls
            speed *= -1;
            rb.velocity = transform.right * speed;
            
            //Destroy(gameObject);
        }

    }


}
