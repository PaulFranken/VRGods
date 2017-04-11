using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public int mana;
    public Spell spell;

	// Use this for initialization
	void Start () {
        spell = new Meteor();
        spell.isChannelling = true;
	}
	
	// Update is called once per frame
	void Update () {
        spell.ChannelSpell();
	}
}
