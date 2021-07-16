using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class CanvasWorld : MonoBehaviourPunCallbacks
{

    public TextMeshProUGUI TextAmmo;
    public Slider          slider;

    private Pistol pistol;
    private PlayerController playerController;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        pistol = FindObjectOfType<Pistol>();
    }

    void Update()
    {
        TextAmmo.text = pistol.Ammo.ToString();
        slider.value = playerController.life;
    }

    [PunRPC]

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(playerController.life);
            stream.SendNext(slider.value);
        } else 
        {
            slider.value = (float) stream.ReceiveNext();
            playerController.life = (float) stream.ReceiveNext();
        }
    }
}
