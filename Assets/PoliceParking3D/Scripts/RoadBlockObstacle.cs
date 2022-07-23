using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RoadBlockObstacle : Obstacles
{
    public override void Animate(Vector3 movementDir)
    {
        float duration = 0.1f;
        float strength = 0.1f;

        transform.DOShakePosition(duration, strength);
        transform.DOShakeRotation(duration, strength);
        transform.DOShakeScale(duration, strength);
    }
}
