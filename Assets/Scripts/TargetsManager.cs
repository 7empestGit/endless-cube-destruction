using UnityEngine;

public class TargetsManager : MonoBehaviour
{
  [Header ("Links")]
  [SerializeField] private TargetController[] targetControllers;
  [SerializeField] private Transform spawnAreaTransform;


  void Start ()
  {
    SetFloorTransforms ();
  }

  private void SetFloorTransforms ()
  {
    for (int i = 0; i < targetControllers.Length; i++)
    {
      targetControllers[i].SetFloorTransform (spawnAreaTransform);
    }
  }
}
