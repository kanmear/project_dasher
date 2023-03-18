using System;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private PlayerController _playerController;
    private PlayerStates _playerState;
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
            _playerState = PlayerStates.GROUNDED;
            _bounceCount = 0;
            _ricochetCount = 0;
        }
        else
        {
            _bounceCount++;

            if (_playerState == PlayerStates.DASHING)
                _ricochetCount = 1;
            else if (_playerState == PlayerStates.RICOCHETING)
                _ricochetCount++;
        }
        
        WallHit?.Invoke(new PlayerCollisionData(collision2D, _bounceCount, _ricochetCount));
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        ScoreCollected?.Invoke(new PlayerTriggerData(
            gameObject, collider, _bounceCount, _ricochetCount));
    }


    void OnCollisionExit2D(Collision2D collision2D) => _playerState = PlayerStates.RICOCHETING;

    public PlayerStates getPlayerState() => _playerState;
    public void setPlayerState(PlayerStates state) => _playerState = state;

    public void setRicochetCount(int count) => _ricochetCount = count;
}
