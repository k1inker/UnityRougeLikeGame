using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(Animator))]
public class BaseAnimation : MonoBehaviour
{
    private Animator _anim;
    private Rigidbody2D _rb;

    void Start()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponentInParent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        _anim.SetFloat("velocity", _rb.velocity.magnitude);
    }
    public void DeathAnimation()
    {
        _anim.SetTrigger("death");
    }
}
