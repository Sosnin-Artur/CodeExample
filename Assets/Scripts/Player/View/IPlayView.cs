using System;

public interface IPlayerView
{
    event Action OnEnableEvent;
    event Action OnDisableEvent;

    void MoveInDirectionX(float direction);
    
    void StopMove();    

    void OnJump();    

    bool IsGrounded();   
}