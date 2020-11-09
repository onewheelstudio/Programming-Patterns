using UnityEngine;
using System.Collections;

public class SpellButtons : MonoBehaviour
{
    //public void AssignFire()
    //{
    //    FindObjectOfType<DoSpell>().AssignSpell(new FireAttack());
    //}

    //public void AssignIce()
    //{
    //    FindObjectOfType<DoSpell>().AssignSpell(new IceAttack());
    //}

    //public void AssignPoison()
    //{
    //    FindObjectOfType<DoSpell>().AssignSpell(new PoisonAttack());
    //}

    public void AssignAttack()
    {
        AttackWithParticles attack = this.GetComponent<AttackWithParticles>();
        if(attack != null)
            FindObjectOfType<DoSpell>().AssignSpell(attack);
    }
}
