using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBallController : MonoBehaviour
{
    public GameObject Player; // Reference to the player with capital "P"
    public float FollowSpeed = 1.5f; // Speed multiplier for the snowball relative to the player
    public float MaxSpeedMultiplier = 2f; // Maximum speed multiplier based on player's speed
    public float MinSpeedMultiplier = 0.5f; // Minimum speed multiplier based on player's speed
    public ParticleSystem SnowParticles;
    public AudioClip move; // Audio clip to play for movement
    private Rigidbody2D rb;
    private Vector2 playerPosition;
    private AudioSource audioSource; // Reference to the AudioSource component
    private Vector2 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerPosition = Player.transform.position;
        direction = (playerPosition - (Vector2)transform.position).normalized;

        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // Add an AudioSource component if one doesn't exist
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (move != null)
        {
            audioSource.clip = move;
            audioSource.Play(); // Start playing the audio
        }
        else
        {
            Debug.LogWarning("No move AudioClip assigned to SnowBallController.");
        }
    }

    void FixedUpdate()
    {
        FollowPlayer();
        AdjustParticleEmission();
        AdjustAudioVolume();
    }

    void FollowPlayer()
    {
        playerPosition = Player.transform.position;
        direction = (playerPosition - (Vector2)transform.position).normalized;
        rb.velocity = direction * FollowSpeed;
    }

    void AdjustParticleEmission()
    {
        float playerSpeed = Mathf.Abs(Player.GetComponent<Rigidbody2D>().velocity.magnitude);
        float speedMultiplier = Mathf.Lerp(MinSpeedMultiplier, MaxSpeedMultiplier, playerSpeed / FollowSpeed);

        var emission = SnowParticles.emission;
        emission.rateOverTime = 100f * speedMultiplier; // Adjust particle emission rate based on speed
    }

    void AdjustAudioVolume()
{
    float distance = Vector2.Distance(transform.position, playerPosition);
    float maxVolumeDistance = 5f; 
    float minVolumeDistance = 0.5f; 

    float volume = Mathf.Lerp(0.7f, 0.2f, (distance - minVolumeDistance) / (maxVolumeDistance - minVolumeDistance));

    audioSource.volume = Mathf.Clamp01(volume);
}
}