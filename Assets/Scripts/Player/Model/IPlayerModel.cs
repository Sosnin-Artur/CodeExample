using MVP;
using UnityEngine;

public interface IPlayerModel : IModel
{
    ReactiveProperty<Vector3> Position { get; set; }    
}