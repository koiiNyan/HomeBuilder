using UnityEngine;
using System.Collections;

public class ParticleManager : MonoBehaviour
{
    public GameObject particlePrefab;
    public float particleObjDuration = 2f;

    public GameObject particlePrefabFinal;
    public float particleFinDuration = 2f;

    public void PlayParticleAtPosition(Vector3 localPosition, Transform parent, Vector3 scale)
    {
        GameObject particleInstance = Instantiate(particlePrefab, parent);
        particleInstance.transform.localPosition = localPosition;
        particleInstance.transform.localScale = scale;

        ParticleSystem particleSystem = particleInstance.GetComponent<ParticleSystem>();
        if (particleSystem != null)
        {
            StartCoroutine(StopAndDestroyParticle(particleSystem, particleInstance, particleObjDuration));
        }
    }

    private IEnumerator StopAndDestroyParticle(ParticleSystem particleSystem, GameObject particleObject, float particleDuration)
    {
        particleSystem.Play();
        yield return new WaitForSeconds(particleDuration);
        particleSystem.Stop();
        yield return new WaitForSeconds(particleSystem.main.startLifetime.constantMax);
        Destroy(particleObject);
    }


    public void PlayFinalParticle()
    {
        GameObject particleInstance = Instantiate(particlePrefabFinal);
        //particleInstance.transform.localPosition = new Vector3(0f, 10.72f, 0f);
        //particleInstance.transform.localScale = new Vector3(1f, 1f, 1f);
        //particleInstance.transform.localRotation = Quaternion.Euler(0f, -45f, 0f);

        ParticleSystem particleSystem = particleInstance.GetComponent<ParticleSystem>();
        if (particleSystem != null)
        {
            StartCoroutine(StopAndDestroyParticle(particleSystem, particleInstance, particleFinDuration));
        }
    }
}