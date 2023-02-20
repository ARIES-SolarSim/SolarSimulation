using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SceneChangeTest : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Changing Scenes");
        if (other.gameObject.tag.Equals("GameController"))
        {
            transform.localPosition = new Vector3(transform.localPosition.x, 1, transform.localPosition.z);
        }
    }

    public void Update()
    {
        if (transform.localPosition.y == 1)
        {
            PhotonNetwork.LoadLevel(2);
        }
    }
}
