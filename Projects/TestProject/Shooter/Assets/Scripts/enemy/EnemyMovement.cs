using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    public int damage = 25;
    public int damageEnemy;
    public float AttackSpeed = 1.2f;
    public bool isAttacking;
    public bool inRange;

    public GameObject player;
    public Transform playerPos;
    NavMeshAgent agent;

    Vector3 previousPos;
    public Animator anim;

    void OnEnable()
    {
        player = GameObject.FindWithTag("Player");
        playerPos = player.GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        damageEnemy = damage * 2;

        if (!isAttacking)
        {
            agent.isStopped = false;
            agent.SetDestination(playerPos.transform.position);
        }
    }
}

