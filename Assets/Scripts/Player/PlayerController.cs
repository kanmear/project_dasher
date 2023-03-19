using UnityEngine;

public class PlayerController : BallController
{
    [SerializeField] private GameObject _hoverParticleObj;
    private ParticleSystem _hoverParticleSys;

    private PlayerBehaviour _playerBehaviour;

    protected override void Awake()
    {
        base.Awake();

        _playerBehaviour = gameObject.GetComponent<PlayerBehaviour>();
        _hoverParticleSys = _hoverParticleObj.GetComponent<ParticleSystem>();
    }

    protected override void Update() 
    {   
        base.Update();

        UpdatePlayerData();
        HandleHoverParticles();
    }

    private void HandleHoverParticles()
    {
        if (_hoverInput && _hoverParticleSys.isStopped)
            _hoverParticleSys.Play();
        else if (!_hoverInput && _hoverParticleSys.isPlaying)
            _hoverParticleSys.Stop();
    }

    private void UpdatePlayerData()
    {
        if (_dashInput != null)
        {
            _playerBehaviour.setPlayerState(
                BallStates.DASHING
            );
        }
        else if (_hoverInput)
        {
            _playerBehaviour.setRicochetCount(0);
            _playerBehaviour.setPlayerState(
                BallStates.HOVERING
            );
        }
    }

    override protected BallStates GetState() => _playerBehaviour.getPlayerState();
}
