using TMPro;
using UnityEngine;

public class BouncyBall : MonoBehaviour
{
    public float minY = -3.5f;
    public float maxVelocity = 15f;

    private Rigidbody2D rb;

    int score = 0;
    int lives = 5;
    public TextMeshProUGUI _scoreTxt;
    public GameObject[] livesImage;

    public GameObject gameOverPanel;
    public GameObject youWinPanel;
    private int brickCount;

    
    public Gradient backgroundColorGradient;
    private Camera mainCamera;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        brickCount = FindObjectOfType<LevelGenerator>().transform.childCount;

        
        mainCamera = Camera.main;
    }

    
    void Update()
    {
        if (transform.position.y < minY)
        {
            if (lives <= 0)
            {
                GameOver();
                
            }
            
            transform.position = Vector3.zero;
            rb.velocity = Vector3.zero;
            lives--;
            livesImage[lives].SetActive(false);
        }

        if (rb.velocity.magnitude > maxVelocity)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            Destroy(collision.gameObject);
            score += 10;
            _scoreTxt.text = score.ToString("0000");
            brickCount--;
            
            ChangeBackgroundColor();

            if (brickCount <= 0)
            {
                youWinPanel.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                youWinPanel.SetActive(false);
            }
        }
    }

    void GameOver()
    {
       gameOverPanel.SetActive(true);
       Time.timeScale = 0;
       Destroy(gameObject);
    }

   
    void ChangeBackgroundColor()
    {
        Color newColor = new Color(Random.value, Random.value, Random.value);
        mainCamera.backgroundColor = newColor;
    }

}
