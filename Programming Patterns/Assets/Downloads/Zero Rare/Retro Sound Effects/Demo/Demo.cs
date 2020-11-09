using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour
{
    public AudioSource audioSource;

    //jump
    public AudioClip[] jumpSounds;
    public Sprite[] playerSprites;
    public GameObject player;
    public SpriteRenderer playerSpriteRenderer;
    private float playerGravity;
    private float gravityMulti = 15;
    private int jumpSoundIndex;

    //explosion
    public AudioClip[] explosionSounds;
    public Sprite[] explosionSprites;
    public SpriteRenderer explosionSpriteRenderer;
    private int explosionSpriteIndex;
    private int explosionSoundIndex;
    private bool aniExplosion;
    private float aniExplosionDelay;

    //coin
    public AudioClip[] coinSounds;
    public Sprite[] coinSprites;
    public SpriteRenderer coinSpriteRenderer;
    private int coinSpriteIndex;
    private int coinSoundIndex;
    private bool aniCoin;
    private float aniCoinDelay;

    //power up
    public AudioClip[] powerUpSounds;
    public Sprite[] powerUpSprites;
    public SpriteRenderer powerUpSpriteRenderer;
    private int powerUpSoundIndex;
    private bool aniPowerUp;
    private float aniPowerUpDelay;

    //game over
    public AudioClip[] gameOverSounds;
    public Sprite[] gameOverSprites;
    public SpriteRenderer gameOverSpriteRenderer;
    public GameObject gameOverGo;
    private bool aniGameOver;
    private float aniGameOverDelay;
    private float gameOverGravity;
    private float gameOverGravityMulti = 25;
    private int gameOverSoundIndex;

    //laser
    public AudioClip[] laserSounds;
    public GameObject laserPrefab;
    public Transform laserSpawnPosition;
    private int laserSoundIndex;
    private List<GameObject> laserGameObjects = new List<GameObject>();

    public void Update()
    {
        //game over
        if (aniGameOver)
        {
            gameOverGravity -= gameOverGravityMulti * Time.deltaTime;
            if (gameOverGravity < -12)
                gameOverGravity = -12;

            Vector3 losePosition = gameOverGo.transform.position + Vector3.up * gameOverGravity * Time.deltaTime;
            gameOverGo.transform.position = losePosition;
            gameOverGo.transform.eulerAngles += Vector3.forward * 15 * Time.deltaTime;

            aniGameOverDelay -= Time.deltaTime;
            if (aniGameOverDelay < 0)
            {
                gameOverSpriteRenderer.sprite = gameOverSprites[0];
                gameOverGo.transform.position = new Vector3(gameOverGo.transform.position.x, 0, gameOverGo.transform.position.z);
                gameOverGo.transform.eulerAngles = Vector3.zero;

                aniGameOver = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && !aniGameOver)
        {
            gameOverSpriteRenderer.sprite = gameOverSprites[1];
            gameOverGravity = 9;
            aniGameOverDelay = 2;
            aniGameOver = true;

            audioSource.PlayOneShot(gameOverSounds[gameOverSoundIndex]);
            gameOverSoundIndex++;
            if (gameOverSoundIndex == 30)
                gameOverSoundIndex = 0;
        }


        //power up
        if (aniPowerUp)
        {
            aniPowerUpDelay -= Time.deltaTime;
            if (aniPowerUpDelay < 0)
            {
                powerUpSpriteRenderer.sprite = powerUpSprites[0];
                aniPowerUp = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && !aniPowerUp)
        {
            aniPowerUp = true;
            powerUpSpriteRenderer.sprite = powerUpSprites[1];
            aniPowerUpDelay = .75f;

            audioSource.PlayOneShot(powerUpSounds[powerUpSoundIndex]);
            powerUpSoundIndex++;
            if (powerUpSoundIndex == 30)
                powerUpSoundIndex = 0;
        }

        //coin
        if (aniCoin)
        {
            aniCoinDelay -= Time.deltaTime;
            if (aniCoinDelay < 0)
            {
                aniCoinDelay = .05f;

                coinSpriteIndex++;
                if (coinSpriteIndex == coinSprites.Length)
                {
                    aniCoin = false;
                    coinSpriteRenderer.sprite = coinSprites[0];
                }
                else
                    coinSpriteRenderer.sprite = coinSprites[coinSpriteIndex];
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) && !aniCoin)
        {
            aniCoin = true;
            coinSpriteIndex = -1;

            audioSource.PlayOneShot(coinSounds[coinSoundIndex]);
            coinSoundIndex++;
            if (coinSoundIndex == 30)
                coinSoundIndex = 0;
        }

        //explosion
        if (aniExplosion)
        {
            aniExplosionDelay -= Time.deltaTime;
            if (aniExplosionDelay<0)
            {
                aniExplosionDelay = .1f;

                explosionSpriteIndex++;
                if (explosionSpriteIndex == explosionSprites.Length)
                {
                    aniExplosion = false;
                    explosionSpriteRenderer.sprite = null;
                }
                else
                    explosionSpriteRenderer.sprite = explosionSprites[explosionSpriteIndex];
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha6) && !aniExplosion)
        {
            aniExplosion = true;
            explosionSpriteIndex = -1;

            audioSource.PlayOneShot(explosionSounds[explosionSoundIndex]);
            explosionSoundIndex++;
            if (explosionSoundIndex == 30)
                explosionSoundIndex = 0;
        }

        //jump
        playerGravity -= gravityMulti * Time.deltaTime;
        if (playerGravity < -3)
            playerGravity = -3;

        if (player.transform.position.y != 0)
            playerSpriteRenderer.sprite = playerSprites[1];
        else
            playerSpriteRenderer.sprite = playerSprites[0];

        if (Input.GetKey(KeyCode.Alpha1) && player.transform.position.y == 0)
        {
            player.transform.position += Vector3.up * 0.01f;

            playerGravity = 5;

            audioSource.PlayOneShot(jumpSounds[jumpSoundIndex]);

            jumpSoundIndex++;
            if (jumpSoundIndex == 30)
                jumpSoundIndex = 0;
        }

        Vector3 playerPosition = player.transform.position + Vector3.up * playerGravity * Time.deltaTime;

        if (playerPosition.y < 0)
            playerPosition = new Vector3(player.transform.position.x, 0, player.transform.position.z);

        player.transform.position = playerPosition;

        //laser
        laserGameObjects.RemoveAll(item => item == null);
        if (laserGameObjects.Count > 0)
        {
            for (int i = 0; i < laserGameObjects.Count; i++)
            {
                if (!laserGameObjects[i].GetComponent<SpriteRenderer>().isVisible)
                {
                    Destroy(laserGameObjects[i]);
                }
                else
                {
                    laserGameObjects[i].transform.position += laserSpawnPosition.up * 12 * Time.deltaTime;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            GameObject go = Instantiate(laserPrefab, laserSpawnPosition.position, laserSpawnPosition.rotation) as GameObject;
            laserGameObjects.Add(go);
            
            audioSource.PlayOneShot(laserSounds[laserSoundIndex]);

            laserSoundIndex++;
            if (laserSoundIndex == 30)
                laserSoundIndex = 0;
        }
    } 
}
