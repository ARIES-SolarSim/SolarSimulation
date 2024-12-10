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
    public GameObject tide1;
    public GameObject tide2;

    public bool isOnPc;

    private void Start()
    {
        PCDropdownSample(0);
    }

    public void allDrop(int index)
    {
        if(isOnPc)
        {
            PCDropdownSample(index);
        }
        else
        {
            Debug.Log(index);
            PhotonView view = GetComponent<PhotonView>();
            view.RPC("DropdownSample", RpcTarget.All, index);
        }
    }

    public void PCDropdownSample(int index)
    {
        //image = GameObject.Find("Image");
        //tide = GameObject.Find("Tide");
        //United Kingdom
        if (!LobbyManager.userType)
        {
            if (index == 0)
            {

                Arrow.transform.localPosition = new Vector3(0.16f, 0.391f, -0.06f);
                Arrow.transform.localRotation = Quaternion.Euler(-73.359f, 113.345f, -234.088f);

                tide1.GetComponent<TideTempTwo>().enabled = false;
                tide1.GetComponent<TideTempTwo>().enabled = true;

                tide2.GetComponent<TideTempTwo>().enabled = false;
                tide2.GetComponent<TideTempTwo>().enabled = true;


                //image.GetComponent<RectTransform>().anchoredPosition  = new Vector3(-420, -45, 0);

                tide1.GetComponent<TideTempTwo>().minY = (8 + (51.5072f / 90) * (12)) + 285;
                tide2.GetComponent<TideTempTwo>().minY = 8 + (51.5072f / 90) * (12) + 285;

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

                Arrow.transform.localPosition = new Vector3(-0.241f, 0.206f, 0.382f);
                Arrow.transform.localRotation = Quaternion.Euler(-19.648f, -28.411f, -100f);

                Debug.Log("Japan");
                tide1.GetComponent<TideTempTwo>().enabled = false;
                tide1.GetComponent<TideTempTwo>().enabled = true;

                tide2.GetComponent<TideTempTwo>().enabled = false;
                tide2.GetComponent<TideTempTwo>().enabled = true;




                //image.GetComponent<RectTransform>().anchoredPosition = new Vector3(-280, 40, 0);

                tide1.GetComponent<TideTempTwo>().minY = 8 + (35.6762f / 90) * (12) + 285;
                tide2.GetComponent<TideTempTwo>().minY = 8 + (35.6762f / 90) * (12) + 285;

            }
            //India
            if (index == 2)
            {
                Arrow.transform.localPosition = new Vector3(0.07f, 0.168f, 0.413f);
                Arrow.transform.localRotation = Quaternion.Euler(-15.226f, 11.595f, -104.036f);

                Debug.Log("India");
                //Destroy(tide.GetComponent<TideTempTwo>());
                //tide.AddComponent<TideTempTwo>();
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                tide1.GetComponent<TideTempTwo>().enabled = false;
                tide1.GetComponent<TideTempTwo>().enabled = true;

                tide2.GetComponent<TideTempTwo>().enabled = false;
                tide2.GetComponent<TideTempTwo>().enabled = true;


                //image.GetComponent<RectTransform>().anchoredPosition = new Vector3(-290, -85, 0);

                tide1.GetComponent<TideTempTwo>().minY = 8 + (8.5241f / 90) * (12) + 285;
                tide2.GetComponent<TideTempTwo>().minY = 8 + (8.5241f / 90) * (12) + 285;

            }
            //North Pole
            if (index == 3)
            {
                Arrow.transform.localPosition = new Vector3(-0.011f, 0.4347f, -0.0308f);
                Arrow.transform.localRotation = Quaternion.Euler(-84.74f, 129.386f, -211.002f);

                Debug.Log("North Pole");
                //Destroy(tide.GetComponent<TideTempTwo>());
                //tide.AddComponent<TideTempTwo>();
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                tide1.GetComponent<TideTempTwo>().enabled = false;
                tide1.GetComponent<TideTempTwo>().enabled = true;

                tide2.GetComponent<TideTempTwo>().enabled = false;
                tide2.GetComponent<TideTempTwo>().enabled = true;


                //image.GetComponent<RectTransform>().anchoredPosition = new Vector3(-390, 20, 0);

                tide1.GetComponent<TideTempTwo>().minY = 8 + (90.00f / 90) * (12) + 285;
                tide2.GetComponent<TideTempTwo>().minY = 8 + (90.00f / 90) * (12) + 285;

            }
            //Singapore
            if (index == 4)
            {
                Arrow.transform.localPosition = new Vector3(-0.107f, 0.0148f, 0.4466f);
                Arrow.transform.localRotation = Quaternion.Euler(-6.191f, -20.791f, -43f);

                Debug.Log("Singapore");
                //Destroy(tide.GetComponent<TideTempTwo>());
                //tide.AddComponent<TideTempTwo>();
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                tide1.GetComponent<TideTempTwo>().enabled = false;
                tide1.GetComponent<TideTempTwo>().enabled = true;

                tide2.GetComponent<TideTempTwo>().enabled = false;
                tide2.GetComponent<TideTempTwo>().enabled = true;


                //image.GetComponent<RectTransform>().anchoredPosition = new Vector3(-260, -40, 0);

                tide1.GetComponent<TideTempTwo>().minY = 8 + (1.3521f / 90) * (12) + 285;
                tide2.GetComponent<TideTempTwo>().minY = 8 + (1.3521f / 90) * (12) + 285;

            }
            //Miami
            if (index == 5)
            {
                Arrow.transform.localPosition = new Vector3(0.071f, 0.2135f, -0.3951f);
                Arrow.transform.localRotation = Quaternion.Euler(-25.272f, -190.961f, 44.095f);

                Debug.Log("Miami");
                //Destroy(tide.GetComponent<TideTempTwo>());
                //tide.AddComponent<TideTempTwo>();
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                tide1.GetComponent<TideTempTwo>().enabled = false;
                tide1.GetComponent<TideTempTwo>().enabled = true;

                tide2.GetComponent<TideTempTwo>().enabled = false;
                tide2.GetComponent<TideTempTwo>().enabled = true;


                //image.GetComponent<RectTransform>().anchoredPosition = new Vector3(-260, -40, 0);

                tide1.GetComponent<TideTempTwo>().minY = 8 + (25.7617f / 90) * (12) + 285;
                tide2.GetComponent<TideTempTwo>().minY = 8 + (25.7617f / 90) * (12) + 285;

            }
            //Virginia Beach
            if (index == 6)
            {
                Arrow.transform.localPosition = new Vector3(0.0811f, 0.3114f, -0.3459f);
                Arrow.transform.localRotation = Quaternion.Euler(-27.68f, -193.621f, 45.282f);

                Debug.Log("Virginia Beach");
                //Destroy(tide.GetComponent<TideTempTwo>());
                //tide.AddComponent<TideTempTwo>();
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                tide1.GetComponent<TideTempTwo>().enabled = false;
                tide1.GetComponent<TideTempTwo>().enabled = true;

                tide2.GetComponent<TideTempTwo>().enabled = false;
                tide2.GetComponent<TideTempTwo>().enabled = true;


                //image.GetComponent<RectTransform>().anchoredPosition = new Vector3(-260, -40, 0);

                tide1.GetComponent<TideTempTwo>().minY = 8 + (36.8516f / 90) * (12) + 285;
                tide2.GetComponent<TideTempTwo>().minY = 8 + (36.8516f / 90) * (12) + 285;

            }
            //Barcelona
            if (index == 7)
            {
                Arrow.transform.localPosition = new Vector3(0.309f, 0.34f, -0.003f);
                Arrow.transform.localRotation = Quaternion.Euler(-20.582f, -283.212f, 114.958f);

                Debug.Log("Barcelona");
                //Destroy(tide.GetComponent<TideTempTwo>());
                //tide.AddComponent<TideTempTwo>();
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                tide1.GetComponent<TideTempTwo>().enabled = false;
                tide1.GetComponent<TideTempTwo>().enabled = true;

                tide2.GetComponent<TideTempTwo>().enabled = false;
                tide2.GetComponent<TideTempTwo>().enabled = true;


                //image.GetComponent<RectTransform>().anchoredPosition = new Vector3(-260, -40, 0);

                tide1.GetComponent<TideTempTwo>().minY = 8 + (41.3874f / 90) * (12) + 285;
                tide2.GetComponent<TideTempTwo>().minY = 8 + (41.3874f / 90) * (12) + 285;

            }
            //Sydney
            if (index == 8)
            {
                Arrow.transform.localPosition = new Vector3(-0.3129f, -0.2695f, 0.1339f);
                Arrow.transform.localRotation = Quaternion.Euler(19.113f, -419.504f, 113.551f);

                Debug.Log("Sydney");
                //Destroy(tide.GetComponent<TideTempTwo>());
                //tide.AddComponent<TideTempTwo>();
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                tide1.GetComponent<TideTempTwo>().enabled = false;
                tide1.GetComponent<TideTempTwo>().enabled = true;

                tide2.GetComponent<TideTempTwo>().enabled = false;
                tide2.GetComponent<TideTempTwo>().enabled = true;


                //image.GetComponent<RectTransform>().anchoredPosition = new Vector3(-260, -40, 0);

                tide1.GetComponent<TideTempTwo>().minY = 8 + (33.8688f / 90) * (12) + 285;
                tide2.GetComponent<TideTempTwo>().minY = 8 + (33.8688f / 90) * (12) + 285;


            }
            Debug.Log(Arrow.transform.position);
            Debug.Log(Arrow.transform.rotation);
        }

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

                Arrow.transform.localPosition = new Vector3(0.16f, 0.391f, -0.06f);
                Arrow.transform.localRotation = Quaternion.Euler(-73.359f, 113.345f, -234.088f);

                tide1.GetComponent<TideTempTwo>().enabled = false;
                tide1.GetComponent<TideTempTwo>().enabled = true;

                tide2.GetComponent<TideTempTwo>().enabled = false;
                tide2.GetComponent<TideTempTwo>().enabled = true;

                
                //image.GetComponent<RectTransform>().anchoredPosition  = new Vector3(-420, -45, 0);
                
                tide1.GetComponent<TideTempTwo>().minY = (8 + (51.5072f / 90) * (12)) + 285;
                tide2.GetComponent<TideTempTwo>().minY = 8 + (51.5072f / 90) * (12) + 285;

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

                Arrow.transform.localPosition = new Vector3(-0.241f, 0.206f, 0.382f);
                Arrow.transform.localRotation = Quaternion.Euler(-19.648f, -28.411f, -100f);

                Debug.Log("Japan");
                tide1.GetComponent<TideTempTwo>().enabled = false;
                tide1.GetComponent<TideTempTwo>().enabled = true;

                tide2.GetComponent<TideTempTwo>().enabled = false;
                tide2.GetComponent<TideTempTwo>().enabled = true;


               
                
                //image.GetComponent<RectTransform>().anchoredPosition = new Vector3(-280, 40, 0);
               
                tide1.GetComponent<TideTempTwo>().minY = 8 + (35.6762f / 90) * (12) + 285;
                tide2.GetComponent<TideTempTwo>().minY = 8 + (35.6762f / 90) * (12) + 285;

            }
            //India
            if (index == 2)
            {
                Arrow.transform.localPosition = new Vector3(0.07f, 0.168f, 0.413f);
                Arrow.transform.localRotation = Quaternion.Euler(-15.226f, 11.595f, -104.036f);

                Debug.Log("India");
                //Destroy(tide.GetComponent<TideTempTwo>());
                //tide.AddComponent<TideTempTwo>();
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                tide1.GetComponent<TideTempTwo>().enabled = false;
                tide1.GetComponent<TideTempTwo>().enabled = true;

                tide2.GetComponent<TideTempTwo>().enabled = false;
                tide2.GetComponent<TideTempTwo>().enabled = true;

                
                //image.GetComponent<RectTransform>().anchoredPosition = new Vector3(-290, -85, 0);
                
                tide1.GetComponent<TideTempTwo>().minY = 8 + (8.5241f / 90) * (12) + 285;
                tide2.GetComponent<TideTempTwo>().minY = 8 + (8.5241f / 90) * (12) + 285;

            }
            //North Pole
            if (index == 3)
            {
                Arrow.transform.localPosition = new Vector3(-0.011f, 0.4347f, -0.0308f);
                Arrow.transform.localRotation = Quaternion.Euler(-84.74f, 129.386f, -211.002f);

                Debug.Log("North Pole");
                //Destroy(tide.GetComponent<TideTempTwo>());
                //tide.AddComponent<TideTempTwo>();
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                tide1.GetComponent<TideTempTwo>().enabled = false;
                tide1.GetComponent<TideTempTwo>().enabled = true;

                tide2.GetComponent<TideTempTwo>().enabled = false;
                tide2.GetComponent<TideTempTwo>().enabled = true;

                
                //image.GetComponent<RectTransform>().anchoredPosition = new Vector3(-390, 20, 0);
                
                tide1.GetComponent<TideTempTwo>().minY = 8 + (90.00f / 90) * (12) + 285;
                tide2.GetComponent<TideTempTwo>().minY = 8 + (90.00f / 90) * (12) + 285;

            }
            //Singapore
            if (index == 4)
            {
                Arrow.transform.localPosition = new Vector3(-0.107f, 0.0148f, 0.4466f);
                Arrow.transform.localRotation = Quaternion.Euler(-6.191f, -20.791f, -43f);

                Debug.Log("Singapore");
                //Destroy(tide.GetComponent<TideTempTwo>());
                //tide.AddComponent<TideTempTwo>();
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                tide1.GetComponent<TideTempTwo>().enabled = false;
                tide1.GetComponent<TideTempTwo>().enabled = true;

                tide2.GetComponent<TideTempTwo>().enabled = false;
                tide2.GetComponent<TideTempTwo>().enabled = true;

                
                //image.GetComponent<RectTransform>().anchoredPosition = new Vector3(-260, -40, 0);
                
                tide1.GetComponent<TideTempTwo>().minY = 8 + (1.3521f / 90) * (12) + 285;
                tide2.GetComponent<TideTempTwo>().minY = 8 + (1.3521f / 90) * (12) + 285;

            }
            //Miami
            if (index == 5)
            {
                Arrow.transform.localPosition = new Vector3(0.071f, 0.2135f, -0.3951f);
                Arrow.transform.localRotation = Quaternion.Euler(-25.272f, -190.961f, 44.095f);

                Debug.Log("Miami");
                //Destroy(tide.GetComponent<TideTempTwo>());
                //tide.AddComponent<TideTempTwo>();
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                tide1.GetComponent<TideTempTwo>().enabled = false;
                tide1.GetComponent<TideTempTwo>().enabled = true;

                tide2.GetComponent<TideTempTwo>().enabled = false;
                tide2.GetComponent<TideTempTwo>().enabled = true;

                
                //image.GetComponent<RectTransform>().anchoredPosition = new Vector3(-260, -40, 0);
              
                tide1.GetComponent<TideTempTwo>().minY = 8 + (25.7617f / 90) * (12) + 285;
                tide2.GetComponent<TideTempTwo>().minY = 8 + (25.7617f / 90) * (12) + 285;

            }
            //Virginia Beach
            if (index == 6)
            {
                Arrow.transform.localPosition = new Vector3(0.0811f, 0.3114f, -0.3459f);
                Arrow.transform.localRotation = Quaternion.Euler(-27.68f, -193.621f, 45.282f);

                Debug.Log("Virginia Beach");
                //Destroy(tide.GetComponent<TideTempTwo>());
                //tide.AddComponent<TideTempTwo>();
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                tide1.GetComponent<TideTempTwo>().enabled = false;
                tide1.GetComponent<TideTempTwo>().enabled = true;

                tide2.GetComponent<TideTempTwo>().enabled = false;
                tide2.GetComponent<TideTempTwo>().enabled = true;

                
                //image.GetComponent<RectTransform>().anchoredPosition = new Vector3(-260, -40, 0);
               
                tide1.GetComponent<TideTempTwo>().minY = 8 + (36.8516f / 90) * (12) + 285;
                tide2.GetComponent<TideTempTwo>().minY = 8 + (36.8516f / 90) * (12) + 285;

            }
            //Barcelona
            if (index == 7)
            {
                Arrow.transform.localPosition = new Vector3(0.309f, 0.34f, -0.003f);
                Arrow.transform.localRotation = Quaternion.Euler(-20.582f, -283.212f, 114.958f);

                Debug.Log("Barcelona");
                //Destroy(tide.GetComponent<TideTempTwo>());
                //tide.AddComponent<TideTempTwo>();
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                tide1.GetComponent<TideTempTwo>().enabled = false;
                tide1.GetComponent<TideTempTwo>().enabled = true;

                tide2.GetComponent<TideTempTwo>().enabled = false;
                tide2.GetComponent<TideTempTwo>().enabled = true;

                
                //image.GetComponent<RectTransform>().anchoredPosition = new Vector3(-260, -40, 0);
               
                tide1.GetComponent<TideTempTwo>().minY = 8 + (41.3874f / 90) * (12) + 285;
                tide2.GetComponent<TideTempTwo>().minY = 8 + (41.3874f / 90) * (12) + 285;

            }
            //Sydney
            if (index == 8)
            {
                Arrow.transform.localPosition = new Vector3(-0.3129f, -0.2695f, 0.1339f);
                Arrow.transform.localRotation = Quaternion.Euler(19.113f, -419.504f, 113.551f);

                Debug.Log("Sydney");
                //Destroy(tide.GetComponent<TideTempTwo>());
                //tide.AddComponent<TideTempTwo>();
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                tide1.GetComponent<TideTempTwo>().enabled = false;
                tide1.GetComponent<TideTempTwo>().enabled = true;

                tide2.GetComponent<TideTempTwo>().enabled = false;
                tide2.GetComponent<TideTempTwo>().enabled = true;

                
                //image.GetComponent<RectTransform>().anchoredPosition = new Vector3(-260, -40, 0);
           
                tide1.GetComponent<TideTempTwo>().minY = 8 + (33.8688f / 90) * (12) + 285;
                tide2.GetComponent<TideTempTwo>().minY = 8 + (33.8688f / 90) * (12) + 285;


            }
            Debug.Log(Arrow.transform.position);
            Debug.Log(Arrow.transform.rotation);
        }

    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
