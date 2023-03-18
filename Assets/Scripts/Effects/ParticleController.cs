using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] private ParticleSystem _scoreParticles;
    [SerializeField] private float _minInheritedVelocity = -1f;
    [SerializeField] private float _maxInheritedVelocity = 1f;
    void Start()
    {
        PlayerBehaviour.ScoreCollected += spawnScoreParticles;
    }

    void Update()
    {
        
    }

    private void spawnScoreParticles(PlayerCollisionData playerData)
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

}
