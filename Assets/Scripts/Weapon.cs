using UnityEngine;

public class Weapon : MonoBehaviour
{
  public float attackDuration = 1.0f;
  float timer = 0;

  BoxCollider2D weaponCollider;

  void Awake()
  {
    weaponCollider = GetComponent<BoxCollider2D>();
  }

  void Update()
  {
    if (timer >= 0)
    {
      timer -= Time.deltaTime;
    }
    else if (weaponCollider.enabled)
    {
      weaponCollider.enabled = false;
    }
  }
  void OnTriggerStay2D(Collider2D other)
  {
    EnemyController e = other.GetComponent<EnemyController>();

    if (e != null)
    {
      e.ChangeHealth(-1);
    }
  }

  public void Attack()
  {
    weaponCollider.enabled = true;
    timer = attackDuration;
  }
}
