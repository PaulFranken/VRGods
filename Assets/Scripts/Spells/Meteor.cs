using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : Spell {

    public Vector3 origin;
    public Vector3 destination;

    public override void InitiateSpell()
    {
        isChannelling = true;
    }

    public override void StartSpell()
    {
        throw new NotImplementedException();
    }

    public override void ChannelSpell()
    {
        throw new NotImplementedException();
    }

    public override void EndSpell()
    {
        throw new NotImplementedException();
    }


}
