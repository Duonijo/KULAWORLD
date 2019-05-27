using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Bonus
{
    public class Transporters : Box
    {
        [FormerlySerializedAs("isTrigger")] public bool isCollide;
        public Transporters link;

        public void Start()
        {
            isCollide = false;
        }

        public override void OnCollisionEnter(Collision other)
        {
            isCollide = true;
        }
    }
}