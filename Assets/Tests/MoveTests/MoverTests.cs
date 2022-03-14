using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;


public class MovementTests
{    
    private Mover _mover;

    [SetUp]
    public void Init()
    {        
        var gameObject = GameObject.Instantiate(Resources.Load(PathsInResources.Player)) as GameObject; 
        
        _mover = gameObject.GetComponent<Mover>();           
        _mover.transform.position = new Vector3(0, 0, 0);      
        _mover.gameObject.AddComponent<BoxCollider2D>();      
    }

    [UnityTest]
    public IEnumerator WhenStanding_AndGoToRight_ThenPositionXShouldBeBigger()
    {                
        float positionX = _mover.gameObject.transform.position.x;

        _mover.MoveInDirectionX(1);                
        yield return new WaitForSeconds(0.3f);
        
        Assert.Greater(_mover.gameObject.transform.position.x, positionX);
    }

    [UnityTest]
    public IEnumerator WhenGoing_AndStand_ThenPositionXTheSame()
    {        
        _mover.MoveInDirectionX(1);
        float positionX = _mover.transform.position.x;
        
        _mover.StopMove();
        yield return new WaitForSeconds(0.1f);

        Assert.True(Mathf.Approximately(_mover.transform.position.x, positionX));
    }

    [UnityTest]
    public IEnumerator WhenStanding_AndJump_ThenPositionYShouldBeBigger()
    {        
        var ground = new GameObject().AddComponent<BoxCollider2D>();
        ground.size = Vector2.one;
        ground.transform.position = _mover.transform.position + new Vector3(0, -2f);
        yield return new WaitForSeconds(0.1f);
        float positionY = _mover.gameObject.transform.position.y;

        _mover.OnJump();
        yield return new WaitForSeconds(0.1f);

        Assert.Greater(_mover.transform.position.y, positionY);
    }

    [UnityTest]
    public IEnumerator WhenFalling_AndJump_ThenPositionYShouldBeLess()
    {
        float positionY = _mover.gameObject.transform.position.y;                

        _mover.OnJump();
        yield return new WaitForSeconds(0.3f);

        Assert.Less(_mover.transform.position.y, positionY);
    }
}
