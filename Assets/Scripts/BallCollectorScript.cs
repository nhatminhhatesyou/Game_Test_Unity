using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class BallCollectorScript : MonoBehaviour
{
    public int winCondition = 10;
    public Material[] mats;
    public GameObject ball;
    int balls_count = 0;
    int balls_init = 10;
    public Text scoreText;
    int red_count,
        yellow_count,
        green_count;


    void Start()
    {
        //Random amount of each type of BALLS, ensure every type has the amount greater than 0
        red_count = Random.Range(1, balls_init - 2);
        yellow_count = Random.Range(1, balls_init - red_count -1);
        green_count = balls_init - red_count - yellow_count;
        
        //Random material of PLAYER
        ChangeMaterial();

        //Spawn balls
        for (int i = 1; i <= red_count; i++)
        {
            BallSpawner(ball, mats[1]);
        }

        for (int i = 1; i <= yellow_count; i++)
        {
            BallSpawner(ball, mats[2]);
        }

        for (int i = 1; i <= green_count; i++)
        {
            BallSpawner(ball, mats[0]);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        Material playerMaterial = this.GetComponent<Renderer>().material;
        
        if (other.gameObject.CompareTag("Ball"))
        {
            if (playerMaterial.color == other.GetComponent<Renderer>().material.color)
            {                              
                //Destroy the ball
                Destroy(other.transform.parent.gameObject);
                                   
                //Update amount of each type of Balls when player hits a ball - respawn ball
                if (playerMaterial.color == mats[1].color)
                {
                    if (red_count == 1)
                        BallSpawner(ball, mats[1]);
                    else
                    {
                        red_count--;
                        RandomBallSpawner();
                    }
                }
                else if (playerMaterial.color == mats[2].color)
                {
                    if (yellow_count == 1)
                        BallSpawner(ball, mats[2]);
                    else
                    {
                        yellow_count--;
                        RandomBallSpawner();
                    }
                }
                else if (playerMaterial.color == mats[0].color)
                {
                    if (green_count == 1)
                        BallSpawner(ball, mats[0]);
                    else
                    {
                        green_count--;
                        RandomBallSpawner();
                    }
                }

                //Randomly change the color of player
                ChangeMaterial();

                Debug.Log("Red: " + red_count + " Green: " + green_count + " Yellow: " + yellow_count);

                balls_count++;
                scoreText.text = "SCORE: " + balls_count;
                if (balls_count == winCondition)
                {
                    WinGame();
                }
            }
            else
            {
                EndGame();
            }
        }
    }

    private void ChangeMaterial()
    {
        int matIndex = Random.Range(0, mats.Length);
        this.GetComponentInChildren<MeshRenderer>().material = mats[matIndex]; 
    }

    private void BallSpawner(GameObject ball, Material mat)
    {
        Vector3 randomSpawnPosition = new Vector3(Random.Range(-7, 8), 2, Random.Range(-5, 9));
        ball.GetComponentInChildren<Renderer>().material = mat;
        Instantiate(ball, randomSpawnPosition, Quaternion.identity);
    }

    private void RandomBallSpawner()
    {
        int matIndex = Random.Range(0, mats.Length);
        BallSpawner(ball, mats[matIndex]);
        switch (matIndex)
        {
            case 0: green_count++; break;
            case 1: red_count++; break;
            case 2: yellow_count++; break;
            default: break;
        }
    }
    public void WinGame()
    {
        SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
    }

    public void EndGame()
    {
        SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 2);
    }
}
