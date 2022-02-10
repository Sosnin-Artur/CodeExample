public interface IPlayerView
{
    void MoveInDirectionX(float direction);
    
    void StopMove();    

    void OnJump();    

    bool IsGrounded();   
}