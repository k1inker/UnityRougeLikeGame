using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BaseAnimation : MonoBehaviour
{
    private Animator _anim;
    private Rigidbody2D _rb;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponentInParent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _anim.SetFloat("velocity", _rb.velocity.magnitude);
    }

    public void HurtAnimation()
    {
        _anim.SetTrigger("hurt");
    }
    public void DeathAnimation()
    {
        _anim.SetBool("isDeath",true);
    }
}
