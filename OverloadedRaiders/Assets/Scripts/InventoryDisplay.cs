using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] GameObject[] gallery;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i <4; i++)
        {
            if(i < player.currentWeapon)
            {
                gallery[i].SetActive(true);
            } else
            {
                gallery[i].SetActive(false);
            }
        }
        if (player.powerUps > 40)
        {
            gallery[3].SetActive(true);
        }

    }
}
