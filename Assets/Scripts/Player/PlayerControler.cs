using UnityEngine;
[RequireComponent(typeof(BaseMovement))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerControler : MonoBehaviour
{
    private BaseMovement _movePlayer;
    private PlayerInput _directionPlayer;
    private void Start()
    {
        _movePlayer = GetComponent<BaseMovement>();
        _directionPlayer = GetComponent<PlayerInput>(); 
    }

    private void FixedUpdate()
    {
        _movePlayer.Move(_directionPlayer.directionalMovementPlayer);
        _movePlayer.Turn(_directionPlayer.mousePosition.x - transform.position.x);
    }
}
