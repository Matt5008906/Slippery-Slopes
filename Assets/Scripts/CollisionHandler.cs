using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public AudioClip deathSound; 

    private AudioSource audioSource;
    private PlayerController playerController;

    private bool isCrushed = false; // Flag to prevent multiple collisions triggering multiple actions

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();            
        GetComponent<AudioSource>().enabled = true;


        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); 
        }

        audioSource.clip = deathSound;

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isCrushed) return; 

        if (collision.gameObject.CompareTag("snow"))
        {
            playerController.OnGroundEnter();
        }
        else if (collision.gameObject.CompareTag("rock") || collision.gameObject.CompareTag("snowBall"))
        {
            PlayDeathSound();
            playerController.OnRockCollision(); 
        }

        float dotDown = Vector2.Dot(transform.up, Vector2.down);
        if (dotDown >= 0.40f)
        {
            PlayDeathSound();
            playerController.OnRockCollision();
        }
    }

    void PlayDeathSound()
    {
        if (audioSource != null && deathSound != null)
        {
            audioSource.Play(); 
            isCrushed = true;
        }
        else
        {
        }
    }
}