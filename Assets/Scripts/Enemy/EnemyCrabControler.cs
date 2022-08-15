using UnityEngine;
using Pathfinding;
[RequireComponent(typeof(BaseMovement))]
[RequireComponent(typeof(AIPath))]
public class EnemyCrabControler : MonoBehaviour
{
    [SerializeField] private float _radiusAttack;
    [SerializeField] private LayerMask _layerPlayer;

    private BaseMovement _moveEnemy;
    private AIPath _pathAI;
    private Animator _anim;
    private void Awake()
    {
        _moveEnemy = GetComponent<BaseMovement>();
        _pathAI = GetComponent<AIPath>();
        _anim = GetComponentInChildren<Animator>();
    }
    private void FixedUpdate()
    {
        _moveEnemy.Move(_pathAI.velocity.normalized);
        _moveEnemy.Turn(_pathAI.velocity.x);
        if(_pathAI.reachedEndOfPath)
        {
            SetMeleeAttackAnimation();
        }
    }
    private void SetMeleeAttackAnimation()
    {
        _anim.SetTrigger("attack");
    }
}
