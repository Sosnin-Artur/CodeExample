using UnityEngine;

public static class Regidbody2DExtension
{

    public static void SetVelocityX(this Rigidbody2D rb, float value)
    {        
        rb.velocity = rb.velocity.SetX(value);
    }

    public static void SetVelocityY(this Rigidbody2D rb, float value)
    {
        rb.velocity = rb.velocity.SetY(value);
    }
}

