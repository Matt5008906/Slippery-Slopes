using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishLine : MonoBehaviour
{
    public string nextSceneName; // Name of the next scene to load
    public ParticleSystem finishLineParticles; // Assign your ParticleSystem in the Inspector

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player crossed the finish line!");


            // Play particle effect
            if (finishLineParticles != null)
            {
                finishLineParticles.Play();
            }


            // Load next scene after a delay (if any)
            if (!string.IsNullOrEmpty(nextSceneName))
            {
                // Optionally, add a delay before loading the next scene
                Invoke("LoadNextScene",0.7f); // 2 seconds delay
            }
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
