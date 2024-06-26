using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishLine : MonoBehaviour
{
    public string nextSceneName; 
    public ParticleSystem finishLineParticles; 

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


            if (!string.IsNullOrEmpty(nextSceneName))
            {
                Invoke("LoadNextScene",0.7f); 
            }
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
