using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Transform minPosition;
    [SerializeField] Transform maxPosition;
    float constZ;

    // Start is called before the first frame update
    void Start()
    {
        constZ = this.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        var finalX = Mathf.Clamp(player.transform.position.x, minPosition.position.x, maxPosition.position.x);
        var finalY = Mathf.Clamp(player.transform.position.y, minPosition.position.y, maxPosition.position.y);
        this.transform.position = new Vector3(finalX, finalY, constZ);

    }
}
