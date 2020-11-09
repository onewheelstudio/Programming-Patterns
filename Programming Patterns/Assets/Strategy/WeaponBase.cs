using UnityEngine;
using System.Collections.Generic;


public class WeaponBase : MonoBehaviour
{
    public int damage = 10;
    public virtual void DoDamage()
    {
        PlayerStats.target.health -= damage;
    }
}

public class FireSword : WeaponBase
{
    public new int damage = 20;
    public override void DoDamage()
    {
        PlayerStats.target.health -= damage;
        //do fire damage stuff
    }
}

public class FireAxe : WeaponBase
{
    public new int damage = 35;
    public override void DoDamage()
    {
        PlayerStats.target.health -= damage;
        //do fire damage stuff
    }
}

public class FireDagger : WeaponBase
{
    public new int damage = 100;
    public override void DoDamage()
    {
        PlayerStats.target.health -= damage;
        //do fire damage stuff
    }
}

public interface IDoDamage
{
    void DoDamage(int damage);
}

public class FireDamage : IDoDamage
{
    public void DoDamage(int damage)
    {
        PlayerStats.target.health -= damage;
        //Do fire bits
    }
}

public class IceDamage : IDoDamage
{
    public void DoDamage(int damage)
    {
        PlayerStats.target.health -= damage;
        //Do Ice bits
    }
}

public class Weapon_Base : MonoBehaviour
{
    public int damage = 0;
    public IDoDamage damageType;

    public void TryDoAttack()
    {
        damageType?.DoDamage(damage);
    }

    public void SetDamageType(IDoDamage damageType)
    {
        this.damageType = damageType;
    }
}

public class Weapon__Base : MonoBehaviour
{
    public int damage = 0;
    public List<IDoDamage> damageTypes;

    public void TryDoAttack()
    {
        foreach(IDoDamage damage in damageTypes)
        {
            damage.DoDamage(this.damage);
        }
    }

    public void AddDamageType(IDoDamage damageType)
    {
        damageTypes.Add(damageType);
    }
}

public class Fire_Sword : Weapon_Base
{
    public Fire_Sword()
    {
        damage = 10;
        damageType = new FireDamage();
    }
}

public class Fire_Axe : Weapon_Base
{
    public Fire_Axe()
    {
        damage = 20;
        damageType = new FireDamage();
    }
}

public class Fire_Dagger : Weapon_Base
{
    public Fire_Dagger()
    {
        damage = 40;
        damageType = new FireDamage();
    }
}

public class Ice_Sword : Weapon_Base
{
    public Ice_Sword()
    {
        damage = 12;
        damageType = new IceDamage();
    }
}

public class Sword : Weapon_Base
{
    public Sword(int damage, IDoDamage damageType)
    {
        this.damage = damage;
        this.damageType = damageType;
    }
}


public static class PlayerStats
{
    public static int mana;
    public static int health;
    public static Target target;
}

public class Target
{
    public int health;
}

public abstract class WeaponDamage
{
    public ParticleSystem particleSystem;
}

public class WeaponDamageObject : ScriptableObject
{
    public ParticleSystem particleSystem;
}
