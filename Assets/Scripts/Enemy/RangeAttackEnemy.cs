using UnityEngine;

public class RangeAttackEnemy : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _speedBullet = 3f;
    [SerializeField] private Transform _targetEnemy;
    public void Shoot()
    {
        GameObject bullet = Instantiate(_bulletPrefab, transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(transform.right * _speedBullet, ForceMode2D.Impulse);
        bullet.GetComponent<BulletController>().SetDamage(1);
    }
}
