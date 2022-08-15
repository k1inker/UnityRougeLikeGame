using Pathfinding;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]private Transform _targetEnemy;

    private Vector2 _searchingPosition;
    private GameObject _player;
    private AIPath _aiPath;
    void Start()
    {
        _searchingPosition = transform.position;
        _player = GameObject.Find("Player");
        _aiPath = GetComponent<AIPath>();
        _aiPath.maxSpeed = GetComponent<StatsControl>().velocity;
    }
    private void Update()
    {
        _targetEnemy.transform.position = _player.transform.position;
        if (_targetEnemy.hasChanged)
        {
            _aiPath.SearchPath();
        }
    }
    // Generation random normalized direction
    private static Vector2 GetRandomDir()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
    public Vector2 GetRoamingPosition()
    {
        return _searchingPosition + GetRandomDir() * Random.Range(3f, 3f);
    }
    public bool IsLookPlayer()
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
    public bool IsStuck()
    {
        if (_aiPath.hasPath && _aiPath.velocity.magnitude < 0.1)
            return true;
        return false;
    }
    
}
