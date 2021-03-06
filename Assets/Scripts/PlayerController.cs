﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
  public float moveSpeed = 6.0f;
  public float dashSpeed = 15.0f;
  public float dashDuration = 0.25f;
  public float dashCooldown = 4.0f;

  public int maxHealth = 5;
  public float timeInvincible = 2.0f;
  public GameObject projectilePrefab;
  public AudioClip hitClip;
  public AudioClip throwSound;
  public AudioClip meleeSound;
  public AudioClip dashSound;
  public AudioClip deathSound;
  public float attackRange = 0.19f;
  public float attackSpeed = 0.0f;

  public int health { get { return currentHealth; } }
  float currentSpeed;
  int currentHealth;
  bool isInvincible;
  float invincibleTimer;
  bool isDashing;
  float dashTimer;
  float attackTimer;
  float speedTimer;
  bool isDead;

  AudioSource audioSource;

  Rigidbody2D rigidbody2d;
  Transform weapon;

  Animator animator;
  Animator weaponAnimator;
  Vector2 lookDirection = new Vector2(1, 0);
  Vector2 aimDirection = new Vector2(1, 0);

  Inventory inventory;
  // Start is called before the first frame update
  void Start()
  {
    LevelExit.instance.playerSpawnPos = transform.position;

    rigidbody2d = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    weapon = transform.Find("Weapon");
    weaponAnimator = weapon.GetComponent<Animator>();

    currentHealth = GameState.instance.health;
    currentSpeed = moveSpeed;
    UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);

    audioSource = GetComponent<AudioSource>();

    inventory = Inventory.instance;
    inventory.onItemChangedCallback += UIInventory.instance.UpdateUI;
  }

  // Update is called once per frame
  void Update()
  {
    if (PauseMenu.GameIsPaused || isDead)
    {
      return;
    }

    float horizontal = Input.GetAxis("Horizontal");
    float vertical = Input.GetAxis("Vertical");

    Vector3 crosshairInWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    aimDirection = (Vector2)crosshairInWorldPos - rigidbody2d.position;
    aimDirection.Normalize();

    Vector2 move = new Vector2(horizontal, vertical);

    if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
    {
      lookDirection.Set(move.x, move.y);
      lookDirection.Normalize();
    }

    animator.SetFloat("Look X", lookDirection.x);
    animator.SetFloat("Look Y", lookDirection.y);
    animator.SetFloat("Speed", move.magnitude);

    crosshairInWorldPos.z = -90;
    transform.LookAt(crosshairInWorldPos, Vector3.forward);
    Vector2 position = rigidbody2d.position;
    position = position + move * currentSpeed * Time.deltaTime;
    rigidbody2d.MovePosition(position);

    if (isInvincible)
    {
      invincibleTimer -= Time.deltaTime;

      if (invincibleTimer < 0)
      {
        isInvincible = false;
      }
    }

    if (isDashing)
    {
      UIDash.instance.SetCooldown(1.0f - dashTimer / dashCooldown);
      dashTimer -= Time.deltaTime;

      if (dashTimer < 0)
      {
        isDashing = false;
      }
    }

    // Speed potion timer and normalize currentSpeed
    if (speedTimer >= 0)
    {
      speedTimer -= Time.deltaTime;
    }
    else if (!isDashing)
    {
      currentSpeed = moveSpeed;
    }

    if (attackTimer <= 0)
    {
      if (Input.GetButtonDown("Fire1"))
      {
        Melee();
      }

      if (Input.GetButtonDown("Fire2"))
      {
        Launch();
      }
    }
    else
    {
      attackTimer -= Time.deltaTime;
    }

    if (Input.GetKeyDown(KeyCode.F))
    {
      inventory.DropSelectedItem(rigidbody2d.position, aimDirection);
    }

    if (speedTimer <= 0 && !UICrafting.instance.enabled && !isDashing && Input.GetKeyDown(KeyCode.Space))
    {
      StartCoroutine(Dash());
    }

  }

  public void ChangeHealth(int amount)
  {
    if (amount < 0)
    {

      if (isInvincible)
      {
        return;
      }

      isInvincible = true;
      invincibleTimer = timeInvincible;
      animator.SetTrigger("Hit");
      PlaySound(hitClip);
    }

    currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);

    if (currentHealth <= 0)
    {
      Die();
    }
  }

  public void SetSpeed(float speed, float duration)
  {

    currentSpeed = speed;
    speedTimer = duration;
  }

  void Melee()
  {
    attackTimer = attackSpeed;
    weapon.GetComponent<Weapon>().Attack();

    weapon.LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);
    weapon.position = rigidbody2d.position + aimDirection;

    weaponAnimator.SetTrigger("Melee");
    PlaySound(meleeSound);
  }

  public void Equip(GameObject projectile)
  {
    projectilePrefab = projectile;
  }
  void Launch()
  {
    if (!projectilePrefab || inventory.rangedItem.Item1 == null)
    {
      return;
    }

    attackTimer = attackSpeed;

    GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position, Quaternion.identity);

    Projectile projectile = projectileObject.GetComponent<Projectile>();


    // Debug.Log("distance to cursor: " + distanceToTarget);
    if (projectile.isAOE)
    {
      projectile.Launch(aimDirection, 300, Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
    else
    {
      projectile.Launch(aimDirection, 300);
    }

    animator.SetTrigger("Launch");

    PlaySound(throwSound);
    inventory.UseRanged();
  }

  IEnumerator Dash()
  {
    isDashing = true;
    dashTimer = dashCooldown;

    PlaySound(dashSound);

    // Increase speed for the dashes duration
    currentSpeed = dashSpeed;
    yield return new WaitForSeconds(dashDuration);

    // Reset speed previous to dash
    currentSpeed = moveSpeed;
  }

  void Die()
  {
    isDead = true;
    rigidbody2d.simulated = false;
    StartCoroutine(ShowDeathScreen());
  }

  IEnumerator ShowDeathScreen()
  {
    UIDeathMask.instance.ShowDeathScreen();
    PlaySound(deathSound);

    yield return new WaitForSeconds(5);
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }

  public void PlaySound(AudioClip clip)
  {
    audioSource.PlayOneShot(clip);
  }

  public void Save()
  {
    GameState.instance.health = currentHealth;
    GameState.instance.inventoryItems = new List<(Item, int)>(inventory.items);
    GameState.instance.usedInventorySpace = inventory.usedSpace;
  }
}
