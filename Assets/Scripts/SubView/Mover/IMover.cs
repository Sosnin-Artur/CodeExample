using System;

public interface IMover : IDisposable
{
    void MoveInDirectionX(float direction);

    void StopMove();

    void OnJump();

    bool IsGrounded();
}