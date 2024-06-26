using UnityEngine;

public class SnowTrail : MonoBehaviour
{
    public GameObject player; 
    public ParticleSystem snowParticles; 

    private bool isTrailActive = false;

    void Start()
    {
        if (player == null)
        {
            enabled = false; // Disable the script if player reference is not set
            return;
        }

        if (snowParticles != null)
        {
            snowParticles.Stop();
            isTrailActive = false;
        }
        else
        {
            enabled = false; 
        }
    }

    void Update()
    {
        bool isPlayerGrounded = player.GetComponent<PlayerController>().IsGrounded;
        float horizontalInput = Input.GetAxis("Horizontal");

        if (isPlayerGrounded && Mathf.Abs(horizontalInput) > 0 && !isTrailActive)
        {
            snowParticles.Play();
            isTrailActive = true;
        }
        else if (!isPlayerGrounded || Mathf.Abs(horizontalInput) == 0 && isTrailActive)
        {
            snowParticles.Stop();
            isTrailActive = false;
        }
    }
}
