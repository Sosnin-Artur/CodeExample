using MVP;
using UnityEngine;

public interface IEnemyModel : IModel
{
    ReactiveProperty<Transform> Target { get; set; }
}