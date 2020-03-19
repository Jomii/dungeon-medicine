using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
  public float speed = 3.0f;
  public ParticleSystem smokeEffect;

  Rigidbody2D rigidbody2d;
  Transform target;
  bool broken = true;

  Animator animator;
  // Start is called before the first frame update
  void Start()
  {
    rigidbody2d = GetComponent<Rigidbody2D>();
    target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    animator = GetComponent<Animator>();
  }

  // Update is called once per frame
  void Update()
  {
    if (!broken)
    {
      return;
    }

    // Debug.Log("Target position: " + target.position);
    Vector2 position = rigidbody2d.position;
    Vector2 directionToTarget = new Vector2(target.position.x - position.x, target.position.y - position.y);

    if (Vector2.Distance(position, target.position) > 1.0f)
    {
      directionToTarget.Normalize();
      position = position + directionToTarget * speed * Time.deltaTime;
      rigidbody2d.MovePosition(position);

      animator.SetFloat("Move X", directionToTarget.x);
      animator.SetFloat("Move Y", directionToTarget.y);
    }
  }

  void OnCollisionEnter2D(Collision2D other)
  {
    PlayerController player = other.gameObject.GetComponent<PlayerController>();

    if (player != null)
    {
      player.ChangeHealth(-1);
    }
  }

  public void Fix()
  {
    broken = false;
    rigidbody2d.simulated = false;
    animator.SetTrigger("Fixed");
    smokeEffect.Stop();
  }
}
