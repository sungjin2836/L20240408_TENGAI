using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public GameObject bullet;
    public Transform pos;

    void Start()
    {
        
    }


    void Update()
    {
        float moveX = moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        float moveY = moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");

        transform.Translate(moveX, moveY, 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bullet, pos.transform.position, Quaternion.identity);
        }

    }




}
