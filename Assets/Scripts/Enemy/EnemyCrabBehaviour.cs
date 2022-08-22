using Pathfinding;
using UnityEngine;

public class EnemyCrabBehaviour : MonoBehaviour
{
    private Animator _anim;
    private EnemyControler _enemyPath;
    private AIPath _aiPath;
    void Start()
    {
        _enemyPath = GetComponent<EnemyControler>();
        _aiPath = GetComponent<AIPath>();
        _anim = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        _enemyPath.targetEnemy.position = _enemyPath.playerObj.transform.position;
        if (_aiPath.reachedEndOfPath)
        {
            SetMeleeAttackAnimation();
        }
    }
    private void SetMeleeAttackAnimation()
    {
        _anim.SetTrigger("attack");
    }
}
