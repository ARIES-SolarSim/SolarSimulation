using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class dropdownmenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Arrow;
    public GameObject tide;

    
    public void allDrop(int index)
    {
        Debug.Log(index);
        PhotonView view = GetComponent<PhotonView>();
        view.RPC("DropdownSample", RpcTarget.MasterClient, index);
    }

    [PunRPC]
    public void DropdownSample(int index)
    {
        //image = GameObject.Find("Image");
        //tide = GameObject.Find("Tide");
        //United Kingdom
        if (!LobbyManager.userType)
        {
            if (index == 0)
            {
                tide.GetComponent<TideTempTwo>().enabled = false;
                tide.GetComponent<TideTempTwo>().enabled = true;
                Arrow.transform.localPosition = new Vector3(0.2f, 1, -0.02f);
                Arrow.transform.rotation = Quaternion.Euler(90f, 90f, 90f);
                //image.GetComponent<RectTransform>().anchoredPosition  = new Vector3(-420, -45, 0);
                tide.GetComponent<TideTempTwo>().myX = -5.401577f;
                tide.GetComponent<TideTempTwo>().myZ = -3.792488f;
                tide.GetComponent<TideTempTwo>().minY = 8 + (51.5072f / 90) * (12);

            }
            //Egypt
            /*
            if (index == 1)
            {
                tide.GetComponent<TideTempTwo>().enabled = false;
                tide.GetComponent<TideTempTwo>().enabled = true;
                image.GetComponent<RectTransform>().anchoredPosition = new Vector3(-380, -110, 0);
                tide.GetComponent<TideTempTwo>().myX = -0.730737f;
                tide.GetComponent<TideTempTwo>().myZ = -6.559422f;
                tide.GetComponent<TideTempTwo>().minY = 8 + (30.0444f / 90) * (12);

            }
            */
            //Japan
            if (index == 1)
            {
                Debug.Log("Japan");
                tide.GetComponent<TideTempTwo>().enabled = false;
                tide.GetComponent<TideTempTwo>().enabled = true;
                Arrow.transform.localPosition = new Vector3(-0.34f, 1, 0.24f);
                Arrow.transform.rotation = Quaternion.Euler(90f, 90f, 90f);
                //image.GetComponent<RectTransform>().anchoredPosition = new Vector3(-280, 40, 0);
                tide.GetComponent<TideTempTwo>().myX = 3.621747f;
                tide.GetComponent<TideTempTwo>().myZ = 5.517513f;
                tide.GetComponent<TideTempTwo>().minY = 8 + (35.6762f / 90) * (12);

            }
            //India
            if (index == 2)
            {
                Debug.Log("India");
                //Destroy(tide.GetComponent<TideTempTwo>());
                //tide.AddComponent<TideTempTwo>();
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                tide.GetComponent<TideTempTwo>().enabled = false;
                tide.GetComponent<TideTempTwo>().enabled = true;
                Arrow.transform.localPosition = new Vector3(0.06f, 1, 0.46f);
                Arrow.transform.rotation = Quaternion.Euler(90f, 90f, 90f);
                //image.GetComponent<RectTransform>().anchoredPosition = new Vector3(-290, -85, 0);
                tide.GetComponent<TideTempTwo>().myX = 3.646949f;
                tide.GetComponent<TideTempTwo>().myZ = -4.234735f;
                tide.GetComponent<TideTempTwo>().minY = 8 + (8.5241f / 90) * (12);

            }
            //North Pole
            if (index == 3)
            {
                Debug.Log("North Pole");
                //Destroy(tide.GetComponent<TideTempTwo>());
                //tide.AddComponent<TideTempTwo>();
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                tide.GetComponent<TideTempTwo>().enabled = false;
                tide.GetComponent<TideTempTwo>().enabled = true;
                Arrow.transform.localPosition = new Vector3(-0.05f, 1, -0.01f);
                Arrow.transform.rotation = Quaternion.Euler(90f, 90f, 90f);
                //image.GetComponent<RectTransform>().anchoredPosition = new Vector3(-390, 20, 0);
                tide.GetComponent<TideTempTwo>().myX = 4.203157f;
                tide.GetComponent<TideTempTwo>().myZ = -5.088562f;
                tide.GetComponent<TideTempTwo>().minY = 8 + (90.00f / 90) * (12);

            }
            //Singapore
            if (index == 4)
            {
                Debug.Log("Singapore");
                //Destroy(tide.GetComponent<TideTempTwo>());
                //tide.AddComponent<TideTempTwo>();
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                tide.GetComponent<TideTempTwo>().enabled = false;
                tide.GetComponent<TideTempTwo>().enabled = true;
                Arrow.transform.localPosition = new Vector3(-0.17f, 1, 0.46f);
                Arrow.transform.rotation = Quaternion.Euler(90f, 90f, 90f);
                //image.GetComponent<RectTransform>().anchoredPosition = new Vector3(-260, -40, 0);
                tide.GetComponent<TideTempTwo>().myX = 4.203157f;
                tide.GetComponent<TideTempTwo>().myZ = -5.088562f;
                tide.GetComponent<TideTempTwo>().minY = 8 + (1.3521f / 90) * (12);

            }
            //Miami
            if (index == 5)
            {
                Debug.Log("Miami");
                //Destroy(tide.GetComponent<TideTempTwo>());
                //tide.AddComponent<TideTempTwo>();
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                tide.GetComponent<TideTempTwo>().enabled = false;
                tide.GetComponent<TideTempTwo>().enabled = true;
                Arrow.transform.localPosition = new Vector3(0.023f, 1, -0.45f);
                Arrow.transform.rotation = Quaternion.Euler(90f, 90f, 90f);
                //image.GetComponent<RectTransform>().anchoredPosition = new Vector3(-260, -40, 0);
                tide.GetComponent<TideTempTwo>().myX = -4.582575f;
                tide.GetComponent<TideTempTwo>().myZ = 3.54362f;
                tide.GetComponent<TideTempTwo>().minY = 8 + (25.7617f / 90) * (12);

            }
            //Virginia Beach
            if (index == 6)
            {
                Debug.Log("Virginia Beach");
                //Destroy(tide.GetComponent<TideTempTwo>());
                //tide.AddComponent<TideTempTwo>();
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                tide.GetComponent<TideTempTwo>().enabled = false;
                tide.GetComponent<TideTempTwo>().enabled = true;
                Arrow.transform.localPosition = new Vector3(0.05f, 1, -0.38f);
                Arrow.transform.rotation = Quaternion.Euler(90f, 90f, 90f);
                //image.GetComponent<RectTransform>().anchoredPosition = new Vector3(-260, -40, 0);
                tide.GetComponent<TideTempTwo>().myX = -4.582575f;
                tide.GetComponent<TideTempTwo>().myZ = 3.54362f;
                tide.GetComponent<TideTempTwo>().minY = 8 + (36.8516f / 90) * (12);

            }
            //Barcelona
            if (index == 7)
            {
                Debug.Log("Barcelona");
                //Destroy(tide.GetComponent<TideTempTwo>());
                //tide.AddComponent<TideTempTwo>();
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                tide.GetComponent<TideTempTwo>().enabled = false;
                tide.GetComponent<TideTempTwo>().enabled = true;
                Arrow.transform.localPosition = new Vector3(0.3f, 1, -0.02f);
                Arrow.transform.rotation = Quaternion.Euler(90f, 90f, 90f);
                //image.GetComponent<RectTransform>().anchoredPosition = new Vector3(-260, -40, 0);
                tide.GetComponent<TideTempTwo>().myX = -2.871667f;
                tide.GetComponent<TideTempTwo>().myZ = -4.433514f;
                tide.GetComponent<TideTempTwo>().minY = 8 + (41.3874f / 90) * (12);

            }
            //Sydney
            if (index == 8)
            {
                Debug.Log("Sydney");
                //Destroy(tide.GetComponent<TideTempTwo>());
                //tide.AddComponent<TideTempTwo>();
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                tide.GetComponent<TideTempTwo>().enabled = false;
                tide.GetComponent<TideTempTwo>().enabled = true;
                Arrow.transform.localPosition = new Vector3(-0.4f, 1, 0.4f);
                Arrow.transform.rotation = Quaternion.Euler(90f, 180f, 90f);
                //image.GetComponent<RectTransform>().anchoredPosition = new Vector3(-260, -40, 0);
                tide.GetComponent<TideTempTwo>().myX = 6.555146f;
                tide.GetComponent<TideTempTwo>().myZ = 0.5730919f;
                tide.GetComponent<TideTempTwo>().minY = 8 + (33.8688f / 90) * (12);

            }
        }

    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
