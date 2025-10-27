using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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

        /*string lvlName = SceneManager.GetActiveScene().name;
        if (lvlName == "Level01") particleInstance.transform.position = new Vector3(-1.4f, 6f, -2.07f);

        Vector3 position = new Vector3(0f, 0f, 0f);

        if (lvlName == "Level01") position = new Vector3(-1.4f, 19f, -2.07f);
        StartCoroutine(MoveParticle(particleInstance, position));*/

        ParticleSystem particleSystem = particleInstance.GetComponent<ParticleSystem>();
        if (particleSystem != null)
        {
            StartCoroutine(StopAndDestroyParticle(particleSystem, particleInstance, particleFinDuration));
        }
    }

/*
    private IEnumerator MoveParticle(GameObject particleInstance, Vector3 position)
    {
        Vector3 startPosition = particleInstance.transform.position;
        float journeyLength = Vector3.Distance(startPosition, position);
        float startTime = Time.time;

        while (particleInstance.transform.position != position)
        {
            float distanceCovered = (Time.time - startTime) * 3f;
            float fractionOfJourney = distanceCovered / journeyLength;
            particleInstance.transform.position = Vector3.Lerp(startPosition, position, fractionOfJourney);


            yield return null;
        }

    } */
}