using UnityEngine;
using System.Collections;

public class WeaponButton : MonoBehaviour
{
    public AttackWithParticles attack;

    public void TryAttack()
    {
        attack?.DoAttack();
    }

    public void SetAttackType(AttackWithParticles _attack)
    {
        this.attack = _attack;
    }
}
