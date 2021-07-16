using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BulletPistol : MonoBehaviourPun
{
    void Start()
    {
        Invoke("Destroy", 5);
    }

    void Update()
    {
        transform.Translate(new Vector3(5 * Time.deltaTime, 0, 0));
    }

    void Destroy()
    {
        PhotonNetwork.Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Statics"))
        {
            if(PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Destroy(gameObject);
            }
            
        }

        if(other.gameObject.CompareTag("Player"))
        {
            PlayerController inimigo = other.gameObject.GetComponent<PlayerController>();
            inimigo.life -= 5;
            if(PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
        

    }
}
