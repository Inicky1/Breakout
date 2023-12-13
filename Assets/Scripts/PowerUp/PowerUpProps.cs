using System;
using UnityEngine;

namespace PowerUp
{
    public class PowerUpProps
    {
        public PowerUpType Type { get; private set; }
        public GameObject GmObject { get; set; }
        public Boolean IsEnable { get; set; }

        public PowerUpProps(PowerUpType type, GameObject gmObject)
        {
            Type = type;
            GmObject = gmObject;
            IsEnable = PlayerPrefs.GetInt($"is{Type}") == 1;//if it's one it means player purchased it 
        }
    }
}