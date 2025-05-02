using UnityEngine;

public abstract class State : ScriptableObject
{
    public abstract void Enter(Enemy enemy);
    public abstract void Tick(Enemy enemy); // Renamed from Update
    public abstract void Exit(Enemy enemy);
}
