using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
  [Header ("Links")]
  [SerializeField] private TMP_Text timerText;
  [SerializeField] private List<GameObject> bombImagesList;
  [SerializeField] private TMP_Text killedEnemiesAmountText;

  public void UpdateTimer (float timerInSeconds)
  {
    timerText.text = timerInSeconds > 0 ? timerInSeconds.ToString () : "-";
  }

  public void UpdateBombImages (int amountOfAvailableBombs)
  {
    for (int i = 0; i < bombImagesList.Count; i++)
    {
      bombImagesList[i].SetActive (i < amountOfAvailableBombs);
    }
  }

  public void UpdateKilledEnemies (int value)
  {
    killedEnemiesAmountText.text = value.ToString ();
  }
}
