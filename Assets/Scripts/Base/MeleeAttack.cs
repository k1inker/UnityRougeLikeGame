using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private float _damageCount;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _radiusAttack;
    [SerializeField] private LayerMask _layerDamageable;

    public void EntityDamage()
    {
        Collider2D[] hitEntity = Physics2D.OverlapCircleAll(transform.position, _radiusAttack, _layerDamageable);
        foreach(Collider2D entity in hitEntity)
            entity.GetComponent<HealthControl>().TakeDamageAndCheckDeath(_damageCount);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _radiusAttack);
    }
}
