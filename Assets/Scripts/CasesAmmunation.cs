using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CasesAmmunation : MonoBehaviourPun
{
   
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    void OnCollisionEnter (Collision other)
    {
        
        
        if(other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if(player.GetComponentInChildren<Pistol>().Ammo <= 10)
            {
                player.GetComponentInChildren<Pistol>().RPCMoreAmmo();
                PhotonNetwork.Destroy(gameObject);
            }

        }
    }
}
