using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public Resources resource;
        public int amount;
        public int current;
    }

    public ResourceEntry[] resources;
}