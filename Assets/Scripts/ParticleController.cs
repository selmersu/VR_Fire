using UnityEngine;

public class ParticleController : MonoBehaviour
{
    // Array to store references to multiple ParticleSystem components
    public ParticleSystem[] particleSystems;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure all particle systems in the array are not null
        for (int i = 0; i < particleSystems.Length; i++)
        {
            if (particleSystems[i] == null)
            {
                particleSystems[i] = GetComponent<ParticleSystem>();
            }
        }
    }

    // Method to start all particle systems
    public void StartParticles()
    {
        foreach (ParticleSystem ps in particleSystems)
        {
            if (ps != null)
            {
                ps.Play();
            }
        }
    }

    // Method to stop all particle systems
    public void StopParticles()
    {
        foreach (ParticleSystem ps in particleSystems)
        {
            if (ps != null)
            {
                ps.Stop();
            }
        }
    }
}
