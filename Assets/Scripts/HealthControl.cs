using UnityEngine;

public class HealthControl : MonoBehaviour
{
    private StatsControl _exhealth;
    private BaseAnimation _anim;
    private void Awake()
    {
        _exhealth = GetComponent<StatsControl>();
        _anim = GetComponentInChildren<BaseAnimation>();
    }
    public void TakeDamageAndCheckDeath(float damage)
    {
        _exhealth.health -= damage;
        if(IsDead())
        {
            _anim.DeathAnimation();
        }

    }
    public void Heal(float health)
    {
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
}
