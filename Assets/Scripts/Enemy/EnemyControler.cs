using UnityEngine;
using Pathfinding;
[RequireComponent(typeof(BaseMovement))]
[RequireComponent(typeof(AIPath))]
public class EnemyControler : MonoBehaviour
{
    private BaseMovement _moveEnemy;
    private AIPath _movementAI;
    private void Awake()
    {
        _moveEnemy = GetComponent<BaseMovement>();
        _movementAI = GetComponent<AIPath>();
    }
    private void FixedUpdate()
    {
        _moveEnemy.Move(_movementAI.velocity.normalized);
        _moveEnemy.Turn(_movementAI.velocity.x); ;
    }
}
