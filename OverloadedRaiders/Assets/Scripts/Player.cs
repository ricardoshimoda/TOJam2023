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
    [SerializeField] GameObject slash;
    [SerializeField] GameObject[] meleeHitBox;
    [SerializeField] int[] meleeDurationMS;
    [SerializeField] float[] meleeSize;
    [SerializeField] GameObject slashRoot;


    bool invencibility = false;
    public bool interactive = false;
    Animator playerAnimator;
    bool facingRight = true;
    GameObject meleeSlash;

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
                case 1:
                case 2:
                    meleeHitBox[currentWeapon].SetActive(true);
                    meleeSlash = Instantiate(slash, slashRoot.transform);
                    meleeSlash.transform.localScale = new Vector3(meleeSize[currentWeapon], meleeSize[currentWeapon], 1);
                    StartCoroutine("disableMelee");
                    break;
                case 3:
                    // pistol
                    GameObject currentBullet = Instantiate(bullet, slashRoot.transform);
                    currentBullet.transform.SetParent(bulletAnchor);
                    break;
            }
        }
    }

    private IEnumerator disableMelee() {
        var currentTimerMS = 0;
        GameObject currentGameObject = null;
        switch (currentWeapon)
        {
            case 0:
            case 1:
            case 2:
                currentTimerMS = meleeDurationMS[currentWeapon];
                currentGameObject = meleeHitBox[currentWeapon];
                break;
        }
        yield return new WaitForSeconds(currentTimerMS / 1000);
        currentGameObject.SetActive(false);
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
            if (powerUps >= 10 && powerUps < 20)
            {
                currentWeapon = 1;
            }
            else if (powerUps >= 20 && powerUps < 30)
            {
                currentWeapon = 2;
            }
            if (powerUps >= 30 && powerUps < 40)
            {
                currentWeapon = 3;
            }
        }
        else if (collision.tag == "Portal")
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
