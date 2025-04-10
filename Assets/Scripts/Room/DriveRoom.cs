using UnityEngine;

public class DriveRoom : RoomSystem, IRoomAction
{
    [SerializeField] float missChance = 10;
    [SerializeField] float missModifier = 10;
    protected override void Init()
    {
        base.Init();
        RoomManager.Instance.AddRoom(this);
    }
    public void RoomAction()
    {
        if (!onFire)
        {
            TrailerManager.Instance.SetMissChance(missChance + missModifier * crewList.Count);
        } else
        {
            TrailerManager.Instance.SetMissChance(missChance);
        }
        
    }
}
