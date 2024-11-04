using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SinkController : MonoBehaviour, ILoadable
{
    public Material[] newMaterials;
    public Vector3 particlePosition = new Vector3(0f, 0f, 0f); // Position to play the particle effect
    public Vector3 particleScale = new Vector3(0.004f, 0.004f, 0.004f); // Scale of the particle effect

    private ParticleManager particleManager;

    void Start()
    {
        particleManager = FindObjectOfType<ParticleManager>();
    }

    public void OnAllPartsPlaced()
    {
        ChangeMaterials();
        DestroyUIParts();
        PlayParticleEffect();
        SaveManager.SaveGame(SceneManager.GetActiveScene().name);
    }

    void ChangeMaterials()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.materials = newMaterials;
    }

    void DestroyUIParts()
    {
        GameObject[] uiParts = GameObject.FindGameObjectsWithTag("BedUI");
        foreach (GameObject part in uiParts)
        {
            Destroy(part);
        }
    }

    void PlayParticleEffect()
    {
        if (particleManager != null)
        {
            particleManager.PlayParticleAtPosition(particlePosition, transform, particleScale);
        }
    }

    public void OnLoad()
    {
        ChangeMaterials();
        DestroyUIParts();
    }
}