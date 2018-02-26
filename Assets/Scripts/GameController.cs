using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject[] hazards;
    public Vector3 spawnValue;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    private int score;
    public Text scoreText;

    public Text gameOverText;
    private bool gameOver;

    public Text restartText;
    private bool restart;

    public GameObject restartButton;

	// Use this for initialization
	void Start() {
        gameOverText.text = "";
        gameOver = false;

        restartButton.SetActive(false);
        restartText.text = "";
        restart = false;

        score = 0;
        UpdateScore();
        StartCoroutine( SpawnWaves());        
	}

    //private void Update()
    //{
    //    if (restart)
    //    {
    //        if (Input.GetKeyDown(KeyCode.R))
    //        {
    //            Application.LoadLevel(Application.loadedLevel);
    //        }
    //    }
    //}

    public void GameOver()
    {
        restartButton.SetActive(true);
        gameOver = true;
        gameOverText.text = "GAME OVER";
    }

    public void RestartGame()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void addScore(int value)
    {
        score = score + value;
        UpdateScore();
    }

    // Update Scpre
    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    // Update is called once per frame
    IEnumerator SpawnWaves () {
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValue.x, spawnValue.x),
                            spawnValue.y,
                            spawnValue.z
                            );
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);

                if (gameOver)
                {
                    restart = true;
                    restartText.text = "Press to restart";
                }
            }
            yield return new WaitForSeconds(waveWait);
        }        
    }
}
