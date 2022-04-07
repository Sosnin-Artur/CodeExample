using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCrossing : MonoBehaviour
{
    [SerializeField]
    private LayerMask _interactionLayer;
    [SerializeField]
    private LayerChechker _layerChechker;
    [SerializeField]
    private LevelWays _way;
    [SerializeField]
    private BoxCollider2D _collider;

    private enum LevelWays
    {
        NextLevel,
        PreviousLevel
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_layerChechker.IsInLayerMask(_interactionLayer, collision.gameObject))
        {
            if (_way == LevelWays.NextLevel)
            {
                LevelSystem.GoToNextLevel();
            }
            else if (_way == LevelWays.PreviousLevel)
            {
                LevelSystem.GoToPreviousLevel();
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (_way == LevelWays.NextLevel)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(transform.position, _collider.size);
        }
        else if (_way == LevelWays.PreviousLevel)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(transform.position, _collider.size);
        }
    }
}
