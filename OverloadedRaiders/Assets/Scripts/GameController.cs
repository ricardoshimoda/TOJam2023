using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] int gameState;
    [SerializeField] GameObject pnlStart;
    [SerializeField] GameObject pnlGame;
    [SerializeField] GameObject pnlLevelTransition;
    [SerializeField] GameObject pnlDeath;
    [SerializeField] float spawnTimeMS;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject player;
    [SerializeField] Transform enemyRoot;
    [SerializeField] Transform powerUpRoot;
    [SerializeField] public int powerUpGoal;

    float currentTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (spawnTimeMS <= 0)
        {
            spawnTimeMS = 500;
        }
        changeState(1);
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
        var playerData = player.GetComponent<Player>();
        if(playerData.health <= 0)
        {
            changeState(3);
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
                pnlLevelTransition.SetActive(false);
                pnlDeath.SetActive(false);
                break;
            case 1:
                pnlStart.SetActive(false);
                pnlGame.SetActive(true);
                pnlLevelTransition.SetActive(false);
                pnlDeath.SetActive(false);
                break;
            case 2:
                pnlStart.SetActive(false);
                pnlGame.SetActive(false);
                pnlLevelTransition.SetActive(true);
                pnlDeath.SetActive(false);
                break;
            case 3:
                pnlStart.SetActive(false);
                pnlGame.SetActive(false);
                pnlLevelTransition.SetActive(false);
                pnlDeath.SetActive(true);
                break;

        }
    }

    private void spawnEnemy()
    {
        var currentEnemy = Instantiate(enemy, enemyRoot);
        currentEnemy.transform.position = generateInitialPosition();
        var enemyMovement = currentEnemy.GetComponent<EnemyMovement>();
        enemyMovement.setBasicInfo(player, powerUpRoot);
    }

    private Vector3 generateInitialPosition() {
        /* generate a random position */
        float randX;
        float randY;
        float limit = 0.125f;
        float quadrant = Random.value;
        if (quadrant < limit)
        {
            randX = Random.Range(-1.0f, -0.1f);
            randY = Random.Range(-1.0f, -0.1f);
        }
        else if (quadrant < (2 * limit))
        {
            randX = Random.Range(-1.0f, -0.1f);
            randY = Random.Range(0.0f, 1.0f);
        }
        else if (quadrant < (3 * limit))
        {
            randX = Random.Range(-1.0f, -0.1f);
            randY = Random.Range(1.1f, 2.0f);
        }
        else if (quadrant < (4 * limit))
        {
            randX = Random.Range(0.0f, 1.0f);
            randY = Random.Range(1.1f, 2.0f);
        }
        else if (quadrant < (5 * limit))
        {
            randX = Random.Range(1.1f, 2.0f);
            randY = Random.Range(1.1f, 2.0f);
        }
        else if (quadrant < (6 * limit))
        {
            randX = Random.Range(1.1f, 2.0f);
            randY = Random.Range(0.0f, 1.0f);
        }
        else if (quadrant < (7 * limit))
        {
            randX = Random.Range(1.1f, 2.0f);
            randY = Random.Range(-1.0f, -0.1f);
        }
        else
        {
            randX = Random.Range(0.0f, 1.0f);
            randY = Random.Range(-1.0f, -0.1f);
        }
        return Camera.main.ViewportToWorldPoint(new Vector3(randX, randY, Camera.main.nearClipPlane));
    }
}
