using UnityEngine;

public struct PlayerTriggerData
{
    private GameObject _player;
    private Collider2D _collider;
    private int _bounceCount;
    private int _ricochetCount;

    public PlayerTriggerData(GameObject player, Collider2D collider, int bounceCount, int ricochetCount)
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

public struct PlayerCollisionData
{
    private GameObject _player;
    private Collision2D _collision;
    private int _bounceCount;
    private int _ricochetCount;

    public PlayerCollisionData(GameObject player, Collision2D collision, int bounceCount, int ricochetCount)
    {
        _player = player;
        _collision = collision;
        _bounceCount = bounceCount;
        _ricochetCount = ricochetCount;    
    }

    public PlayerCollisionData(Collision2D collision, int bounceCount, int ricochetCount)
    {
        _player = null;
        _collision = collision;
        _bounceCount = bounceCount;
        _ricochetCount = ricochetCount;    
    }

    public GameObject getPlayer() => _player;
    public Collision2D getCollision() => _collision;
    public int getBounceCount() => _bounceCount;
    public int getRicochetCount() => _ricochetCount;
}