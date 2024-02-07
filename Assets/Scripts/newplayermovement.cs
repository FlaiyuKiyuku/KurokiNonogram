using UnityEngine;
using System.Collections;

public class newplayermovement : MonoBehaviour {

	[SerializeField] private float speed = 2f;

	private Animator animator;

	void Start () 
	{
		animator = GetComponent<Animator>();
	}

	void Update()
	{
		var velocity = Vector3.forward  * Input.GetAxis("Vertical") * speed;
		transform.Translate(velocity * Time.deltaTime);
		animator.SetFloat("Speed", velocity.z);
	}

	/*void Update () {
	
		if (Input.GetKey (KeyCode.UpArrow) && transform.position.x > 0.2f) 
		{
			transform.Translate (new Vector3 (-1, 0, 0) * speed * Time.deltaTime);
			
		}
		else if (Input.GetKey (KeyCode.DownArrow) && transform.position.x < 17.8f) 
		{
			transform.Translate (new Vector3 (1, 0, 0) * speed * Time.deltaTime);
		} 
		else if (Input.GetKey (KeyCode.LeftArrow) && transform.position.z > 0.2f) 
		{
			transform.Translate (new Vector3 (0, 0, -1) * speed * Time.deltaTime);
		} 
		else if (Input.GetKey (KeyCode.RightArrow) && transform.position.z < 17.8f) 
		{
			transform.Translate (new Vector3 (0, 0, 1) * speed * Time.deltaTime);
		}

	}*/
}
