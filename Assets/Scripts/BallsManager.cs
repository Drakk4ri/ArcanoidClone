using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsManager : MonoBehaviour
{

    #region Singleton
    private static BallsManager instance;
    public static BallsManager Instance => instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    #endregion

    private Ball firstBall;

    [SerializeField] private Ball ballPrefab;
    [SerializeField] private int firstBallForce = 200;

    private Rigidbody2D firstBallRb;

    public List<Ball> Balls { get; set; }

    private void Start()
    {
        InitBall();
    }

    private void Update()
    {
        if (!GameManager.Instance.isGameStaretd)
        {
            Vector3 platformPosition = Platform.Instance.gameObject.transform.position;
            Vector3 startingPoition = new Vector3(platformPosition.x, platformPosition.y + 0.55f, 0);
            firstBall.transform.position = startingPoition;


            if (Input.GetKey(KeyCode.Space))
            {
                firstBallRb.isKinematic = false;
                firstBallRb.AddForce(new Vector2(0, firstBallForce));
                GameManager.Instance.isGameStaretd = true;
            }
        }
    }

    private void InitBall()
    {
        Vector3 platformPosition = Platform.Instance.gameObject.transform.position;
        Vector3 startingPoition = new Vector3(platformPosition.x, platformPosition.y + 0.55f , 0);
        firstBall = Instantiate(ballPrefab, startingPoition, Quaternion.identity);
        firstBallRb = firstBall.GetComponent<Rigidbody2D>();


        this.Balls = new List<Ball>
        {
            firstBall,
        };
    }
}
