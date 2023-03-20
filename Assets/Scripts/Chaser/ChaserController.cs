public class ChaserController : BallController
{
    private ChaserBehaviour _chaserBehaviour;

    protected override void Awake()
    {
        base.Awake();
        
        _chaserBehaviour = gameObject.GetComponent<ChaserBehaviour>();
    }

    protected override void Update() 
    {   
        base.Update();
    }

    protected override BallStates GetState() => _chaserBehaviour.getBallState();

    protected override void SetState(BallStates state) => 
        _chaserBehaviour.setBallState(state);
}
