using System;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] private ParticleSystem _scoreParticles;
    [SerializeField] private float _minInheritedVelocity = -1f;
    [SerializeField] private float _maxInheritedVelocity = 1f;
    [SerializeField] private ParticleSystem _collisionParticles;

    void Start()
    {
        PlayerBehaviour.ScoreCollected += SpawnScoreParticles;
        PlayerBehaviour.WallHit += SpawnWallHitParticles;
    }

    private void SpawnScoreParticles(PlayerTriggerData playerData)
    {
        var velocityOverLifetime = _scoreParticles.velocityOverLifetime;
        velocityOverLifetime.enabled = true;
        velocityOverLifetime.space = ParticleSystemSimulationSpace.Local;
        
        Rigidbody2D playerRB = playerData.getPlayer().GetComponent<Rigidbody2D>();
        float velocityX = Mathf.Clamp(playerRB.velocity.x / 10f, 
            _minInheritedVelocity, _maxInheritedVelocity);
        float velocityY = Mathf.Clamp(playerRB.velocity.y / 10f, 
            _minInheritedVelocity, _maxInheritedVelocity);

        Debug.Log("Transferred velocity x: " + velocityX);
        Debug.Log("Transferred velocity y: " + velocityY);
        velocityOverLifetime.x = velocityX;
        velocityOverLifetime.z = velocityY;

        GameObject.Instantiate(
            _scoreParticles, playerData.getCollider().transform.position, 
            _scoreParticles.gameObject.transform.rotation);
    }

    private void SpawnWallHitParticles(PlayerCollisionData playerCollisionData)
    {   
        Collision2D collision2D = playerCollisionData.getCollision();
        ContactPoint2D contactPoint = collision2D.GetContact(0);
        Vector2 localPosition = contactPoint.point;
        Vector2 contactNormal = contactPoint.normal;
        Vector2 worldPosition = collision2D.transform.TransformPoint(localPosition);
        Quaternion rotation = Quaternion.FromToRotation(Vector2.up, contactNormal);
        GameObject.Instantiate(
            _collisionParticles, worldPosition, rotation);

        Quaternion mirroredRotation = new Quaternion(-rotation.x, -rotation.y, rotation.z, rotation.w);
        mirroredRotation *= Quaternion.AngleAxis(180, Vector3.up);
        GameObject.Instantiate(
            _collisionParticles, worldPosition, mirroredRotation);
    }
}
