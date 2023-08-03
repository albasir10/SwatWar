using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UI;


public class Unit : MonoBehaviour
{
    [Header("�������������� ���������")]
    public float health = 100f;
    public float speed = 3.5f;
    public float viewAngle = 60f;
    public float viewDistance = 10f;
    public float cooldownShoot = 0.5f;
    public Sprite Icon;
    public bool isEnemy = false;

    [Header("���������� ���������")]
    public Animator animator;
    public GameObject head;
    public GameObject body;
    protected StateMachine stateMachine;
    [HideInInspector]
    public NavMeshAgent agent;
    [HideInInspector]
    public AngleVision angleVision;
    [HideInInspector]
    public WeaponOption weaponOption;


    [Header("� �����������")]
    public List<Unit> enemyInRange;
    public Unit currentTarget;

    [Header("������")]
    public GameObject pointToShoot;

    [Header("������")]
    public UnityEvent injuryEvent;

    protected void Start()
    {
        injuryEvent = new();
        weaponOption = GetComponentInChildren<WeaponOption>();
        currentTarget = null;
        stateMachine = new(this, new StateIdleRifle());
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        angleVision = new(this, LayerMask.GetMask("Unit"), LayerMask.GetMask("Objects"));
    }

    protected virtual void Update()
    {
        stateMachine.currentState.UpdateState();
        angleVision.CheckLines();
        if (currentTarget == null)
        {
            enemyInRange = angleVision.FindVisibleTargets();
            SetEnemyIfNull();
        } 

        
    }

    public void SetEnemyIfNull()
    {
        if (enemyInRange.Count > 0)
        {
            currentTarget = enemyInRange[0];
            currentTarget.injuryEvent.AddListener(EnemyInjury);
        }
    }

    public void GetDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            Injury();
        }
    }

    public void Injury()
    {
        injuryEvent.Invoke();
        stateMachine.ChangeState( new StateInjury() );
    }

    public void EnemyInjury()
    {
        currentTarget = null;
    }

    public void MoveToPoint(Vector3 point)
    {
        stateMachine.ChangeState( new StateMove(point) );
    }

    public State GetStateUnit()
    {
        return stateMachine.currentState;
    }

}
