using UnityEngine;
using Pathfinding;
[RequireComponent(typeof(BaseMovement))]
[RequireComponent(typeof(AIPath))]
public class EnemyControler : MonoBehaviour
{
    public Transform targetEnemy;
    [HideInInspector]public GameObject playerObj;

    private AIPath _logicPath;
    private BaseMovement _moveEnemy;

    private Vector2 _roamingPosition;
    private void Awake()
    {
        _moveEnemy = GetComponent<BaseMovement>();
        _logicPath = GetComponent<AIPath>();
        _roamingPosition = transform.position;
        playerObj = GameObject.Find("Player");
        _logicPath.maxSpeed = GetComponent<StatsControl>().velocity;
    }
    private void FixedUpdate()
    {   
        _moveEnemy.Move(_logicPath.velocity.normalized);
        _moveEnemy.Turn(_logicPath.velocity.x);

        if (targetEnemy.hasChanged)
        {
            _logicPath.SearchPath();
        }
    }
    private static Vector2 GetRandomDir()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
    public void GetRoamingPosition(float rangeRoaming)
    {
        targetEnemy.position = _roamingPosition + GetRandomDir() * Random.Range(rangeRoaming, rangeRoaming);
    }
    public bool IsLookPlayer()
    {
        RaycastHit2D[] rayToPlayer = Physics2D.LinecastAll(transform.position, playerObj.transform.position);
        Debug.DrawLine(transform.position, playerObj.transform.position, Color.black);
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
        if (_logicPath.hasPath && _logicPath.velocity.magnitude < 0.1)
            return true;
        return false;
    }
}
