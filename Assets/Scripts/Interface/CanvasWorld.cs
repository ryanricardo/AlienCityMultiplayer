using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class CanvasWorld : MonoBehaviourPunCallbacks, IPunObservable

{

    public TextMeshProUGUI TextAmmo;
    public Slider          slider;

    public Pistol pistol;
    public PlayerController playerController;



    void Start()
    {
        
    }

    void Update()
    {
        
        TextAmmo.text = pistol.GetComponent<Pistol>().Ammo.ToString();
        slider.value = playerController.GetComponent<PlayerController>().life;
    }

    [PunRPC]
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(pistol.Ammo);     
            stream.SendNext(playerController.life);
            stream.SendNext(slider.value);     
            stream.SendNext(TextAmmo.text);   
        }else
        {
            pistol.Ammo = (int) stream.ReceiveNext();
            playerController.life = (float) stream.ReceiveNext();
        }
    }


}
