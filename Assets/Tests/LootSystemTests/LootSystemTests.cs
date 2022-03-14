using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;


public class LootSystemTests
{    
    private EnemyView _enemyView;

    [SetUp]
    public void Init()
    {   
        var gameObject = GameObject.Instantiate(Resources.Load(PathsInResources.Enemy)) as GameObject;
        _enemyView = gameObject.GetComponent<EnemyView>();
    }

    [UnityTest]
    public IEnumerator WhenEnemyDie_ThenItemShouldAppear()
    {                        

        _enemyView.Die();                
        yield return new WaitForSeconds(0.1f);
        
        Assert.IsNotNull(GameObject.FindObjectOfType<BaseGroundItem>());
    }    
}
