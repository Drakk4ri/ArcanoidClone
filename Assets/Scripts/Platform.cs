using UnityEngine;

public class Platform : MonoBehaviour
{

    #region Singleton
    private static Platform instance;
    public static Platform Instance => instance;

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


    [SerializeField] private int platformSpeed = 1;
    [SerializeField] private int bounceBallForce = 200;


    void Update()
    {
        var movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * platformSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ball")
        {
            Rigidbody2D ballrigidbody2D = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector3 hitPoint = collision.contacts[0].point;
            Vector3 platformCenter = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y);

            ballrigidbody2D.velocity = Vector2.zero;

            float difference = platformCenter.x - hitPoint.x;

            if (hitPoint.x < platformCenter.x)
            {
                ballrigidbody2D.AddForce(new Vector2(-(Mathf.Abs(difference * 200)), bounceBallForce));
            }
            else
            {
                ballrigidbody2D.AddForce(new Vector2(Mathf.Abs(difference * 200), bounceBallForce));
            }

        }
    }

}
