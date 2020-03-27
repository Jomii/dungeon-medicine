using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
  public float speed = 3.0f;
  public ParticleSystem smokeEffect;
  public float attackTime = 1.0f;
  // Ranged behaviour
  public bool ranged = false;
  public GameObject projectilePrefab;
  public Transform[] moveSpots;
  public float moveWaitTime;
  public int projectileSpeed = 400;

  Rigidbody2D rigidbody2d;
  Transform target;
  Vector2 directionToTarget;
  bool broken = true;
  float attackTimer = 0.0f;
  // Ranged behaviour
  int randomSpot;
  float waitTime;

  Animator animator;
  // Start is called before the first frame update
  void Start()
  {
    rigidbody2d = GetComponent<Rigidbody2D>();
    target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    animator = GetComponent<Animator>();

    if (ranged)
    {
      waitTime = moveWaitTime;
      randomSpot = Random.Range(0, moveSpots.Length);
    }
  }

  // Update is called once per frame
  void Update()
  {
    if (!broken)
    {
      return;
    }

    if (!ranged)
    {
      MeleeBehaviour();
    }
    else
    {
      RangedBehaviour();
    }
  }

  void MeleeBehaviour()
  {
    Vector2 position = rigidbody2d.position;
    directionToTarget = new Vector2(target.position.x - position.x, target.position.y - position.y);

    if (Vector2.Distance(position, target.position) > 1.0f)
    {
      directionToTarget.Normalize();
      transform.LookAt(Camera.main.ScreenToWorldPoint(target.position), Vector3.forward);
      position = position + directionToTarget * speed * Time.deltaTime;
      rigidbody2d.MovePosition(position);

      animator.SetFloat("Move X", directionToTarget.x);
      animator.SetFloat("Move Y", directionToTarget.y);
    }
  }

  void RangedBehaviour()
  {
    Vector2 position = rigidbody2d.position;
    directionToTarget = new Vector2(moveSpots[randomSpot].position.x - position.x, moveSpots[randomSpot].position.y - position.y);

    if (Vector2.Distance(position, target.position) < 5.0f)
    {
      Shoot();
    }

    if (Vector2.Distance(position, moveSpots[randomSpot].position) > 0.2f)
    {
      directionToTarget.Normalize();
      position = position + directionToTarget * speed * Time.deltaTime;
      rigidbody2d.MovePosition(position);

      animator.SetFloat("Move X", directionToTarget.x);
      animator.SetFloat("Move Y", directionToTarget.y);
    }

    if (!Mathf.Approximately(directionToTarget.x, 0.2f) || !Mathf.Approximately(directionToTarget.y, 0.2f))
    {
      if (waitTime <= 0)
      {
        randomSpot = Random.Range(0, moveSpots.Length);
        waitTime = moveWaitTime;
      }
      else
      {
        waitTime -= Time.deltaTime;
      }
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

  void Shoot()
  {
    if (attackTimer < 0)
    {
      attackTimer = attackTime;
      Vector2 aimDirection = (Vector2)target.position - rigidbody2d.position;
      aimDirection.Normalize();

      GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position, Quaternion.identity);
      EnemyProjectile projectile = projectileObject.GetComponent<EnemyProjectile>();
      projectile.Launch(aimDirection, projectileSpeed);
    }
    else
    {
      attackTimer -= Time.deltaTime;
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
