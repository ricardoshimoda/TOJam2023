using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] float speed;
    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        var mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePoint - this.transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += direction * speed * Time.deltaTime;
        Vector3 pos = Camera.main.WorldToViewportPoint(this.transform.position);
        if (pos.x < 0.0 || pos.x > 1.0 || pos.y < 0.0 || pos.y > 1.0)
        {
            Destroy(this);
        }

    }
}
