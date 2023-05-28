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
    [SerializeField] float[] spawnTimeMS;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject player;
    [SerializeField] Transform enemyRoot;
    [SerializeField] Transform powerUpRoot;
    [SerializeField] public int powerUpGoal;
    [SerializeField] Transform playerRoot;
    [SerializeField] GameObject portal;
    [SerializeField] bool portalShown = false;
    [SerializeField] Transform portalSpawnRoot;

    float currentTime = 0;
    GameObject renderedPortal;

    Player playerData;

    // Start is called before the first frame update
    void Start()
    {
        playerData = player.GetComponent<Player>();
        changeState(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(gameState == 1)
        {
            var currentSpawnTimeMS = spawnTimeMS[playerData.currentWeapon];
            currentTime += Time.deltaTime * 1000;
            if (currentTime > currentSpawnTimeMS)
            {
                currentTime -= currentSpawnTimeMS;
                spawnEnemy();
            }
            if(playerData.health <= 0)
            {
                changeState(3);
                return;
            }
            if(playerData.powerUps >= powerUpGoal && !portalShown)
            {
                portalShown = true;
                spawnPortal();
            }
            if(portalShown && playerData.gotToPortal)
            {
                changeState(2);
            }
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
                playerData.interactive = false;
                break;
            case 1:
                pnlStart.SetActive(false);
                pnlGame.SetActive(true);
                pnlLevelTransition.SetActive(false);
                pnlDeath.SetActive(false);
                portalShown = false;
                restorePlayer();
                foreach (Transform child in enemyRoot)
                {
                    GameObject.Destroy(child.gameObject);
                }
                foreach (Transform child in powerUpRoot)
                {
                    GameObject.Destroy(child.gameObject);
                }
                if(renderedPortal != null)
                {
                    Destroy(renderedPortal);
                }
                break;
            case 2:
                pnlStart.SetActive(false);
                pnlGame.SetActive(false);
                pnlLevelTransition.SetActive(true);
                pnlDeath.SetActive(false);
                // all enemies are defeated at once!
                foreach (Transform child in enemyRoot)
                {
                    GameObject.Destroy(child.gameObject);
                }
                playerData.interactive = false;
                break;
            case 3:
                pnlStart.SetActive(false);
                pnlGame.SetActive(false);
                pnlLevelTransition.SetActive(false);
                pnlDeath.SetActive(true);
                playerData.interactive = false;
                break;

        }
    }

    private void restorePlayer() {
        var playerAnimator = player.GetComponent<Animator>();
        playerAnimator.speed = 1;

        var renderer = player.GetComponent<SpriteRenderer>();
        renderer.color = Color.white;

        playerData.interactive = true;
        playerData.health = 100;
        playerData.powerUps = 0;
        playerData.currentWeapon = 0;
        player.transform.position = playerRoot.position;
    }

    private void spawnPortal() {
        var randomChildIndex = Random.Range(0, portalSpawnRoot.childCount-1);
        Instantiate(portal, portalSpawnRoot.GetChild(randomChildIndex));
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
