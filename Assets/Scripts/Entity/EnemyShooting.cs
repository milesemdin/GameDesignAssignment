using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : Shooting
{
    protected override void Update()
    {
        base.Update();
    }

    protected override void TakeInput()
    {
        Enemy.State state = GetComponent<Enemy>().state;

        if ((state == Enemy.State.Attack || state == Enemy.State.AttackAndMove) && shootReady)
        {
            Shoot();
        }
    }
}
