using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Resources;
using UnityEngine;

public static class ResourceTypes {

    public static Resource GetByType(string type)
    {
        switch (type)
        {
            case "Stone":
                return new StoneResource();
            case "Wood":
                return new WoodResource();
            
        }
        return null;
    }
	
}
