using UnityEngine;

public static class RegidbodyExtension
{
    public static void SetVelocityX(this Rigidbody rb, float value)
    {        
        rb.velocity = rb.velocity.SetX(value);
    }

    public static void SetVelocityY(this Rigidbody rb, float value)
    {
        rb.velocity = rb.velocity.SetY(value);
    }

    public static void SetVelocityZ(this Rigidbody rb, float value)
    {
        rb.velocity = rb.velocity.SetZ(value);
    }
}

