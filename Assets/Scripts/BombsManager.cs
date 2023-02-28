using UnityEngine;
using System.Collections;

public class BombsManager : MonoBehaviour
{
  [Header ("Links")]
  [SerializeField] private GameObject bombPrefab;

  private const float FIRING_ANGLE = 45.0f;
  private const float GRAVITATIONAL_ACCELERATION = 9.81f;
  private const int MAX_AMOUNT_OF_BOMBS = 5;

  private const float GENERATE_BOMB_DELAY = 1.0f;
  private const float BOMB_SHOOT_COOLDOWN_DELAY = 0.5f;

  private float cooldownTimer;
  private float generateBombTimer;
  private int currentBombsAmount;

  void Start ()
  {
    currentBombsAmount = 5;
  }

  void Update ()
  {
    HandleTimers ();
    HandleRaycasting ();
  }
  
  private void HandleTimers ()
  {
    if (cooldownTimer > 0)
    {
      cooldownTimer -= Time.deltaTime;
    }

    if (generateBombTimer > 0)
    {
      generateBombTimer -= Time.deltaTime;
    }
    else if (currentBombsAmount < MAX_AMOUNT_OF_BOMBS)
    {
      currentBombsAmount++;
      UpdateValues ();
    }
  }

  private void HandleRaycasting ()
  {
    if (CanThrowBomb ())
    {
      Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

      if (Physics.Raycast (ray, out RaycastHit hit))
      {
        Vector3 hitPoint = hit.point;
        StartCoroutine (SimulateProjectile (hitPoint));
      }

      currentBombsAmount--;
      cooldownTimer = BOMB_SHOOT_COOLDOWN_DELAY;
      generateBombTimer = GENERATE_BOMB_DELAY;
      UpdateValues ();
    }
  }

  private bool CanThrowBomb ()
  {
    return Input.GetMouseButtonDown (0) && cooldownTimer <= 0 && currentBombsAmount > 0;
  }

  private void UpdateValues ()
  {
    GameManager.Instance.GenerateBombTimer = generateBombTimer;
    GameManager.Instance.CurrentBombsAmount = currentBombsAmount;
  }

  private IEnumerator SimulateProjectile (Vector3 targetPosition)
  {
    Transform projectile = Instantiate (bombPrefab).transform;
    projectile.transform.SetPositionAndRotation (Camera.main.transform.position, Camera.main.transform.rotation);

    float target_Distance = Vector3.Distance (projectile.position, targetPosition);

    float projectile_Velocity = target_Distance / (Mathf.Sin (2 * FIRING_ANGLE * Mathf.Deg2Rad) / GRAVITATIONAL_ACCELERATION);

    float Vx = Mathf.Sqrt (projectile_Velocity) * Mathf.Cos (FIRING_ANGLE * Mathf.Deg2Rad);
    float Vy = Mathf.Sqrt (projectile_Velocity) * Mathf.Sin (FIRING_ANGLE * Mathf.Deg2Rad);

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

    projectile.gameObject.GetComponent<BombController> ().FinishLanding ();
  }
}
