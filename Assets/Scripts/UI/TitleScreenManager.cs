using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class TitleScreenManager : MonoBehaviour
{

    [SerializeField] private GameObject startText; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        StartCoroutine(TextBlink());
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1));
        }
    }

    private IEnumerator TextBlink()
    {
        while (gameObject.activeSelf)
        {
            startText.SetActive(!startText.activeInHierarchy);

            yield return new WaitForSeconds((0.5f));
        }
        yield break;
    }
    
    
}
