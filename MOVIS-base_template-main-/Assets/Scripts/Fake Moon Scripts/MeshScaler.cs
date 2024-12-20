using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class MeshScaler : MonoBehaviour
{
    private Vector3[] scales = {new Vector3(0.000003476238745f, 0.000003476238745f, 0.000003476238745f),
        //Vector3.one };
        new Vector3(0.05079997257f, 0.05079997257f, 0.05079997257f)};
    public static int view = 0;

    public float trailTime;
    private bool isChanging = false;
    public int steps = 0;
    public TrailRenderer tr;

    private bool hasClearedTrail = false;
    public bool isOnPC;

    // Start is called before the first frame update
    void Start()
    {
        view = 0;
    }

    // Update is called once per frame
    void Update()
    {
        PhotonView view2 = GetComponent<PhotonView>();
        if (isChanging)
        {
            if (!LobbyManager.userType)
            {
                PCClearTrail();
            }
            int targetView = (view == 1) ? 0 : 1;
            transform.localScale = Vector3.Lerp(scales[view], scales[targetView], (steps * 1f) / (UniverseController.changeDuration * 1f)) * UniverseController.planetScale;
            steps++;
           // Debug.Log("Changing");
        }

        else
        {
            if(isOnPC)
            {
                PCStartTrail();
            }
            else
            {
                if (FindObjectOfType<ViewTypeObserver>().transform.localPosition.z == 1 && !hasClearedTrail)
                {

                    if (!LobbyManager.userType)
                    {
                        view2.RPC("ClearTrail", RpcTarget.All);
                        hasClearedTrail = true;
                    }

                }
                else if (FindObjectOfType<ViewTypeObserver>().transform.localPosition.z == 0 && hasClearedTrail)
                {
                    if (!LobbyManager.userType)
                    {
                        view2.RPC("StartTrail", RpcTarget.All);
                        hasClearedTrail = false;
                    }

                }
            }
        }
        
        //transform.localScale = Vector3.one * scales[view - 1].x * UniverseController.planetScale;
    }

    [PunRPC]
    public void ClearTrail()
    {
        tr.time = 0;
    }

    [PunRPC]
    public void StartTrail()
    {
        tr.time = trailTime;
    }

    public void PCClearTrail()
    {
        tr.time = 0;
    }

    public void PCStartTrail()
    {
        tr.time = trailTime;
    }

    public void changing()
    {
        isChanging = true;
       // Debug.Log("Set true");
    }

    public void doneChanging()
    {
        isChanging = false;
        //Debug.Log("Set false");
    }
}
