/*using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.InputSystem;
using NSubstitute;

public class MovementTests : InputTestFixture
{
    private PlayerMovement _movement;            
    //private InputTextFixture _input;
    
    [SetUp]
    public void Init()
    {        
        _movement = new GameObject().AddComponent<PlayerMovement>();   
        _movement.transform.position = new Vector3(0, 0, 0);      
        _movement.gameObject.AddComponent<BoxCollider2D>();
    }

    [UnityTest]
    public IEnumerator WhenStay_AndGoToRight_ThenPositionXShouldBeBigger()
    {        
        float positionX = _movement.gameObject.transform.position.x;

        _movement.MoveInDirectionX(1);                
        yield return new WaitForSeconds(0.3f);
        
        Assert.Greater(_movement.gameObject.transform.position.x, positionX);
    }

    [UnityTest]
    public IEnumerator WhenFly_AndTryToJumping_ThenPositionYShouldBeLess()
    {        
        float positionY = _movement.gameObject.transform.position.y;

        _movement.Jump();
        yield return new WaitForSeconds(0.2f);

        Assert.Less(_movement.gameObject.transform.position.y, positionY);
    }

    [UnityTest]
    public IEnumerator WhenStay_AndTryToJumping_ThenPositionYShouldBeGreater()
    {        
        float positionY = _movement.transform.position.y;      
        var obj = new GameObject();        
        obj.AddComponent<BoxCollider2D>().size = new Vector2(0.3f, 0.3f);
        obj.transform.position = _movement.transform.position + new Vector3(0, -0.5f, 0);
                
        _movement.Jump();
        yield return new WaitForSeconds(0.2f);
        
        Assert.Greater(_movement.gameObject.transform.position.y, positionY);
    }
}
*/