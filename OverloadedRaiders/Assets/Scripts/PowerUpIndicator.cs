using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpIndicator : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] GameController controller;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            var final = Mathf.Clamp(player.powerUps, 0, controller.powerUpGoal);
            RectTransform indicator = this.GetComponent<RectTransform>();
            indicator.sizeDelta = new Vector2(200 * ((float)final / (float)controller.powerUpGoal), 50);
        }

    }

}
