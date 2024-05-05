using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    public TextMeshProUGUI massage;
    public GameObject massageUI;

    public GameObject player;
    public GameObject key;

    public static Vector3 playerPos;
    public static bool isDoorLock = true;

    void GameInit()
    {
        isDoorLock = true;
        key.SetActive(true);
        massageUI.SetActive(false);
        player.transform.position =
            player.GetComponent<PlayerController>().startPos;
    }

    private void Awake()
    {
        gm = this;
    }

    private void Start()
    {
        GameInit();
    }

    private void Update()
    {
        playerPos = player.transform.position;
    }

    public void GameClear() => GameEnd(true, "Clear!");
    public void GameOver() => GameEnd(true, "GameOver");
    public void Restart() => GameEnd(false, "");

    public void GameEnd(bool isGameEnd, string msg)
    {
        if (isGameEnd)
        {
            Time.timeScale = 0f;
            massageUI.SetActive(true);
            massage.text = msg.ToString();
        }
        else
        {
            Time.timeScale = 1f;
            GameInit();
        }
    }
}
