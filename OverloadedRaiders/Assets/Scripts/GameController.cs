using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] int gameState;
    [SerializeField] GameObject pnlStart;
    [SerializeField] GameObject pnlGame;
    [SerializeField] float spawnTimeMS;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject player;
    [SerializeField] Transform enemyRoot;

    float currentTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (spawnTimeMS <= 0)
        {
            spawnTimeMS = 500;
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime * 1000;
        if(currentTime > spawnTimeMS && gameState == 1)
        {
            currentTime -= spawnTimeMS;
            spawnEnemy();
        }
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

    private void spawnEnemy()
    {
        /* generate a random position */
        float randX;
        float randY;
        if(Random.value < 0.5f)
        {
            randX = Random.Range(-1.0f, -0.1f);
        } else
        {
            randX = Random.Range(1.1f, 2.0f);
        }
        if (Random.value < 0.5f)
        {
            randY = Random.Range(-1.0f, -0.1f);
        }
        else
        {
            randY = Random.Range(1.0f, 2.0f);
        }
        var initialPosition = Camera.main.ViewportToWorldPoint(new Vector3(randX, randY, Camera.main.nearClipPlane));
        var currentEnemy = Instantiate(enemy, enemyRoot);
        currentEnemy.transform.position = initialPosition;
        var enemyMovement = currentEnemy.GetComponent<EnemyMovement>();
        enemyMovement.setPlayer(player);

    }
}
