using Niantic.ARDK.Utilities;
using UnityEngine;

public class ARProjectileManager : MonoBehaviour
{
    [SerializeField]
    private Camera arCamera;

    [SerializeField]
    private GameObject[] projectilesPrefabs;

    [SerializeField]
    private float force = 200.0f;

    void Update()
    {
        if (PlatformAgnosticInput.touchCount <= 0) return;

        var touch = PlatformAgnosticInput.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            LaunchRandomProjectile();
        }
    }

    void LaunchRandomProjectile()
    {
        var prefab = projectilesPrefabs[Random.Range(0, projectilesPrefabs.Length)];
        var projectile = Instantiate(prefab, arCamera.transform.position, Quaternion.identity);

        var projectileRigidBody = projectile.GetComponent<Rigidbody>();
        projectileRigidBody.AddForce(arCamera.transform.forward * force);
    }
}
