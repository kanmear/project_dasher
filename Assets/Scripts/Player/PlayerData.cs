using UnityEngine;

public struct PlayerCollisionData
{
    private GameObject _player;
    private Collider2D _collider;
    private int _bounceCount;
    private int _ricochetCount;

    public PlayerCollisionData(GameObject player, Collider2D collider, int bounceCount, int ricochetCount)
    {
        _player = player;
        _collider = collider;
        _bounceCount = bounceCount;
        _ricochetCount = ricochetCount;    
    }

    public GameObject getPlayer() => _player;
    public Collider2D getCollider() => _collider;
    public int getBounceCount() => _bounceCount;
    public int getRicochetCount() => _ricochetCount;
}