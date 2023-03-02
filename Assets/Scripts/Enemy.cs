using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float enemyHealth;
    [SerializeField]
    private float movingSpeed;
    
    private float killReward;
    private float damage;

    private GameObject targetTile;

    private void Start()
    {
        initializeEnemy();
    }

    private void initializeEnemy()
    {
        targetTile = mapGenerator.startTile;
    }
    
    private void takeDamage(float damage)
    {
        enemyHealth -= damage;
        if (enemyHealth <= 0)
        {
            die();
        }
    }
    
    private void die()
    {
        Destroy(gameObject);
    }

    private void moveEnemy()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetTile.transform.position, movingSpeed * Time.deltaTime);
    }

    private void checkPosition()
    {
        if (targetTile != null && targetTile != mapGenerator.endTile)
        {
            float distance = (transform.position - targetTile.transform.position).magnitude;
            if (distance < 0.1f)
            {
                int currIndex = mapGenerator.pathTiles.IndexOf(targetTile);
                targetTile = mapGenerator.pathTiles[currIndex + 1];
            }
        }
    }

    private void Update()
    {
        checkPosition();
        moveEnemy();
        
        takeDamage(0.1f);
    }
}