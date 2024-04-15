using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public int BulletLev = 0;
    public int UltCount = 1;
    public GameObject bullet;
    public GameObject bullet2;
    public Transform pos;
    public Transform pos2;
    public GameObject item;
    public GameObject gold;

    Animator anim;
    private bool isDeath;

    private int TotalGold = 0;

    void Start()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        float moveX = moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        float moveY = moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");

        transform.Translate(moveX, moveY, 0);

        if(BulletLev > 2)
        {
            BulletLev = 3;
        }

        Vector2 vec2 = pos2.transform.position;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (BulletLev)
            {
                case 0:
                    NormalShoot();
                    break;
                case 1:
                    NormalShoot();
                    Instantiate(bullet2, vec2, Quaternion.identity);
                    break;
                case 2:
                    NormalShoot();
                    Instantiate(bullet2, vec2, Quaternion.identity);
                    Instantiate(bullet2, new Vector2(vec2.x, vec2.y + 0.2f), Quaternion.identity);
                    break;
                case 3:
                    NormalShoot();
                    Instantiate(bullet2, vec2, Quaternion.identity);
                    Instantiate(bullet2, new Vector2(vec2.x, vec2.y + 0.2f), Quaternion.identity);
                    Instantiate(bullet2, new Vector2(vec2.x, vec2.y - 0.2f), Quaternion.identity);
                    break;
            }
            
        }



        Vector3 viewpos = Camera.main.WorldToViewportPoint(transform.position);
        viewpos.x = Mathf.Clamp(viewpos.x, 0.04f, 0.96f); 
        viewpos.y = Mathf.Clamp(viewpos.y,0.05f, 0.95f); 
        Vector3 worldpos = Camera.main.ViewportToWorldPoint(viewpos);//다시월드좌표로 변환
        transform.position = worldpos; //좌표를 적용한다.

        if (isDeath)
        {
            PlayerDeath();
        }

    }

    private void PlayerDeath()
    {
        anim.SetBool("isDeath", true);
        Debug.Log("isDeath"+isDeath);
        Instantiate(item, pos.transform.position, Quaternion.identity);
        Instantiate(gold, pos.transform.position, Quaternion.identity);
        Invoke("SetOff", 1);
    }

    private void SetOff()
    {
        SceneManager.LoadScene("Start");
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
            Destroy(collision.gameObject);
            if(BulletLev > 0)
            {
                pos2.gameObject.SetActive(true);
            }
        }
        if (collision.CompareTag("Item2"))
        {
            UltCount++;
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("MBullet"))
        {
            BulletLev--;
            Debug.Log(BulletLev);
            if(BulletLev < 0)
            {
                isDeath = true;
            }
        }
        if (collision.CompareTag("Gold"))
        {
            TotalGold += 100;
            Debug.Log(TotalGold);
            Destroy(collision.gameObject);
        }
    }




}
