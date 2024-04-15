using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 15f;

    void Start()
    {

    }


    void Update()
    {
        transform.Translate(Vector2.right * Speed * Time.deltaTime);
        
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Boss"))
        {
        Destroy(gameObject);
        }
    }

}
