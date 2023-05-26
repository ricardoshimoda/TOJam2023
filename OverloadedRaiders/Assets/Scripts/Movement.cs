using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float basicSpeed;
    [SerializeField] public float health;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (health > 0)
        {
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
    }
}
