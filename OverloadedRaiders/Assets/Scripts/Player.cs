using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float basicSpeed;
    [SerializeField] public float health;
    [SerializeField] public int weapon;
    [SerializeField] Camera currentCamera;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletAnchor;
    [SerializeField] public int powerUps;
    [SerializeField] public int invencibilityTimeMS;
    [SerializeField] public bool gotToPortal;

    bool invencibility = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (health > 0)
        {
            movement();
            fire();
        }
    }

    private void movement() {
        var hSpeed = Input.GetAxisRaw("Horizontal");
        var vSpeed = Input.GetAxisRaw("Vertical");
        var speed = basicSpeed * new Vector3(hSpeed, vSpeed, 0).normalized * Time.deltaTime;
        var finalPosition = transform.position + speed;
        // checks final position before letting player out of sight
        Vector3 pos = Camera.main.WorldToViewportPoint(finalPosition);
        if (pos.x < 0.0 || pos.x > 1.0)
        {
            speed.x = 0;
        }
        if (pos.y < 0.0 || pos.y > 1.0)
        {
            speed.y = 0;
        }
        transform.position += speed;
    }

    private void fire() {
        if (weapon == 1 && Input.GetButtonDown("Fire1"))
        {
            GameObject currentBullet = Instantiate(bullet, this.transform);
            currentBullet.transform.SetParent(bulletAnchor);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.tag);
        if(collision.tag == "Enemy" && !invencibility)
        {
            invencibility = true;
            StartCoroutine("EndInvencibility");
            var enemyData = collision.GetComponent<EnemyMovement>();
            health -= enemyData.damage;
        } else if (collision.tag == "PowerUp")
        {
            powerUps++;
        } else if (collision.tag == "Portal")
        {
            // End of level!
            gotToPortal = true;
        }

    }

    private IEnumerator EndInvencibility() {
        yield return new WaitForSeconds(invencibilityTimeMS / 1000);
        invencibility = false;
    }


}
