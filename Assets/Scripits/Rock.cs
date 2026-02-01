using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CinemachineImpulseSource))]
public class Rock : MonoBehaviour
{
    [SerializeField] private ParticleSystem collisionParticleSystem;
    [SerializeField] private AudioSource boullderSmeshAudioSource;
    [SerializeField] private float collistionCooldown = 1f;
    private float collisionTimer = 1f;
    [SerializeField] private float shakeModifer = 10f;
    CinemachineImpulseSource cinemachineImpulseSource;

    private void Awake()
    {
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();

    }
    private void Update()
    {

        collisionTimer += Time.deltaTime;

    }

    void OnCollisionEnter(Collision other)
    {

        if (collisionTimer < collistionCooldown) return;

        FireImpulse();
        CollisionFX(other);

        collisionTimer = 0f;
    }

    private void FireImpulse()
    {
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        float shakeIntensity = (1f / distance) * this.shakeModifer;
        float minShakeIntensity = Mathf.Min(shakeIntensity, 1f);
        cinemachineImpulseSource.GenerateImpulse(minShakeIntensity);
    }

    private void CollisionFX(Collision other)
    {
        ContactPoint contactPoint = other.contacts[0];
        collisionParticleSystem.transform.position = contactPoint.point;
        collisionParticleSystem.Play();
        boullderSmeshAudioSource.Play();
    }
}
