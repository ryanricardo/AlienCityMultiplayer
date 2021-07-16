using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Pistol : MonoBehaviourPun
{
    private PlayerController player;

    public int        Ammo;
    public GameObject PrefabBullet;
    public Transform  TransformExitBullet;

    void Start()
    {
        Ammo = 10;
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if(!photonView.IsMine){return;}
        Actions();
    }

    void Actions()
    {
       
        if(Input.GetMouseButtonDown(0) && Ammo > 0)
        {
            PhotonNetwork.Instantiate(PrefabBullet.name, TransformExitBullet.transform.position, 
            TransformExitBullet.transform.rotation);
            Ammo -= 1;
        }
    }
}
