using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class EnemyIA : MonoBehaviour
{
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;
	public EnemyHealth enemyHealth;

	private bool facingRight = true;

	public float speed = 5f;

	public bool isInvincible = false;
	private bool isHitted = false;
	[SerializeField] private float rangeTargeting;

	[SerializeField] private float m_DashForce = 25f;
	private bool isDashing = false;

	public GameObject enemy;
	private float distToPlayer;
	private float distToPlayerY;
	public float meleeDist = 1.5f;
	public float rangeDist = 5f;
	private bool canAttack = true;
	private Transform attackCheck;
	public int damage = 1;

	public GameObject throwableObject;

	private float randomDecision = 0;
	private bool doOnceDecision = true;
	private bool endDecision = false;
	private Animator anim;


	void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		attackCheck = transform.Find("AttackCheck").transform;
		anim = GetComponent<Animator>();

	}

	// Update is called once per frame
	void FixedUpdate()
	{


		if (enemy != null)
		{
			if (isDashing)
			{
				m_Rigidbody2D.velocity = new Vector2(transform.localScale.x * m_DashForce, 0);
			}
			else if (!isHitted)
			{
				distToPlayer = enemy.transform.position.x - transform.position.x;
				distToPlayerY = enemy.transform.position.y - transform.position.y;

				if (Mathf.Abs(distToPlayer) < 0.25f)
				{
					GetComponent<Rigidbody2D>().velocity = new Vector2(0f, m_Rigidbody2D.velocity.y);
					anim.SetBool("IsWaiting", true);
				}
				else if (Mathf.Abs(distToPlayer) > 0.25f && Mathf.Abs(distToPlayer) < meleeDist && Mathf.Abs(distToPlayerY) < 2f)
				{
					GetComponent<Rigidbody2D>().velocity = new Vector2(0f, m_Rigidbody2D.velocity.y);
					if ((distToPlayer > 0f && transform.localScale.x < 0f) || (distToPlayer < 0f && transform.localScale.x > 0f))
						Flip();
					if (canAttack)
					{
						MeleeAttack();
					}
				}
				else if (Mathf.Abs(distToPlayer) > meleeDist && Mathf.Abs(distToPlayer) < rangeDist)
				{
					anim.SetBool("IsWaiting", false);
					m_Rigidbody2D.velocity = new Vector2(distToPlayer / Mathf.Abs(distToPlayer) * speed, m_Rigidbody2D.velocity.y);
				}
				else
				{
					if (!endDecision)
					{
						if ((distToPlayer > 0f && transform.localScale.x < 0f) || (distToPlayer < 0f && transform.localScale.x > 0f))
							Flip();

						if (randomDecision < 0.4f)
							Run();
						else if (randomDecision >= 0.4f && randomDecision < 0.6f)
							Jump();
						else if (randomDecision >= 0.6f && randomDecision < 0.8f)
							StartCoroutine(Dash());
						else
							Idle();
					}
					else
					{
						endDecision = false;
					}
				}
			}
			else if (isHitted)
			{
				if ((distToPlayer > 0f && transform.localScale.x > 0f) || (distToPlayer < 0f && transform.localScale.x < 0f))
				{
					Flip();
					StartCoroutine(Dash());
				}
				else
					StartCoroutine(Dash());
			}
		}
		else
		{
			Collider2D[] colliderArray = Physics2D.OverlapCircleAll(transform.position, rangeTargeting);
			foreach (Collider2D collider2D in colliderArray)
			{
				/*
				if (col
				 {

				}
				*/
			}
		}

		if (transform.localScale.x * m_Rigidbody2D.velocity.x > 0 && !m_FacingRight && enemyHealth.health > 0)
		{
			Flip();
		}
		else if (transform.localScale.x * m_Rigidbody2D.velocity.x < 0 && m_FacingRight && enemyHealth.health > 0)
		{
			Flip();
		}
	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public void ApplyDamage(float damage)
	{
		if (!isInvincible)
		{
			float direction = damage / Mathf.Abs(damage);
			damage = Mathf.Abs(damage);
			anim.SetBool("Hit", true);
			//enemyHealth.health -= damage;
			transform.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
			transform.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction * 300f, 100f));
			StartCoroutine(HitTime());
		}
	}

	public void MeleeAttack()
	{
		transform.GetComponent<Animator>().SetBool("Attack", true);
		Collider2D[] collidersEnemies = Physics2D.OverlapCircleAll(attackCheck.position, 0.9f);
		for (int i = 0; i < collidersEnemies.Length; i++)
		{
			if (collidersEnemies[i].gameObject.tag == "Enemy")
			{
				//collidersEnemies[i].gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
			}
		}
		StartCoroutine(WaitToAttack(0.5f));
	}


	public void Run()
	{
		anim.SetBool("IsWaiting", false);
		m_Rigidbody2D.velocity = new Vector2(distToPlayer / Mathf.Abs(distToPlayer) * speed, m_Rigidbody2D.velocity.y);
		if (doOnceDecision)
			StartCoroutine(NextDecision(0.5f));
	}
	public void Jump()
	{
		Vector3 targetVelocity = new Vector2(distToPlayer / Mathf.Abs(distToPlayer) * speed, m_Rigidbody2D.velocity.y);
		Vector3 velocity = Vector3.zero;
		m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref velocity, 0.05f);
		if (doOnceDecision)
		{
			anim.SetBool("IsWaiting", false);
			m_Rigidbody2D.AddForce(new Vector2(0f, 850f));
			StartCoroutine(NextDecision(1f));
		}
	}

	public void Idle()
	{
		m_Rigidbody2D.velocity = new Vector2(0f, m_Rigidbody2D.velocity.y);
		if (doOnceDecision)
		{
			anim.SetBool("IsWaiting", true);
			StartCoroutine(NextDecision(1f));
		}
	}

	public void EndDecision()
	{
		randomDecision = Random.Range(0.0f, 1.0f);
		endDecision = true;
	}

	IEnumerator HitTime()
	{
		isInvincible = true;
		isHitted = true;
		yield return new WaitForSeconds(0.1f);
		isHitted = false;
		isInvincible = false;
	}

	IEnumerator WaitToAttack(float time)
	{
		canAttack = false;
		yield return new WaitForSeconds(time);
		canAttack = true;
	}

	IEnumerator Dash()
	{
		anim.SetBool("IsDashing", true);
		isDashing = true;
		yield return new WaitForSeconds(0.1f);
		isDashing = false;
		EndDecision();
	}

	IEnumerator NextDecision(float time)
	{
		doOnceDecision = false;
		yield return new WaitForSeconds(time);
		EndDecision();
		doOnceDecision = true;
		anim.SetBool("IsWaiting", false);
	}

	IEnumerator DestroyEnemy()
	{
		CapsuleCollider2D capsule = GetComponent<CapsuleCollider2D>();
		capsule.size = new Vector2(1f, 0.25f);
		capsule.offset = new Vector2(0f, -0.8f);
		capsule.direction = CapsuleDirection2D.Horizontal;
		transform.GetComponent<Animator>().SetBool("IsDead", true);
		yield return new WaitForSeconds(0.25f);
		m_Rigidbody2D.velocity = new Vector2(0, m_Rigidbody2D.velocity.y);
		yield return new WaitForSeconds(1f);
		Destroy(gameObject);
	}
}

