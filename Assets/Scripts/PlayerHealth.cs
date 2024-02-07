using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public CubeManager cm;
    public int life;
    public GameObject[] hearts;
	public GameObject GameOver;
	public GameObject UIObj;
	public bool isdead;
	public Animator animator;
	public ParticleSystem dust;
	[SerializeField] private Transform player;
	public AudioClip /*Gameover,*/ Kurokidead;
	public AudioSource audioSource;
	public float volume = 0.1f;
	public bool died = false;
	public GameObject Bgm;

	private void Update()
    {
		if (life < 1)
		{
			
			
			animator.SetBool("isMoving", false);
			dust.Stop();
			player.gameObject.GetComponent<player>().enabled = false;
			UIObj.SetActive(false);
			GameOver.SetActive(true);
			

			if (Input.GetKey(KeyCode.X))
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}
			if (Input.GetKey(KeyCode.Escape))
			{
				SceneManager.LoadScene("MainMenu");
			}
		}
	}

    public void LoseHealth()
    {
		life -= 1;
		if (life < 3)
		{
			Destroy(hearts[0].gameObject);
		}
		if (life < 2)
		{
			Destroy(hearts[1].gameObject);
		}
		if (life < 1)
		{
			Destroy(hearts[2].gameObject);
			died = true;
			Lose();
		}
		/*if (life >= 1)
        {
			return;
        }*/
	}
	public void Lose()
    {
		if (died)
        {
			audioSource.PlayOneShot(Kurokidead, volume);
			//audioSource.PlayOneShot(Gameover, volume);
			Bgm.SetActive(false);
			died = false;
		}
		
	}
}
