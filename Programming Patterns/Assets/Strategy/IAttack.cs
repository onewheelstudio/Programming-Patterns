using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack
{    
    void DoAttack();
}

public abstract class AttackWithParticles : MonoBehaviour
{
    public ParticleSystem particles;
    public abstract void DoAttack();
}

namespace AttackInterface
{
    public class FireAttack : IAttack
    {
        public void DoAttack()
        {
            Debug.Log("Doing Fire Attack");
        }
    }

    public class IceAttack : IAttack
    {
        public void DoAttack()
        {
            Debug.Log("Doing Ice Attack");
        }
    }

    public class PoisonAttack : IAttack
    {
        public void DoAttack()
        {
            Debug.Log("Doing Poison Attack");
        }
    }
}





