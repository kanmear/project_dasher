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

        HandleHoverParticles();
    }

    private void HandleHoverParticles()
    {
        if (_hoverInput && _hoverParticleSys.isStopped)
            _hoverParticleSys.Play();
        else if (!_hoverInput && _hoverParticleSys.isPlaying)
            _hoverParticleSys.Stop();
    }

    override protected BallStates GetState() => _playerBehaviour.getBallState();
    protected override void SetState(BallStates state) => _playerBehaviour.setBallState(state);
    protected override void SetRicochetCount(int count) => _playerBehaviour.setRicochetCount(count);
}
