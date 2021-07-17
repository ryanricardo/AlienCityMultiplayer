using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Pistol : MonoBehaviourPun, IPunObservable
{
    private PlayerController player;
    public int        Ammo;
    public GameObject PrefabBullet;
    public Transform  TransformExitBullet;

    void Start()
    {
        if(!photonView.IsMine){return;}
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
            RPCSubmitAmmo();
        }
    }

    public void RPCSubmitAmmo()
    {
        photonView.RPC("SubmitAmmo", RpcTarget.All);
    }
    
    public void RPCMoreAmmo()
    {
        photonView.RPC("MoreAmmo", RpcTarget.All);
    }

    [PunRPC]

    public void MoreAmmo()
    {
        Ammo = 10;
    }

    [PunRPC]
    public void SubmitAmmo()
    {
        Ammo -= 1;
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(Ammo);
        }else 
        {
            Ammo = (int) stream.ReceiveNext();
        }
    }
}
