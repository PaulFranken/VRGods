using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour {

    public bool isChannelling;

    public abstract void InitiateSpell();

    public abstract void StartSpell();

    public abstract void ChannelSpell();

    public abstract void EndSpell();

  
}
