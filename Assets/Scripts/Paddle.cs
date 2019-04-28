using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField]
    float speed;
    float width;

    string input;
    Vector2 startPos;
    Touch touch;
    float move, deltaX;
    public bool isUpper;
    float direction;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        width = transform.localScale.x;
        rb = GetComponent<Rigidbody2D>();
    }

    public void Init(bool isUpperPad)
    {

        isUpper = isUpperPad;

        Vector2 pos = Vector2.zero;

        if (isUpperPad)
        {
            //place on the top
            pos = new Vector2(0, GameManager.topLeft.y);
            pos -= Vector2.up * transform.localScale.y;

            input = "PaddleTop";
        }
        else
        {
            //palce on the buttom
            pos = new Vector2(0, GameManager.butRight.y);
            pos += Vector2.up * transform.localScale.y;
            input = "PaddleDown";
        }
        //update Paddle Position
        transform.position = pos;
        transform.name = input;
    }
    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetAxis(input) != 0)
        {
            move = Input.GetAxis(input) * Time.deltaTime * speed;
            if (transform.position.x < GameManager.topLeft.x + width / 2 && move < 0)
            {
                move = 0;
            }
            if (transform.position.x > GameManager.butRight.x - width / 2 && move > 0)
            {
                move = 0;
            }
            transform.Translate(move * Vector2.right);
        }*/

        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                touch = Input.GetTouch(i);
                Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        if (touchPos.y > 1 && isUpper)
                            deltaX = touchPos.x - transform.position.x;
                        if (touchPos.y < -1 && !isUpper)
                            deltaX = touchPos.x - transform.position.x;
                        break;
                    case TouchPhase.Moved:
                        if (touchPos.y > 1 && isUpper)
                            Move(touchPos);
                        if (touchPos.y < -1 && !isUpper)
                            Move(touchPos);
                        break;
                    case TouchPhase.Ended:
                        if (touchPos.y > 1 && isUpper)
                            rb.velocity = Vector2.zero;
                        if (touchPos.y < -1 && !isUpper)
                            rb.velocity = Vector2.zero;
                        break;
                }
            }
        }
    }

    void Move(Vector2 touchPos)
    {
        //moving borders
        if (transform.position.x < GameManager.topLeft.x + width / 2 && touchPos.x - deltaX < transform.position.x)
            rb.velocity = Vector2.zero;
        else if (transform.position.x > GameManager.butRight.x - width / 2 && touchPos.x - deltaX > transform.position.x)
            rb.velocity = Vector2.zero;
        else
            rb.MovePosition(new Vector2(touchPos.x - deltaX, transform.position.y));
    }
}