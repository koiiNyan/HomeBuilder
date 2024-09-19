using UnityEngine;
using System.Collections;

public class ParticleManager : MonoBehaviour
{
    public GameObject particlePrefab;
    public float particleDuration = 2f;
    //private readonly Vector3 defaultScale = new Vector3(0.2f, 0.2f, 0.2f);

    public void PlayParticleAtPosition(Vector3 localPosition, Transform parent, Vector3 scale)
    {
        GameObject particleInstance = Instantiate(particlePrefab, parent);
        particleInstance.transform.localPosition = localPosition;
        particleInstance.transform.localScale = scale;

        ParticleSystem particleSystem = particleInstance.GetComponent<ParticleSystem>();
        if (particleSystem != null)
        {
            StartCoroutine(StopAndDestroyParticle(particleSystem, particleInstance));
        }
    }

    private IEnumerator StopAndDestroyParticle(ParticleSystem particleSystem, GameObject particleObject)
    {
        particleSystem.Play();
        yield return new WaitForSeconds(particleDuration);
        particleSystem.Stop();
        yield return new WaitForSeconds(particleSystem.main.startLifetime.constantMax);
        Destroy(particleObject);
    }
}