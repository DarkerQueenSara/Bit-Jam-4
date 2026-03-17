using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{

    public TextMeshProUGUI scoreText;

    public List<GameObject> pips;

    private Player player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    private void Update()
    {
        scoreText.text = player.playerScore.ToString();

        for (int i = 0; i < pips.Count; i++)
        {
            pips[i].SetActive(i + 1 <= player.playerHp);
        }
    }
}
