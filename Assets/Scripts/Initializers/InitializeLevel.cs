using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeLevel : MonoBehaviour
{
    [SerializeField] 
    private PlayerView _playerView;

    private void Awake()
    {
        var playerModel = new PlayerModel();                

        var playerPresenter = new PlayerPresenter(_playerView, playerModel);
    }
}
