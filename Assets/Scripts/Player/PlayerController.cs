using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class PlayerController : MonoBehaviourPun, IPunObservable
{
    
    public float life;
    public bool  dead;

    private float AxisHorizontal;
    private float AxisVertical;
    private Animator animator;

    void Start()
    {
        
        life  = 100;
        animator = GetComponent<Animator>();

        if(!photonView.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            return;
        }
    }

   
    void Update()
    {   
        if(!photonView.IsMine){return;}


        UpdateMovimentation();
        UpdateAnimations();
        UpdateActions();
        
    }

    void UpdateMovimentation()
    {
        AxisHorizontal = Input.GetAxis("Horizontal");
        AxisVertical = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(0, 0, AxisVertical * 15 * Time.deltaTime));
        transform.Rotate(new Vector3(0, AxisHorizontal * 5, 0));
    }



    void UpdateAnimations()
    {
        if(AxisHorizontal != 0 || AxisVertical != 0)
        {
            animator.SetBool("Correndo", true);
        }else
        {
            animator.SetBool("Correndo", false);
        }
    }

    void UpdateActions()
    {
        if(life <= 0)
        {
            PhotonNetwork.LeaveRoom();
            UnityEngine.SceneManagement.SceneManager.LoadScene("Loose");
        }
    }

    public void RPCLostLife()
    {
        photonView.RPC("LostLife", RpcTarget.All);
    }

    [PunRPC]
    public void LostLife()
    {
        life -= 5;
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(life);
        }else 
        {
            life = (float) stream.ReceiveNext();
        }
    }
}
