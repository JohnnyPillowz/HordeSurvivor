using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    public static event Action OnInteractionStarted;
    public static event Action OnInteractionStopped;

    private bool canMove = true;
        
    private Vector2 moveInput;
    private Player player;

    private void Start()
    {
        player = Player.Instance;
    }


    public void Interact(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnInteractionStarted?.Invoke();
        }
        else if (context.canceled)
        {
            OnInteractionStopped?.Invoke();
        }
    }
    
    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();  
    }
    private void Update()
    {
        if (!canMove) { return; }
        
        Vector3 movementVector = new Vector3(moveInput.x, 0, moveInput.y);
        Vector3 movement = movementVector * player.stats.moveSpeed;
        transform.Translate(movement * Time.deltaTime);
    }

    public void ChangeCanMove(bool canMove)
    {
        this.canMove = canMove;
    }
}
