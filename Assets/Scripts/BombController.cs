using UnityEngine;

public class BombController : MonoBehaviour
{
  [Header ("Parameters")]
  [SerializeField] private float explosionRadius;

  [Header ("Links")]
  [SerializeField] private MeshRenderer mRenderer;
  [SerializeField] private SphereCollider sCollider;

  public void FinishLanding ()
  {
    SimulateExplosion ();
  }

  private void SimulateExplosion ()
  {
    mRenderer.enabled = false;

    sCollider.enabled = true;
    sCollider.radius = explosionRadius;

    Destroy (gameObject, 0.5f);
  }
}
