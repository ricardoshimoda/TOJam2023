using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float basicSpeed;
    [SerializeField] GameObject player;

    public void setPlayer(GameObject setPlayer) {
        player = setPlayer;
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

        }
    }
}
