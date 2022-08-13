using UnityEngine;

public class AttackEnemy : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _speedBullet = 3f;
    [SerializeField] private Transform _targetEnemy;
    private void OnEnable()
    {
        GetComponentInParent<EnemyAnimation>().AttackEnemyAnimation();
        float rotationZ = Mathf.Atan2(_targetEnemy.position.y, _targetEnemy.position.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(rotationZ, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 1);
    }
    public void Shoot()
    {
        GameObject bullet = Instantiate(_bulletPrefab, transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(transform.right * _speedBullet, ForceMode2D.Impulse);
        bullet.GetComponent<BulletController>().SetDamage(1);
    }
}
