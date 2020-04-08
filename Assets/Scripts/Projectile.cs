using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
  public bool isAOE = false;
  public GameObject aoePrefab;
  Rigidbody2D rigidbody2d;
  Vector2 target;
  float originalDistance;
  // Start is called before the first frame update
  void Awake()
  {
    // target = 0;
    rigidbody2d = GetComponent<Rigidbody2D>();
  }

  // Update is called once per frame
  void Update()
  {
    if (isAOE)
    {
      transform.Rotate(Vector3.forward, 360 * Time.deltaTime);

      float distanceToTarget = Vector2.Distance(transform.position, target);
      if (originalDistance / 2 < distanceToTarget)
      {
        transform.localScale = transform.localScale + new Vector3(0.02f, 0.02f, 0);
      }
      else
      {
        transform.localScale = transform.localScale - new Vector3(0.02f, 0.02f, 0);
      }

      if (distanceToTarget < 0.1f && distanceToTarget > 0)
      {
        SpawnAOE();
        Destroy(gameObject);
      }
    }

    if (transform.position.magnitude > 1000.0f)
    {
      Destroy(gameObject);
    }
  }

  public void Launch(Vector2 direction, float force)
  {
    rigidbody2d.AddForce(direction * force);
  }

  public void Launch(Vector2 direction, float force, Vector2 targetPosition)
  {
    target = targetPosition;
    originalDistance = Vector2.Distance(transform.position, target);
    rigidbody2d.AddForce(direction * force);
  }

  void OnCollisionEnter2D(Collision2D other)
  {
    EnemyController e = other.collider.GetComponent<EnemyController>();

    if (e != null)
    {
      e.ChangeHealth(-1);
    }

    Destroy(gameObject);
  }

  void SpawnAOE()
  {
    GameObject playerAOE = Instantiate(aoePrefab, transform.position, Quaternion.identity);
  }
}
