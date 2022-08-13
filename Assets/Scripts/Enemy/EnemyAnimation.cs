using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private GameObject _gameObjAttack;
    private Animator _anim;
    private bool _isAtacking = false;
    void Awake()
    {
        _anim = GetComponent<Animator>();
    }
    public void AttackEnemyAnimation()
    {
        _isAtacking = !_isAtacking;
        _anim.SetBool("attack", _isAtacking);
        if(_isAtacking == false)
        {
            _gameObjAttack.SetActive(false);
        }
    }
    private void DeathEnemy()
    {
        Destroy(GetComponentInParent<Rigidbody2D>().gameObject);
    }
    private void AttackEnemy()
    {
        GetComponentInChildren<AttackEnemy>().Shoot();
    }
}
