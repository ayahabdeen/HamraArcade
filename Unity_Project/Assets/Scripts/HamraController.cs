using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class HamraController : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    public Vector2 direction = Vector2.down;
    public Vector2 bombDirection;
    public float speed = 15;
    public Tilemap destructibleTiles;
    public int lives = 0;
    public GameObject pauseBombs;
    public bool bombsPaused;
    private bool isFrozen;

    [Header("Bomb")]
    public KeyCode inputKey = KeyCode.RightShift;
    public GameObject bombPrefab;
    private GameObject bomb;
    public float bombFuseTime = 3f;
    public int bombAmount = 5;
    public int bombsRemaining;
    private Vector2 position;

    public Text clocksAmount;
    public GameObject clocksDisplay;
    [Header("kiss")]
    public KeyCode ClockKey = KeyCode.LeftAlt;
    public GameObject ClockPrefab;
    private GameObject Clock;
    public float ClockFuseTime = 3f;
    //public int kissAmount = 1;
    public int ClockRemaining = 5;

    [Header("Explosion")]
    public Explosion explosionPrefab;
    public LayerMask explosionLayerMask;
    public float explosionDuration = 1f;
    public int explosionRadius = 1;

    [Header("Input")]
    public KeyCode inputUp = KeyCode.UpArrow;
    public KeyCode inputDown = KeyCode.DownArrow;
    public KeyCode inputLeft = KeyCode.LeftArrow;
    public KeyCode inputRight = KeyCode.RightArrow;

    [Header("Sprites")]
    public AnimatedSpriteRenderer spriteRendererUp;
    public AnimatedSpriteRenderer spriteRendererDown;
    public AnimatedSpriteRenderer spriteRendererLeft;
    public AnimatedSpriteRenderer spriteRendererRight;
    public AnimatedSpriteRenderer spriteRendererDeath;
    private AnimatedSpriteRenderer activeSpriteRenderer;

    [Header("audio")]
    public AudioClip hamraAttackClip;
    public AudioClip bombClip;
    public AudioClip hamraIsHit;
    public AudioSource radio;

    public void Start()
    {
        bombDirection = Vector2.left;
        bombAmount = 5;
        pauseBombs.SetActive(false);
        bombsPaused = false;
        isFrozen = false;
        radio = GetComponent<AudioSource>();
        clocksDisplay.SetActive(true);
    }
    private void OnEnable()
    {
        bombsRemaining = bombAmount;
    }
    public void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        activeSpriteRenderer = spriteRendererDown;
    }
    public void Update()
    {
        if (FindObjectOfType<GameManager>().gameIsPaused == false && isFrozen == false)
        {
            if (bombsRemaining > 0 && Input.GetKeyDown(inputKey))
            {
                generateBomb();
            }
            if (ClockRemaining > 0 && Input.GetKeyDown(ClockKey))
            {
                generateClock();
            }
            if (bombsPaused == true)
            {
                StartCoroutine(ResertBombs());
            }

            if (Input.GetKey(inputUp))
            {
                SetDirection(Vector2.up, spriteRendererUp);
                bombDirection = Vector2.up;
            }
            else if (Input.GetKey(inputDown))
            {
                SetDirection(Vector2.down, spriteRendererDown);
                bombDirection = Vector2.down;
            }
            else if (Input.GetKey(inputLeft))
            {
                SetDirection(Vector2.left, spriteRendererLeft);
                bombDirection = Vector2.left;
            }
            else if (Input.GetKey(inputRight))
            {
                SetDirection(Vector2.right, spriteRendererRight);
                bombDirection = Vector2.right;
            }
            else if (Input.GetKey(inputDown) && Input.GetKey(inputLeft))
            {
                SetDirection(Vector2.down, spriteRendererDown);
                bombDirection = Vector2.zero;
            }
            else if (Input.GetKey(inputUp) && Input.GetKey(inputLeft))
            {
                SetDirection(Vector2.up, spriteRendererUp);
                bombDirection = Vector2.zero;
            }
            else if (Input.GetKey(inputDown) && Input.GetKey(inputRight))
            {
                SetDirection(Vector2.down, spriteRendererDown);
                bombDirection = Vector2.zero;
            }
            else if (Input.GetKey(inputUp) && Input.GetKey(inputRight))
            {
                SetDirection(Vector2.up, spriteRendererUp);
                bombDirection = Vector2.zero;
            }
            else
            {
                SetDirection(Vector2.zero, activeSpriteRenderer);
            }
        }           
    }
    private void generateBomb()
    {
        position = transform.position;
        bomb = Instantiate(bombPrefab, position, Quaternion.identity);
        Physics2D.IgnoreCollision(bomb.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        //radio.clip = bombClip;
        //radio.Play();

        //bombsRemaining--;
        //clocksAmount.text = bombsRemaining.ToString();
        if (bombsRemaining == 0)
        {
            bombsPaused = true;
            Debug.Log("Hamra" + bombsRemaining);
        }       
    }
    private void generateClock()
    {
        position = transform.position;
        Clock = Instantiate(ClockPrefab, position, Quaternion.identity);
        Physics2D.IgnoreCollision(Clock.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        radio.clip = hamraAttackClip;
        radio.Play();

        ClockRemaining--;
        clocksAmount.text = ClockRemaining.ToString();
        /*if (ClockRemaining == 0)
        {
            bombsPaused = true;
            Debug.Log("Hamra" + bombsRemaining);
        }*/
    }
    public void AddBomb()
    {
        bombsRemaining = 5;
    }
    public void AddClock()
    {
        ClockRemaining = 5;
    }
    public void FixedUpdate()
    {
        Vector2 position = rigidbody.position;
        Vector2 translation = direction * speed * Time.fixedDeltaTime;

        rigidbody.MovePosition(position + translation);
    }
    public void SetDirection(Vector2 newDirection, AnimatedSpriteRenderer spriteRenderer)
    {
        direction = newDirection;

        spriteRendererUp.enabled = spriteRenderer == spriteRendererUp;
        spriteRendererDown.enabled = spriteRenderer == spriteRendererDown;
        spriteRendererLeft.enabled = spriteRenderer == spriteRendererLeft;
        spriteRendererRight.enabled = spriteRenderer == spriteRendererRight;

        activeSpriteRenderer = spriteRenderer;
        activeSpriteRenderer.idle = direction == Vector2.zero;
    }
    public void DeathSequence()
    {
        enabled = false;
        //GetComponent<BombController>().enabled = false;

        spriteRendererUp.enabled = false;
        spriteRendererDown.enabled = false;
        spriteRendererLeft.enabled = false;
        spriteRendererRight.enabled = false;
        spriteRendererDeath.enabled = true;

        Invoke(nameof(OnDeathSequenceEnded), 1.25f);
    }
    public void OnDeathSequenceEnded()
    {
        gameObject.SetActive(false);
        //FindObjectOfType<GameManager>().CheckWinState();
    }
    public void SpeedIncrease()
    {
        StartCoroutine(IncreaseSpeed());
    }
    public void SpeedDecrease()
    {
        StartCoroutine(DecreaseSpeed());
    }
    public IEnumerator IncreaseSpeed()
    {
        speed = speed + 50;
        yield return new WaitForSeconds(10);
        speed = 50;
    }
    public IEnumerator DecreaseSpeed()
    {
        speed = speed - 2;
        yield return new WaitForSeconds(10);
        speed = 5;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            other.isTrigger = false;
        }
    }
    public IEnumerator ResertBombs()
    {
        bombsPaused = false;
        clocksDisplay.SetActive(false);
        pauseBombs.SetActive(true);
        yield return new WaitForSeconds(5);
        ClockRemaining = 3;
        pauseBombs.SetActive(false);
        clocksAmount.text = ClockRemaining.ToString();
        clocksDisplay.SetActive(true);
    }
    public IEnumerator FreezeMe()
    {
        isFrozen = true;
        yield return new WaitForSeconds(10);  //Freezer
        isFrozen = false;
    }

    public void HamraCry()
    {
        radio.clip = hamraIsHit;
        radio.Play();
    }

    public void Explosion()
    {
        radio.clip = bombClip;
        radio.Play();
    }
}
