using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameObject PlayerPrefab;

    void OnJoinedRoom()
    {
        StartGame();
    }

    void StartGame()
    {
        PhotonNetwork.Instantiate(PlayerPrefab.name, transform.position, Quaternion.identity, 0);
    }

    void OnGUI()
    {
        if (PhotonNetwork.room == null) return;

        if (GUILayout.Button("Leave Room"))
        {
            PhotonNetwork.LeaveRoom();
        }
    }

    void OnDisconnectedFromPhoton()
    {
        Debug.LogWarning("OnDisconnectedFromPhoton");
    }
}
