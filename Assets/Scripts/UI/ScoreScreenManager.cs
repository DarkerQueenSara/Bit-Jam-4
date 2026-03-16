using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;


public class ScoreScreenManager : MonoBehaviour
{
    public List<TextMeshProUGUI> leaderboardBrackets;
    [SerializeField] private GameObject startText; 

    private void Start()
    {
        int index = -1;
            
        for (int i = 0; i < GameManager.Instance.topScores.Count; i++)
        {
            if (!(GameManager.Instance.lastScore >= GameManager.Instance.topScores[i])) continue;
            index = i;
            break;
        }

        if (index != -1)
        {
            GameManager.Instance.topScores.Insert(index, GameManager.Instance.lastScore);
            GameManager.Instance.topScores.RemoveAt(GameManager.Instance.topScores.Count - 1);
        }
            
        for (int i = 0; i < leaderboardBrackets.Count; i++)
        {
            leaderboardBrackets[i].text = (i==index) ? 
                "<u>" + (i + 1) + " - " + GameManager.Instance.topScores[i] + "</u>" : 
                (i + 1) + " - " + GameManager.Instance.topScores[i];
        }
        
        StartCoroutine(TextBlink());

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex - 1));
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    
    private IEnumerator TextBlink()
    {
        while (gameObject.activeSelf)
        {
            startText.SetActive(!startText.activeInHierarchy);

            yield return new WaitForSeconds((1f));
        }
        yield break;
    }
}
