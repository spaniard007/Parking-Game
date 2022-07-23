using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Match;

public class Obstacles : MonoBehaviour
{
   public virtual void Animate(Vector3 movementDir)
   {
      Debug.Log("Show Obstacle Anim");
   }
}
