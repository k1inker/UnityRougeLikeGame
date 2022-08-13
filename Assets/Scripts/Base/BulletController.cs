using UnityEngine;
public class BulletController : MonoBehaviour
{
    private Animator _destroyBullet;
    private float damage;
    private void Awake()
    {
        _destroyBullet =  GetComponent<Animator>();
    }
    public void SetDamage(float damage)
    {
        this.damage = damage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Damageable")
        {
            collision.gameObject.GetComponent<HealthControl>().TakeDamageAndCheckDeath(damage);
        }
        if (collision.gameObject.tag != "Object")
        {
            //_destroyBullet.SetTrigger("destroyBullet");
            Destroy(this.gameObject);
        }
    }
}
