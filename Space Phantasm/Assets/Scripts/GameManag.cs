using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManag : MonoBehaviour
{
    public GameObject gameOver;
    public GameObject gameWin;

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            gameOver.SetActive(true);

        }

        if (GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            gameWin.SetActive(true);

        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
