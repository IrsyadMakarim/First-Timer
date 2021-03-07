using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] zombies;
    public int riseSpeed = 1;
    public int scoreThreshold = 5;
    public Image life01;
    public Image life02;
    public Image life03;
    public Text scoreText;
    public Button gameOverButton;

    private bool isRising = false;
    private bool isFalling = false;
    private int activeZombieIndex = 0;
    private Vector2 startPosition;
    private int zombieSmashed;
    private int livesRemaining;
    private bool gameOver;

 
    // Start is called before the first frame update
    void Start()
    {
        zombieSmashed = 0;
        livesRemaining = 3;
        gameOver = false;
        scoreText.text = zombieSmashed.ToString();
        pickNewZombie();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            if (isRising)
            {
                if (zombies[activeZombieIndex].transform.position.y - startPosition.y >= 3f)
                {
                    //Jika true, maka turunkan
                    isRising = false;
                    isFalling = true;
                }
                else
                {
                    zombies[activeZombieIndex].transform.Translate(Vector2.up * Time.deltaTime * riseSpeed);
                }
            }
            else if (isFalling)
            {
                if (zombies[activeZombieIndex].transform.position.y - startPosition.y <= 0f)
                {
                    //Stop making it fall
                    isFalling = false;
                    isRising = false;
                    livesRemaining--;
                    updateLifeUI();
                }
                else
                {
                    zombies[activeZombieIndex].transform.Translate(Vector2.down * Time.deltaTime * riseSpeed);
                }
            }
            else
            {
                zombies[activeZombieIndex].transform.position = startPosition;
                pickNewZombie();
            }
        }     
    }

    private void updateLifeUI()
    {
        if (livesRemaining == 3)
        {
            life01.gameObject.SetActive(true);
            life02.gameObject.SetActive(true);
            life03.gameObject.SetActive(true);
        }
        if (livesRemaining == 2)
        {
            life01.gameObject.SetActive(true);
            life02.gameObject.SetActive(true);
            life03.gameObject.SetActive(false);
        }
        if (livesRemaining == 1)
        {
            life01.gameObject.SetActive(true);
            life02.gameObject.SetActive(false);
            life03.gameObject.SetActive(false);
        }
        if (livesRemaining == 0)
        {
            //Game Over
            life01.gameObject.SetActive(false);
            life02.gameObject.SetActive(false);
            life03.gameObject.SetActive(false);
            gameOver = true;
            gameOverButton.gameObject.SetActive(true);
        }
    }

    private void pickNewZombie()
    {
        isRising = true;
        isFalling = false;
        activeZombieIndex = UnityEngine.Random.Range(0, zombies.Length); //Generate sebuah angka antara 0 dan 6
        startPosition = zombies[activeZombieIndex].transform.position;
    }

    public void killEnemy()
    {
        zombieSmashed++;
        increaseSpawnSpeed();
        scoreText.text = zombieSmashed.ToString();
        //Code to kill enemy
        zombies[activeZombieIndex].transform.position = startPosition;
        pickNewZombie();
    }

    private void increaseSpawnSpeed()
    {
        if(zombieSmashed >= scoreThreshold)
        {
            riseSpeed++;
            scoreThreshold *= 2;
        }
    }

    public void onRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void onMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
