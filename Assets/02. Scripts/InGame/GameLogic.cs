using UnityEngine;

public abstract class GameLogic
{
    public abstract void OnChangeGameMode(GameManager manager);
    public abstract void OnGameReset();

    public abstract int GetMaxHealth();
    public abstract float GetTimePerDamage();
    public abstract string GetBestScoreDataKey();
}
