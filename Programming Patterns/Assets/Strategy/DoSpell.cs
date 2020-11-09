using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoSpell : MonoBehaviour
{
    [SerializeField]
    private string spellName;
    private IAttack spellAttack;
    private AttackWithParticles spellWithParticles;

    public void CastSpell()
    {
        spellAttack?.DoAttack();
        spellWithParticles?.DoAttack();
    }

    public void AssignSpell(IAttack spell)
    {
        spellAttack = spell;
        spellName = spell.GetType().ToString();
    }

    public void AssignSpell(AttackWithParticles spell)
    {
        spellWithParticles = spell;
        spellName = spell.GetType().ToString();
    }
}
