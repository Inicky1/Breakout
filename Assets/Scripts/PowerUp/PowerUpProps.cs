using System;
using UnityEngine;

namespace PowerUp
{
    public class PowerUpProps
    {
        public PowerUpType Type { get; private set; }
        public GameObject GmObject { get; set; }
        public Boolean IsEnable { get; set; }

        public PowerUpProps(PowerUpType type, GameObject gmObject, bool isEnable = true)
        {
            Type = type;
            GmObject = gmObject;
            IsEnable = isEnable;
        }
    }
}