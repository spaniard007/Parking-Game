using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ConeObstacle : Obstacles
{
    public override void Animate(Vector3 movementDir)
    {
        var duration = 0.5f;
        transform.DOShakePosition(
            duration:0.1f,
            strength:0.1f,
            vibrato:5).SetDelay(duration*0.5f);
    }
}
