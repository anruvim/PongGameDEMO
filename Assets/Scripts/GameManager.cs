using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Ball ball;
    public Paddle paddle;
    public Text ballParText;
    public Text countdownText;
    private Scene pongScene;

    public static Vector2 topLeft;
    public static Vector2 butRight;

    // Start is called before the first frame update
    void Start(){
        pongScene = SceneManager.GetSceneByName("PongGame");

        topLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height));
        butRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0));
        
        Paddle paddle1 = Instantiate(paddle) as Paddle;
        Paddle paddle2 = Instantiate(paddle) as Paddle;

        paddle1.Init(true); //upper
        paddle2.Init(false); //down

        Instantiate(ball);

    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
        SceneManager.UnloadScene(pongScene);
    }

    public void DisplayBallPar(float size, float speed) {
        size *= 100;
        ballParText.text = "SIZE: " + size.ToString("00") + " | SPEED: " + speed.ToString("F1");
    }

    public void DisplayCountdown(string countdown)
    {
        countdownText.text = countdown;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
