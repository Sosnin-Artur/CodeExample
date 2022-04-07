using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;


public class EnemyStateMachineTests
{       
    private EnemyStateMachine _statemachine;

    private IEnemyView _view;
    private IEnemyModel _model;

    [SetUp]
    public void Init()
    {
        var target = new GameObject();
        var reactiveTarget = new ReactiveProperty<Transform>(target.transform);

        _view = Substitute.For<IEnemyView>();
        _model = Substitute.For<IEnemyModel>();
        _view.Target.Returns(reactiveTarget);

        _statemachine = new EnemyStateMachine(_view);
        _statemachine.InitStates(_view);
    }    

    [Test]
    public void WhenEnemyChangeStateToAttack_ThenCurrentStateShouldBeAttack()
    {
        var state = new EnemyAttackState(_view);
        
        _statemachine.ChangeState(state);

        Assert.AreEqual(_statemachine.CurrentState, state);
    }
}
