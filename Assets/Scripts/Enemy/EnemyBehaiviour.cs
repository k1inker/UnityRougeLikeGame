using Pathfinding;
using UnityEngine;

public class EnemyBehaiviour : MonoBehaviour
{
    [SerializeField] private Transform _targetEnemy;
    [SerializeField] private GameObject _enemyAttack;
    [SerializeField,Range(1, 9)] private int _chanceAttack = 4;

    private Vector2 _searchingPosition;
    private GameObject _player;
    private AIPath _aiPath;
    void Awake()
    {
        _searchingPosition = transform.position;
        _player = GameObject.Find("Player");
        _targetEnemy.position = GetRoamingPosition();
        _aiPath = GetComponent<AIPath>();
        _aiPath.maxSpeed = GetComponent<StatsControl>().velocity;
    }
    // Generation random normalized direction
    private static Vector2 GetRandomDir()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
    private Vector2 GetRoamingPosition()
    {
        return _searchingPosition + GetRandomDir() * Random.Range(3f, 3f);
    }

    void Update()
    {
        if (IsLookPlayer())
        {
            FollowPlayer();
        }
        else
        {
            _searchingPosition = _aiPath.steeringTarget;
            SearchingTarget();
        }
        if (IsStuck())
        {
            _targetEnemy.position = GetRoamingPosition();
            _aiPath.canSearch = true;
        }
    }
    private bool IsLookPlayer()
    {
        RaycastHit2D[] rayToPlayer = Physics2D.LinecastAll(transform.position, _player.transform.position);
        Debug.DrawLine(transform.position, _player.transform.position, Color.black);
        foreach (RaycastHit2D hit in rayToPlayer)
        {
            GameObject other = hit.collider.gameObject;
            if (other.CompareTag("Wall"))
                return false;
        }
        return true;
    }
    private void FollowPlayer()
    {
        _aiPath.canSearch = true;
        _targetEnemy.position = _player.transform.position;
    }
    private void SearchingTarget()
    {
        _aiPath.canSearch = false;
        if (!_aiPath.hasPath)
        {
            _aiPath.canSearch = true;
        }
        if (_aiPath.reachedEndOfPath)
        {
            _targetEnemy.position = GetRoamingPosition();
            _aiPath.canSearch = true;
        }
    }
    private bool IsStuck()
    {
        if (_aiPath.hasPath && _aiPath.velocity.magnitude < 0.1)
            return true;
        return false;
    }
    
}
