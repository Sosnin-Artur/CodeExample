using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;


public class SwordTests
{    
    private SwordItem _sword;

    [SetUp]
    public void Init()
    {        
        var grandParent = new GameObject().GetComponent<Transform>();
        var parent = new GameObject().GetComponent<Transform>();
        parent.SetParent(grandParent);
        var gameObject = GameObject.Instantiate(Resources.Load(PathsInResources.Sword), parent) as GameObject; 
        
        _sword = gameObject.GetComponent<SwordItem>();                   
    }

    [UnityTest]
    public IEnumerator WhenUseSword_ThenPositionXShouldBeBigger()
    {                
        float positionX = _sword.gameObject.transform.position.x;

        _sword.Use();                
        yield return new WaitForSeconds(0.1f);
        
        Assert.Greater(_sword.gameObject.transform.position.x, positionX);
    }    
}
