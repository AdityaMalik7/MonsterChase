using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveForce = 10f;
    [SerializeField]
    private float jumpForce = 11f;

    private float movementX;
    private Rigidbody2D myBody;
    private Animator anim;
    private SpriteRenderer sr;

    private string WALK_ANIMATION = "Walk";
    private bool isGrounded;
    private string GROUND_TAG = "Ground";
    private string ENEMY_TAG = "Enemy";

    private Timer timerScript;
    private bool hasWon = false;

    [SerializeField] private AudioClip PlayerDeathSound;
    private AudioSource audioSource;

    [System.Obsolete]
    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        timerScript = FindObjectOfType<Timer>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        PlayerMoveKeyboard();
        AnimatePlayer();

        // ✅ Check if player survived for 60 seconds and trigger win
        if (timerScript != null && timerScript.RemainingTime <= 0 && !hasWon)
        {
            hasWon = true;
            Win();
        }
    }

    private void FixedUpdate()
    {
        PlayerJump();
    }

    void PlayerMoveKeyboard()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime * moveForce;
    }

    void AnimatePlayer()
    {
        if (movementX < 0)
        {
            anim.SetBool(WALK_ANIMATION, true);
            sr.flipX = true;
        }
        else if (movementX > 0)
        {
            anim.SetBool(WALK_ANIMATION, true);
            sr.flipX = false;
        }
        else
        {
            anim.SetBool(WALK_ANIMATION, false);
        }
    }

    void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isGrounded = false;
            myBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GROUND_TAG))
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag(ENEMY_TAG))
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(ENEMY_TAG))
        {
            Die();
        }
    }

    void Die()
    {


        if (timerScript != null)
        {
            timerScript.StopTimer();
        }


        if (PlayerDeathSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(PlayerDeathSound);
        }


        if (GameManger.instance != null)
        {
            GameManger.instance.ShowGameOverScreen();
        }


        Destroy(gameObject);
    }

    void Win()
    {
        Debug.Log("Player Wins!");

        if (GameManger.instance != null)
        {
            GameManger.instance.ShowWinScreen();
        }
    }
}
