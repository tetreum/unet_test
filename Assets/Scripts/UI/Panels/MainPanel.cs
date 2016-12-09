using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainPanel : NetworkBehaviour
{
    public void host() {
        NetworkManager.singleton.matchMaker.CreateMatch("room-123", 10, true, "", "", "", 0, 0, OnMatchCreate);
    }

    public void connect() {
        NetworkManager.singleton.matchMaker.ListMatches(0, 1, "room-123", true, 0, 0, OnMatchList);
    }

    public void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        if (!success)
        {
            Debug.LogError("Room creation failed");
            return;
        }

        StartCoroutine(loadRoom(extendedInfo, matchInfo, true));
    }

    private IEnumerator loadRoom(string extendedInfo, MatchInfo matchInfo, bool isNew = false)
    {
        var async = SceneManager.LoadSceneAsync("Level1");

        yield return async;

        if (isNew) {
            NetworkManager.singleton.OnMatchCreate(true, extendedInfo, matchInfo);
        } else {
            NetworkManager.singleton.OnMatchJoined(true, extendedInfo, matchInfo);
        }

        Menu.Instance.hidePanel("Main");
    }

    private void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList) {
        NetworkManager.singleton.matchMaker.JoinMatch(matchList[0].networkId, "", "", "", 0, 0, OnMatchJoined);
    }

    public void OnMatchJoined(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        if (!success)
        {
            Debug.LogError("Room creation failed");
            return;
        }

        StartCoroutine(loadRoom(extendedInfo, matchInfo));
    }
}