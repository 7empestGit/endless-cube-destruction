using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
  private Transform floorTransform;

  #region Unity Methods

  void OnTriggerEnter (Collider other)
  {
    if (other.CompareTag ("Bomb"))
      GotDamaged ();
  }

  #endregion

  #region Private Methods

  private IEnumerator GetPositionAndMove ()
  {
    int randDelay = Random.Range (3, 7);
    yield return new WaitForSeconds (randDelay);

    StartCoroutine (MoveTo (GetRandomPosition (), 2f));
  }

  private Vector3 GetRandomPosition ()
  {
    float randomX = Random.Range (floorTransform.localScale.x / 2, -floorTransform.localScale.x / 2);
    float randomZ = Random.Range (floorTransform.localScale.z / 2, -floorTransform.localScale.z / 2);

    float finalX = randomX > 0 ? randomX - transform.localScale.x / 2 : randomX + transform.localScale.x / 2;
    float finalZ = randomZ > 0 ? randomZ - transform.localScale.z / 2 : randomZ + transform.localScale.z / 2;

    return new Vector3 (finalX, 1, finalZ);
  }

  private IEnumerator MoveTo (Vector3 destinationPos, float duration)
  {
    float time = 0;
    Vector3 startPosition = transform.position;
    while (time < duration)
    {
      transform.position = Vector3.Lerp (startPosition, destinationPos, time / duration);
      time += Time.deltaTime;
      yield return null;
    }
    transform.position = destinationPos;

    StartCoroutine (GetPositionAndMove ());
  }

  private IEnumerator Respawn ()
  {
    int randDelay = Random.Range (1, 4);
    yield return new WaitForSeconds (randDelay);

    StartCoroutine (GetPositionAndMove ());
  }

  #endregion


  #region Public Methods

  public void GotDamaged ()
  {
    StopAllCoroutines ();
    StartCoroutine (Respawn ());
  }

  public void SetFloorTransform (Transform transform)
  {
    floorTransform = transform;
  }

  #endregion
}
