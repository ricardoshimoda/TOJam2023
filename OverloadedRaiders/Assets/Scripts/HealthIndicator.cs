using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class HealthIndicator : MonoBehaviour
{

    [SerializeField] Player player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            var finalHealth = Mathf.Clamp(player.health, 0, 100);
            RectTransform indicator = this.GetComponent<RectTransform>();
            indicator.sizeDelta = new Vector2(200 * (finalHealth / 100.0f), 50);
        }
    }
}
