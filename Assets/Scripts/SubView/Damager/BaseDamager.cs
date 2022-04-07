using UnityEngine;

public abstract class BaseDamager : MonoBehaviour
{
    public abstract int Damage { get; set; }
    public abstract float KnockbackForce { get; set; }

}