using UnityEngine;

abstract public class BallBehaviour : MonoBehaviour
{
    protected BallStates _ballState;

    protected abstract void OnCollisionEnter2D(Collision2D collision2D);

    void OnCollisionExit2D(Collision2D collision2D) => _ballState = BallStates.RICOCHETING;

    public BallStates getPlayerState() => _ballState;
    public void setPlayerState(BallStates state) => _ballState = state;
}
