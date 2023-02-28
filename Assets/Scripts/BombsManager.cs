using UnityEngine;
using System.Collections;

public class BombsManager : MonoBehaviour
{
  [Header ("Links")]
  [SerializeField] private GameObject bombPrefab;

  public float firingAngle = 45.0f;

  private const float GRAVITATIONAL_ACCELERATION = 9.81f;


  void Update ()
  {
    if (Input.GetMouseButtonDown (0))
    {
      Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

      if (Physics.Raycast (ray, out RaycastHit hit))
      {
        Vector3 hitPoint = hit.point;
        Debug.Log ("hitPoint: " + hitPoint);
        StartCoroutine (SimulateProjectile (hitPoint));
      }
    }
  }

  private IEnumerator SimulateProjectile (Vector3 targetPosition)
  {
    Transform projectile = Instantiate (bombPrefab).transform;
    projectile.transform.SetPositionAndRotation (Camera.main.transform.position, Camera.main.transform.rotation);

    float target_Distance = Vector3.Distance (projectile.position, targetPosition);

    float projectile_Velocity = target_Distance / (Mathf.Sin (2 * firingAngle * Mathf.Deg2Rad) / GRAVITATIONAL_ACCELERATION);

    float Vx = Mathf.Sqrt (projectile_Velocity) * Mathf.Cos (firingAngle * Mathf.Deg2Rad);
    float Vy = Mathf.Sqrt (projectile_Velocity) * Mathf.Sin (firingAngle * Mathf.Deg2Rad);

    float flightDuration = target_Distance / Vx;

    // Rotate projectile to face the target.
    projectile.rotation = Quaternion.LookRotation (targetPosition - projectile.position);

    float elapse_time = 0;

    while (elapse_time < flightDuration)
    {
      projectile.Translate (0, (Vy - (GRAVITATIONAL_ACCELERATION * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

      elapse_time += Time.deltaTime;

      yield return null;
    }
  }
}
