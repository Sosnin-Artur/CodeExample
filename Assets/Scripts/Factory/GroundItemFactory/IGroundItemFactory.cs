using UnityEngine;

public interface IGroundItemFactory<T> : IFactory<T> where T : BaseGroundItem
{
}
