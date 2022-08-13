using UnityEngine;
using System.Collections;

public class RangeWeaponController : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float _fireRate = 2f;
    [SerializeField] private float _speedBullet = 5f;
    [SerializeField] private float _damage = 4f;
   
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _firePoint;

    private bool _isCanShoot = true;
   
    private StatsControl _rotateSpeed;
    private Vector2 _directionMouseFromPlayer;
    private Transform _parentTransform;
    private BaseMovement _facingRight;
    
    private void Awake()
    {
        _rotateSpeed = GetComponentInParent<StatsControl>();
        _parentTransform = GetComponentInParent<Transform>();
        _facingRight = GetComponentInParent<BaseMovement>();
    }  
    private void FixedUpdate()
    {
        RotateWeapon();
    }

    private void RotateWeapon()
    {
        _directionMouseFromPlayer = Camera.main.ScreenToWorldPoint(Input.mousePosition) - _parentTransform.position;
        _directionMouseFromPlayer.Normalize();

        float rotationZ = Mathf.Atan2(_directionMouseFromPlayer.y, _directionMouseFromPlayer.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(rotationZ, Vector3.forward);
        if (_facingRight.facingRight)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _rotateSpeed.rotateSpeed);
        }
        else
        {
            rotation = Quaternion.Euler(-180, 0, -rotationZ);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _rotateSpeed.rotateSpeed);
        }
        
    }

    public void FireBullet()
    {
        if (!_isCanShoot)
            return;
        GameObject bullet = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(_firePoint.right * _speedBullet, ForceMode2D.Impulse);
        bullet.GetComponent<BulletController>().SetDamage(_damage);
        StartCoroutine(TurnFire());
    }
    
    IEnumerator TurnFire()
    {
        _isCanShoot = false;
        yield return new WaitForSeconds(_fireRate);
        _isCanShoot = true;
    }    
}
