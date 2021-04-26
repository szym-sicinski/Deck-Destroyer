using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ParticlesType // Enum for diffrent types of particles
{
    BLOOD,
    HEAL,
    BUFF_STR,
    BUFF_DEX
}
public class ParticleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bloodParticles;
    [SerializeField] private GameObject healParticles;
    [SerializeField] private GameObject BuffStrParticles;
    [SerializeField] private GameObject BuffDexParticles;

    public void SpawnParticles(Vector3 position, ParticlesType type) //Spawning particles of given type at given position and destroys them in 1.5 second
    {
        GameObject particles;
        switch (type)
        {
            case ParticlesType.BLOOD:
                particles = Instantiate(bloodParticles, position, Quaternion.identity) as GameObject;
                break;
            case ParticlesType.HEAL:
                particles = Instantiate(healParticles, position, Quaternion.identity) as GameObject;
                break;
            case ParticlesType.BUFF_STR:
                particles = Instantiate(BuffStrParticles, position, Quaternion.identity) as GameObject;
                break;
            case ParticlesType.BUFF_DEX:
                particles = Instantiate(BuffDexParticles, position, Quaternion.identity) as GameObject;
                break;
            default:
                Debug.LogError("Wrong particles type");
                particles = null;
                break;
        }
        Destroy(particles,1.5f);
    }

}
