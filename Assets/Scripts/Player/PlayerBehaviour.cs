using System;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private PlayerController _playerController;
    private BallStates _playerState;
    private int _bounceCount = 0;
    private int _ricochetCount = 0;
    public static event Action<PlayerTriggerData> ScoreCollected;
    public static event Action<PlayerCollisionData> WallHit;

    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.GetContact(0).normal.Equals(Vector3.up))
        {
            _playerState = BallStates.GROUNDED;
            _bounceCount = 0;
            _ricochetCount = 0;
        }
        else
        {
            _bounceCount++;

            if (_playerState == BallStates.DASHING)
                _ricochetCount = 1;
            else if (_playerState == BallStates.RICOCHETING)
                _ricochetCount++;
        }
        
        WallHit?.Invoke(new PlayerCollisionData(collision2D, _bounceCount, _ricochetCount));
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        ScoreCollected?.Invoke(new PlayerTriggerData(
            gameObject, collider, _bounceCount, _ricochetCount));
    }


    void OnCollisionExit2D(Collision2D collision2D) => _playerState = BallStates.RICOCHETING;

    public BallStates getPlayerState() => _playerState;
    public void setPlayerState(BallStates state) => _playerState = state;

    public void setRicochetCount(int count) => _ricochetCount = count;
}
