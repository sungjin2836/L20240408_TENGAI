using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public int BulletLev = 0;
    public int UltCount = 1;
    public GameObject bullet;
    public GameObject bullet2;
    public Transform pos;
    public Transform pos2;

    void Start()
    {
        
    }


    void Update()
    {
        float moveX = moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        float moveY = moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");

        transform.Translate(moveX, moveY, 0);

        if(BulletLev > 2)
        {
            BulletLev = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (BulletLev)
            {
                case 0:
                    NormalShoot();
                    //Instantiate(bullet2, pos2.transform.position, Quaternion.identity);
                    break;
                case 1:
                    NormalShoot();
                    Instantiate(bullet2, pos2.transform.position, Quaternion.identity);
                    break;
                case 2:
                    NormalShoot();
                    Instantiate(bullet2, pos2.transform.position, Quaternion.identity);
                    Instantiate(bullet2, pos2.transform.position, Quaternion.identity);
                    break;
                case 3:
                    NormalShoot();
                    Instantiate(bullet2, pos2.transform.position, Quaternion.identity);
                    Invoke("ShootBullet2", 0.1f);
                    break;
            }
            
        }



        Vector3 viewpos = Camera.main.WorldToViewportPoint(transform.position);
        viewpos.x = Mathf.Clamp(viewpos.x, 0.04f, 0.96f); 
        viewpos.y = Mathf.Clamp(viewpos.y,0.05f, 0.95f); 
        Vector3 worldpos = Camera.main.ViewportToWorldPoint(viewpos);//다시월드좌표로 변환
        transform.position = worldpos; //좌표를 적용한다.



    }

    private void NormalShoot()
    {
        Instantiate(bullet, pos.transform.position, Quaternion.identity);
        Invoke("ShootBullet", 0.1f);
    }

    void ShootBullet()
    {
        Instantiate(bullet, pos.transform.position, Quaternion.identity);
    }
    void ShootBullet2()
    {
        Instantiate(bullet2, pos2.transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            BulletLev++;
            Debug.Log("BulletLev" + BulletLev);
            Destroy(collision.gameObject);
            if(BulletLev > 0)
            {
                pos2.gameObject.SetActive(true);
            }
        }else if (collision.CompareTag("Item2"))
        {
            UltCount++;
            Debug.Log("UltCount" + UltCount);
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("MBullet"))
        {
            BulletLev--;
            if(BulletLev < 0)
            {
                Destroy(gameObject);
            }
        }
    }




}
