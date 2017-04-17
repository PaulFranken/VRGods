using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Resources;
using UnityEngine;

public class ResourceMap : MonoBehaviour
{
    public enum Resources
    {
        STONE,
        WOOD,
        GOLD,
        IRON
    }


    [System.Serializable]
    public class ResourceEntry
    {
        public Resource resource;
        public int amount;
        public int current;
    }

    public ResourceEntry[] resources;
}