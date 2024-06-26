using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBallController : MonoBehaviour
{
    public GameObject Player; 
    public float FollowSpeed = 35f; 
    public float MaxSpeedMultiplier = 3f; 
    public float MinSpeedMultiplier = 1f; 
    public ParticleSystem SnowParticles;
    public AudioClip move; 
    private Rigidbody2D rb;
    private Vector2 playerPosition;
    private AudioSource audioSource; 
    private Vector2 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerPosition = Player.transform.position;
        direction = (playerPosition - (Vector2)transform.position).normalized;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
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
        emission.rateOverTime = 100f * speedMultiplier; 
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