using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float baseMoveSpeed = 28f;
    public float jumpForce = 120f;
    public float flipSpeed = 400f;
    public float groundCheckRadius = 3f;

    public AudioClip moveSound;

    private Rigidbody2D rb;
    private AudioSource audioSource;
    private bool isGrounded = false;
    private bool isFlipping = false;
    private bool hasJumped = false;
    private int flipDirection = 1;

    public bool IsGrounded
    {
        get { return isGrounded; }
    }

void Start()
{
    rb = GetComponent<Rigidbody2D>();
    audioSource = GetComponent<AudioSource>();

    if (rb == null)
    {
        Debug.LogError("Rigidbody2D component not found!");
    }

    if (audioSource == null)
    {
        Debug.LogError("AudioSource component not found!");
    }

    if (moveSound != null)
    {
        audioSource.clip = moveSound;
    }
    else
    {
        Debug.LogWarning("Move sound clip is missing!");
    }
}

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(transform.position, groundCheckRadius, LayerMask.GetMask("snow"));
        Move();

        if (Input.GetKey(KeyCode.RightArrow) && !isFlipping)
        {
            StartFlip(1);
        }

        if (Input.GetKeyUp(KeyCode.RightArrow) && isFlipping)
        {
            StopFlip();
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    void Move()
{
    float horizontalInput = Input.GetAxis("Horizontal");

    if (horizontalInput > 0)
    {
        Vector2 movement = new Vector2(horizontalInput, 0f) * GetAdjustedSpeed() * Time.deltaTime;
        transform.Translate(movement);
    }

    if (horizontalInput != 0 && isGrounded)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.UnPause(); 
        }
    }
    else if (horizontalInput == 0 || !isGrounded)
    {
        audioSource.Pause(); 
    }

    float volumeModifier = GetVolumeModifier();
    audioSource.volume = volumeModifier;
}

    float GetAdjustedSpeed()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckRadius + 0.1f, LayerMask.GetMask("snow"));
        if (hit.collider != null)
        {
            float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
            float speedModifier = Mathf.Lerp(0.5f, 1.5f, slopeAngle / 45f); 
            return baseMoveSpeed * speedModifier;
        }
        return baseMoveSpeed;
    }

    float GetVolumeModifier()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckRadius + 0.1f, LayerMask.GetMask("snow"));
        if (hit.collider != null)
        {
            float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
            float volumeModifier = Mathf.Lerp(0.5f, 1.5f, slopeAngle / 45f); 
            return volumeModifier;
        }
        return 1f; // Default volume if not on slope
    }

    void Jump()
    {
        if (!hasJumped)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            hasJumped = true;
            audioSource.Pause(); // Pause the audio
        }
    }

    public void OnGroundEnter()
    {
        isGrounded = true;
        hasJumped = false;
        audioSource.UnPause(); // Resume the audio
    }

    public void OnGroundExit()
    {
        isGrounded = false;
    }

    public void OnRockCollision()
    {
        RestartLevel();
        hasJumped = false;
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void StartFlip(int direction)
    {
        isFlipping = true;
        flipDirection = direction;
        StartCoroutine(Flip());
    }

    void StopFlip()
    {
        isFlipping = false;
    }

    IEnumerator Flip()
    {
        while (isFlipping)
        {
            float rotationAmount = flipSpeed * flipDirection * Time.deltaTime;
            transform.Rotate(Vector3.forward, -rotationAmount); // Rotate counterclockwise
            yield return null;
        }
    }

    void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}