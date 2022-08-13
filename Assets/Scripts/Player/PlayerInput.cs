using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // for playerMoovement
    internal Vector2 directionalMovementPlayer;
    internal Vector2 mousePosition;

    private InteractObject _interactObject;
    private RangeWeaponController _rangeWeaponController;

    private void Awake()
    {
        _interactObject = GetComponent<InteractObject>();
        _rangeWeaponController = GetComponentInChildren<RangeWeaponController>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        directionalMovementPlayer = new Vector2(horizontal, vertical).normalized;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.E))
        {
            _interactObject.StartClickAnim();
        }
        if (Input.GetButton("Fire1"))
        {
            _rangeWeaponController.FireBullet();
        }
    }
}
