using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] int gameState;
    [SerializeField] GameObject pnlStart;
    [SerializeField] GameObject pnlGame;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void changeState(int state)
    {
        gameState = state;
        switch (gameState)
        {
            case 0:
                pnlStart.SetActive(true);
                pnlGame.SetActive(false);
                break;
            case 1:
                pnlStart.SetActive(false);
                pnlGame.SetActive(true);
                break;
        }
    }
}
