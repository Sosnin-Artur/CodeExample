using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TestTools;
using NSubstitute;

public class PlayerTests : InputTestFixture
{
    IPlayerModel _model;
    IPlayerView _view;
    BasePlayerPresenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _view = new GameObject().AddComponent<PlayerView>();
        _model = new PlayerModel();
        _presenter = new PlayerPresenter(_view, _model, new PlayerInputAction());        
    }    
}
