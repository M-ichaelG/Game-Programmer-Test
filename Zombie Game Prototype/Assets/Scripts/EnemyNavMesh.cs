using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMesh : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Transform playerPositionTransform;
    private int HP;
    [SerializeField]
    private int enemyVariant;       //1 = normal, 2 = clown, 3 = boss
    private int[] normalHP = new int[4] { 100, 125, 150, 175 };
    private int clownHP = 250;
    private int bossHP = 2500;

    private void Awake()
    {
        if (enemyVariant != 3)
            navMeshAgent = GetComponent<NavMeshAgent>();
        playerPositionTransform = GameManager.instance.trPlayer;
        if (enemyVariant == 1)
        {
            int rand = Random.Range(0, normalHP.Length);
            HP = normalHP[rand];
        } else if (enemyVariant == 2)
        {
            HP = clownHP;
        } else if (enemyVariant == 3)
        {
            HP = bossHP;
        }
    }

    private void Update()
    {
        if (enemyVariant != 3)
        {
            if (navMeshAgent.enabled)
                navMeshAgent.destination = playerPositionTransform.position;
        }
    }

    public void SetDamage (int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            GameManager.instance.countEnemy -= 1;
            Destroy(gameObject);
        }
    }
}
