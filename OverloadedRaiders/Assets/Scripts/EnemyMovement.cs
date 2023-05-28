using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float basicSpeed;
    [SerializeField] GameObject player;
    [SerializeField] GameObject powerUp;
    [SerializeField] Transform powerUpRoot;
    [SerializeField] public float damage;

    public void setBasicInfo(GameObject setPlayer, Transform setPowerUpRoot) {
        player = setPlayer;
        powerUpRoot = setPowerUpRoot;
    }

    public void stop()
    {
        basicSpeed = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // calculates the vector between the enemy and the player
        if (player != null)
        {
            var pursuit = player.transform.position - this.transform.position;
            var speed = pursuit.normalized * Time.deltaTime * basicSpeed;
            this.transform.position += speed;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        var powerUpGO = Instantiate(powerUp, powerUpRoot);
        powerUpGO.transform.position = this.transform.position;
        Destroy(this.gameObject);
    }

}
