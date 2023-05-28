using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("goAway");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator goAway() {
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}
