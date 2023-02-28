using UnityEngine;

public class GameManager : MonoBehaviour
{
  [Header ("Links")]
  [SerializeField] private TargetController[] targetControllers;
  [SerializeField] private Transform spawnAreaTransform;


  void Start ()
  {
    SetFloorTransforms ();
  }

  void Update ()
  {

  }

  private void SetFloorTransforms ()
  {
    for (int i = 0; i < targetControllers.Length; i++)
    {
      targetControllers[i].SetFloorTransform (spawnAreaTransform);
    }
  }
}
