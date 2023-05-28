using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] float basicSpeed;
    [SerializeField] public float health;
    [SerializeField] public int currentWeapon;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletAnchor;
    [SerializeField] public int powerUps;
    [SerializeField] public int invencibilityTimeMS;
    [SerializeField] public bool gotToPortal = false;
    [SerializeField] GameObject punch;
    [SerializeField] GameObject knife;
    [SerializeField] GameObject sword;

    bool invencibility = false;
    public bool interactive = false;
    Animator playerAnimator;
    bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (interactive && health > 0)
        {
            movement();
            fire();
        }
        if(health <= 0 || gotToPortal)
        {
            playerAnimator.SetBool("IsMoving", false);
            if(health <= 0)
            {
                playerAnimator.speed = 0;
                var renderer = GetComponent<SpriteRenderer>();
                renderer.color = Color.clear;
            }
        }
    }

    private void movement() {
        var hSpeed = Input.GetAxisRaw("Horizontal");
        var vSpeed = Input.GetAxisRaw("Vertical");        
        var speed = basicSpeed * new Vector3(hSpeed, vSpeed, 0).normalized * Time.deltaTime;
        playerAnimator.SetBool("IsMoving", speed.magnitude > 0);
        transform.position += speed;
        if(facingRight && speed.x < 0)
        {
            facingRight = false;
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        if(!facingRight && speed.x > 0)
        {
            facingRight = true;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void fire() {
        if (Input.GetButtonDown("Fire1")) {
            switch(currentWeapon)
            {
                case 0:
                    // punch
                    
                    break;
                case 1:
                    // knife
                    break;
                case 2:
                    // sword
                    break;
                case 3:
                    // pistol
                    GameObject currentBullet = Instantiate(bullet, this.transform);
                    currentBullet.transform.SetParent(bulletAnchor);
                    break;
            }
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
