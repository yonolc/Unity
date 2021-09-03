using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class movekarakter : MonoBehaviour
{
	//referensikan kata kunci Animator diberi variable anim
	public Animator anim;
	//variable animasi jump
	bool isjump, isfall, isfall2, isjump2;
	//Variabel untuk gerakan horizontal
	float dirX;
	//variable untuk hitung mundur loncatan yang telah dibuat
	public int hitungLoncatan;
	//Variabel untuk kecepatan gerak dan dapat diatur di Inspector
	public float kecepatan = 10f;
	//variable untuk ketinggian lompatan
	public float lompatan = 8;
	//variable muka kanan kiri
	private bool facingRight = true;
	//referensikan komponen RigidBody
	Rigidbody2D rb;
	//variable untuk berapa banyak lompatan yang dapat dibuat
	public int jumpvalue = 2;
	//variable pendeteksi ditanah
	bool isGrounded = true;
	// Start is called before the first frame update
	void Start()
	{

		anim = GetComponent<Animator>();
		//untuk mengoperasikan komponenRigidBody
		rb = GetComponent<Rigidbody2D>();

	}

	// Update is called once per frame
	void Update()
	{
		if (CrossPlatformInputManager.GetButtonUp("Jump") && hitungLoncatan > 0)
		{
			Jump();

		}

		if (isGrounded)
		{
			hitungLoncatan = jumpvalue;
		}
		else if (isGrounded && hitungLoncatan == 0)
		{
			hitungLoncatan = jumpvalue;
		}

		anim.SetFloat("speed", Mathf.Abs(dirX));
		dirX = CrossPlatformInputManager.GetAxis("Horizontal");




	}
	//fungsi hadap kanan kiri
	void Flip()
	{
		facingRight = !facingRight;
		Vector3 Scaler = transform.localScale;
		Scaler.x *= -1;
		transform.localScale = Scaler;
	}

	void FixedUpdate()
	{

		if (rb.velocity.y > 0)
		{
			anim.SetBool("isjump", true);
			anim.SetBool("isfall", false);
			anim.SetBool("isjump2", false);
		}
		if (rb.velocity.y > 0 && hitungLoncatan == 0)
		{
			anim.SetBool("isjump2", true);
			anim.SetBool("isjump", false);
			anim.SetBool("isfall", false);
		}
		else if (rb.velocity.y < 0)
		{
			anim.SetBool("isjump", false);
			anim.SetBool("isjump2", false);
			anim.SetBool("isfall", true);
		}
		else
		{
			anim.SetBool("isfall", false);
		}


		rb.velocity = new Vector2(dirX * kecepatan, rb.velocity.y);
		if (facingRight == false && dirX > 0)
		{
			Flip();
		}
		else if (facingRight == true && dirX < 0)
		{
			Flip();
		}




	}
	//fungsi lompatan
	private void Jump()
	{

		rb.velocity = Vector2.up * lompatan;
		isGrounded = false;
		hitungLoncatan--;

	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (!isGrounded)
		{
			isGrounded = true;
		}
	}
	/*   script hadap kanan kiri
	void hadap()
    {
		if (dirX > 0)
			mukakanan = true;
		else if (dirX < 0)
			mukakiri = false;
		if (((mukakanan) && (localScale.x < 0)) || ((!mukakanan) && (localScale.x > 0)))
			localScale.x *= -1;
		transform.localScale = localScale;
    }
	*/

}