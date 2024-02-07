using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;

public class title : MonoBehaviour
{
    public GameObject quit;
    private Button quitButton;
    public AudioSource Musicstart;

    private void Start()
    {
        quitButton = quit.GetComponent<Button>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            Musicstart.Play();
            SceneManager.LoadScene("easy1");
        }

        if (Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.Escape))
        {
            quitButton.onClick.Invoke();
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

#else
        Application.Quit();
#endif
    }
}
