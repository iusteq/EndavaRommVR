using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class LoginManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField playersName;
    public GameObject ConnectPanel;
    public GameObject ConnectingOptions;
    public GameObject LogInCanvas;

    #region Unity

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    #endregion

    #region Photon

    public override void OnConnected()
    {
        Debug.Log("OnConnected called! Server available!");
    }

    public override void OnConnectedToMaster()
    {

        Debug.Log("OnConnectedToMaster called!");
    }

    #endregion

    #region UI

    public void ConnectToPhotonServer()
    {
        if (!playersName)
        {
            PhotonNetwork.NickName = playersName.text;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public void OnConnectButton()
    {
        ConnectPanel.SetActive(false);
        ConnectingOptions.SetActive(false);
        LogInCanvas.SetActive(false);
        PhotonNetwork.LoadLevel("MeetingRoom");
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    #endregion
}
