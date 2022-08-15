using UnityEngine;

public class HealthControl : MonoBehaviour
{
    private StatsControl _exhealth;
    private float _maxHealth;
    private BaseAnimation _anim;
    private void Awake()
    {
        _exhealth = GetComponent<StatsControl>();
        _anim = GetComponentInChildren<BaseAnimation>();
        _maxHealth = _exhealth.health;
    }
    public void TakeDamageAndCheckDeath(float damage)
    {
        _exhealth.health -= damage;
        _anim.HurtAnimation();
        if(IsDead())
        {
            Die();
        }

    }
    public void Heal(float health)
    {
        if (_exhealth.health + 1 > _maxHealth)
            return;
        _exhealth.health += health;
    }
    public bool IsDead()
    {
        if(_exhealth.health <= 0 )
        {
            return true;
        }
        return false;
    }
    private void Die()
    {
        _anim.DeathAnimation();
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Behaviour[] components = GetComponents<Behaviour>();
        foreach (Behaviour c in components)
        {
            c.enabled = false;
        }
        Destroy(gameObject, 20f);
    }
}
