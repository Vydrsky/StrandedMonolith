using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerTests
{
    
    [Test]
    public void TakeDamage_PositiveValue_PositiveHealth()
    {
        Player player = new Player();
        player.TakeDamage(20);
        Assert.IsTrue(player.HealthPoints == player.MaxHealth-20);
    }

    
}
