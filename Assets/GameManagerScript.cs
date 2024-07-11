using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    public GameObject enemy;
    public GameObject gameOverText;
    public AudioSource hitAudioSource;
    public TextMeshProUGUI scoreText;
    public GameObject bombParticle;

    private bool gameOverFlag = false;

    private int score = 0;
    private int gameTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        float x = Random.Range(-3.0f, 3.0f);
        Instantiate(enemy, new Vector3(x, 0, 10), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        // スコア表示
        scoreText.text = "SCORE " + score;

        // ゲームオーバーなら
        if (gameOverFlag == true)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("TitleScene");
            }
        }
    }

    void FixedUpdate()
    {
        if (gameOverFlag == true)
        {
            return;
        }

        // 敵の発生
        gameTimer++;
        int max = 50 - gameTimer / 100;
        int r = Random.Range(0, max);
        if(r==0)
        {
            float x = Random.Range(-3.0f, 3.0f);
            Instantiate(enemy, new Vector3(x, 0, 15), Quaternion.identity);
        }
    }

    // 弾と敵が衝突
    public void Hit(Vector3 position)
    {
        hitAudioSource.Play();
        score += 1;

        // 爆発パーティクル発生
        Instantiate(bombParticle, position, Quaternion.identity);
    }

    // ゲームオーバー開始
    public void GameOverStart()
    {
        gameOverText.SetActive(true);
        gameOverFlag = true;
    }

    public bool IsGameOver()
    {
        return gameOverFlag;
    }
}
