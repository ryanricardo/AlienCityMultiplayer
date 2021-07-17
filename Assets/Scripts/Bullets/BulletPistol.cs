using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BulletPistol : MonoBehaviourPun
{
    CanvasWorld canvasWorld;
    void Start()
    {
        canvasWorld = FindObjectOfType<CanvasWorld>();
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
        if(!PhotonNetwork.IsMasterClient){return;}

        if(other.CompareTag("Statics"))
        {
            PhotonNetwork.Destroy(this.gameObject);
        }

        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().RPCLostLife();
            PhotonNetwork.Destroy(this.gameObject);
            
        }
    }





}
