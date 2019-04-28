using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour {

    float speed, size, radius;
    bool gameOver;
    Vector2 direction, starting;
    private GameManager gm;

    void RespawnBall(){
        gameOver = false;
        transform.position = direction = Vector2.zero;
        GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        size = Random.Range(0.6f, 1f);
        speed = Random.Range(8f, 13f);
        transform.localScale = new Vector2(size, size);
        radius = transform.localScale.x / 2;
        gm.DisplayBallPar(size, speed);
        StartCoroutine(WaitAndStart());
    }

    void Start(){
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        RespawnBall();
    }

    private IEnumerator WaitAndStart(){
        float currentTime = 2f;
        while (currentTime > 0){
            gm.DisplayCountdown(currentTime.ToString("0"));
            yield return new WaitForSeconds(1.0f);
            currentTime--;
        }
        gm.DisplayCountdown("GO");
        direction = RandomDir();
    }

    private Vector2 RandomDir(){
        Vector2 ballDirection = new Vector2(0, 0);
        while (ballDirection.x < 0.15 && ballDirection.x > -0.15 || ballDirection.y < 0.15 && ballDirection.y > -0.15){
            ballDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }
        return ballDirection;
    }

    private IEnumerator RoundEnd(string winner)
    {
        Handheld.Vibrate();
        Debug.Log(winner);
        gameOver = true;
        Time.timeScale = 0.1f;
        float pauseTime = Time.realtimeSinceStartup + 0.5f;
        while (Time.realtimeSinceStartup < pauseTime)
            yield return 0;
        Time.timeScale = 1;
        RespawnBall();
    }

    void Update(){
            transform.Translate(direction * speed * Time.deltaTime);
            //bounce off walls
            if (transform.position.x > GameManager.butRight.x - radius && direction.x > 0)
            {
                direction.x = -direction.x;
            }
            if (transform.position.x < GameManager.topLeft.x + radius && direction.x < 0)
            {
                direction.x = -direction.x;
            }
        if (!gameOver)
        {
            //Game Over
            if (transform.position.y < GameManager.butRight.y + radius && direction.y < 0)
            {
                Debug.Log("Still Updating!");
                StartCoroutine(RoundEnd("Upper Wins!"));
            }
            if (transform.position.y > GameManager.topLeft.y - radius && direction.y > 0)
            {
                StartCoroutine(RoundEnd("Down Wins!"));
                Debug.Log("Still Updating!");
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Paddle" && !gameOver)
        {
            bool isUpper = other.GetComponent<Paddle>().isUpper;

            if(isUpper && direction.y > 0)
            {
                direction.y = -direction.y;
            }
            if (!isUpper && direction.y < 0)
            {
                direction.y = -direction.y;
            }
        }
    }
}
