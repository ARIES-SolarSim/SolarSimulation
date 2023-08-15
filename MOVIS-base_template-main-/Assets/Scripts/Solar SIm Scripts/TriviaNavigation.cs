using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TriviaNavigation : MonoBehaviour
{
    private Vector3 Origin;
    private Vector3 Destination;
    private GameObject[] planets;
    public Button ScatterButton;
    public int planetId;
    // Start is called before the first frame update
    void Start()
    {
        planets = GameObject.FindGameObjectsWithTag("planet");

        if(!LobbyManager.userType || PhotonNetwork.NickName == "9")
        {
            ScatterButton.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Scatter()
    {
        
        Debug.Log("IN SCATTER");
            if (planetId == 0)
            {
                Origin = transform.position;
                Destination = new Vector3(0, 0, 0);

                if (PhotonNetwork.IsMasterClient)
                {
                    PhotonView view = GetComponent<PhotonView>();
                    view.RPC("moveObject", RpcTarget.All, Origin, Destination);
                }

            }
            else if(planetId == 1)
            {
                Origin = transform.position;
                Destination = new Vector3(0.7072551f, 0.05784007f, 0.05060094f);

                if (PhotonNetwork.IsMasterClient)
                {
                    PhotonView view = GetComponent<PhotonView>();
                    view.RPC("moveObject", RpcTarget.All, Origin, Destination);
                }
            }
            else if (planetId == 2)
            {
                Origin = transform.position;
                Destination = new Vector3(0.0752404f, 0.001417816f, -1.011935f);

                if (PhotonNetwork.IsMasterClient)
                {
                    PhotonView view = GetComponent<PhotonView>();
                    view.RPC("moveObject", RpcTarget.All, Origin, Destination);
                }
            }
            else if (planetId == 3)
            {
                Origin = transform.position;
                Destination = new Vector3(-0.24499f, 0.009951003f, 1.403397f);

                if (PhotonNetwork.IsMasterClient)
                {
                    PhotonView view = GetComponent<PhotonView>();
                    view.RPC("moveObject", RpcTarget.All, Origin, Destination);
                }
            }
            else if (planetId == 4)
            {
                Origin = transform.position;
                Destination = new Vector3(-1.816775f, 0.01664971f, 0.09624183f);

                if (PhotonNetwork.IsMasterClient)
                {
                    PhotonView view = GetComponent<PhotonView>();
                    view.RPC("moveObject", RpcTarget.All, Origin, Destination);
                }

            }
            else if (planetId == 5)
            {
                Origin = transform.position;
                Destination = new Vector3(1.61201f, -0.02882987f, -1.972728f);

                if (PhotonNetwork.IsMasterClient)
                {
                    PhotonView view = GetComponent<PhotonView>();
                    view.RPC("moveObject", RpcTarget.All, Origin, Destination);
                }

            }
            else if (planetId == 6)
            {
                Origin = transform.position;
                Destination = new Vector3(-1.83732f, -0.005745246f, 2.609222f);

                if (PhotonNetwork.IsMasterClient)
                {
                    PhotonView view = GetComponent<PhotonView>();
                    view.RPC("moveObject", RpcTarget.All, Origin, Destination);
                }
            }
            else if (planetId == 7)
            {
                Origin = transform.position;
                Destination = new Vector3(2.713179f, -0.07243448f, 2.615502f);

                if (PhotonNetwork.IsMasterClient)
                {
                    PhotonView view = GetComponent<PhotonView>();
                    view.RPC("moveObject", RpcTarget.All, Origin, Destination);
                }
            }
            else
            {
                Origin = transform.position;
                Destination = new Vector3(4.267019f, -0.0390997f, 0.003761596f);

                if (PhotonNetwork.IsMasterClient)
                {
                    PhotonView view = GetComponent<PhotonView>();
                    view.RPC("moveObject", RpcTarget.All, Origin, Destination);
                }
            }
        
    }

    
    [PunRPC]
    public IEnumerator moveObject(Vector3 Origin, Vector3 Destination)
    {
        float totalMovementTime = 5f;

        float currentMovementTime = 0f;

        while(Vector3.Distance(transform.localPosition, Destination) > 0){
            currentMovementTime += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(Origin, Destination, currentMovementTime / totalMovementTime);
            yield return null;
        }
    }
}
