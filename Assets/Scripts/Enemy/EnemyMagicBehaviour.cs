using UnityEngine;
using Pathfinding;
public class EnemyMagicBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _enemyAttack;

    private Animator _anim;
    private AIPath _pathEnemy;
    private EnemyControler _enemyControls;
    private bool _isAttacked = false;
    private void Awake()
    {
        _enemyControls = GetComponent<EnemyControler>();
        _anim = GetComponentInChildren<Animator>();
        _pathEnemy = GetComponent<AIPath>();
    }
    private void Update()
    {
        if(_enemyControls.IsLookPlayer())
        {
            if (!_isAttacked)
            {
                _enemyAttack.SetActive(true);
                _anim.SetTrigger("attack");
                _isAttacked = true;
            }
            else
            {
                RoamingMovement();
            }
        }
        else if(!_enemyControls.IsLookPlayer() && _isAttacked)
        {
            RoamingMovement();
        }
        else
        {
            _enemyControls.targetEnemy.position = _enemyControls.playerObj.transform.position;
        }
    }
    private void RoamingMovement()
    {
        if (_pathEnemy.reachedEndOfPath)
            _enemyControls.GetRoamingPosition(4f);
    }
}
