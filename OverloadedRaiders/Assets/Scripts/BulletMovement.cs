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
        mousePoint.z = 0;
        direction = (mousePoint - this.transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += direction * speed * Time.deltaTime;
        Vector3 pos = Camera.main.WorldToViewportPoint(this.transform.position, Camera.MonoOrStereoscopicEye.Mono);
        if (pos.x < -1.0 || pos.x > 2.0 || pos.y < -1.0 || pos.y > 2.0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject);
    }
}
