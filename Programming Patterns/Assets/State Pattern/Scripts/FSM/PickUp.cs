using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        //yes, this is a bad way to do it. Just needed it to work for the demo :/
        audioSource = GameObject.Find("Audio Source").GetComponent<AudioSource>();

        if(!NPCSearch.pickUps.Contains(this.gameObject))
            NPCSearch.pickUps.Add(this.gameObject);
        if (!NPCSearch_No_FSM.pickUps.Contains(this.gameObject))
            NPCSearch_No_FSM.pickUps.Add(this.gameObject);
        if (!NPCSearch_ClassBased.pickUps.Contains(this.gameObject))
            NPCSearch_ClassBased.pickUps.Add(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "NPC")
        {
            audioSource.Play();
            NPCSearch.pickUps.Remove(this.gameObject);
            NPCSearch_No_FSM.pickUps.Remove(this.gameObject);
            NPCSearch_ClassBased.pickUps.Remove(this.gameObject);

            this.gameObject.SetActive(false);
        }
    }
}
