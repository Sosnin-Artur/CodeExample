public interface IMover
{
    void MoveInDirectionX(float direction);

    void StopMove();

    void OnJump();

    bool IsGrounded();
}