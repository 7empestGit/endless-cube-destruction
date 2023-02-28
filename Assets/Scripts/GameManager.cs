using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  [Header ("Links")]
  [SerializeField] private UIManager UIManager;

  public int KilledEnemiesAmount;
  public float GenerateBombTimer { get { return GenerateBombTimer; } set { UIManager.UpdateTimer (value); } }

  public int CurrentBombsAmount { get { return CurrentBombsAmount; } set { UIManager.UpdateBombImages (value); } }

  #region Singleton

  public static GameManager Instance;

  void Awake ()
  {
    Instance = this;
  }

  #endregion

  public void EnemyKilled ()
  {
    KilledEnemiesAmount++;
    UIManager.UpdateKilledEnemies (KilledEnemiesAmount);
  }
}
