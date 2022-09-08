using UnityEngine;
using System.Collections;

public class KHGirlAnimControl : MonoBehaviour {
	
	public Animator animator;

	private Transform defaultCamTransform;
	private Vector3 resetPos;
	private Quaternion resetRot;
	private GameObject cam;
	private GameObject fighter;

	void Start()
	{
		cam = GameObject.FindWithTag("MainCamera");
		defaultCamTransform = cam.transform;
		resetPos = defaultCamTransform.position;
		resetRot = defaultCamTransform.rotation;
		fighter = GameObject.FindWithTag("Player");
		fighter.transform.position = new Vector3(0,0,0);
	}

	void OnGUI () 
	{
		if (GUI.RepeatButton (new Rect (25, 480, 100, 30), "Reset Scene")) 
		{
			defaultCamTransform.position = resetPos;
			defaultCamTransform.rotation = resetRot;
			fighter.transform.position = new Vector3(0,0,0);
			animator.Play("Idle");
		}

		if (GUI.Button(new Rect(25, 20, 100, 30), "Walk"))
		{
			animator.Play("Walk");
		}

		if (GUI.Button(new Rect(25, 180, 100, 30), "Idle"))
		{
			animator.Play("Idle");
		}
		if (GUI.Button(new Rect(25, 420, 100, 30), "Idle1"))
		{
			animator.Play("Idle1");
		}

		if (GUI.Button(new Rect(25, 60, 100, 30), "Run"))
		{
			animator.Play("Run");
		}

		if (GUI.Button(new Rect(25, 100, 100, 30), "Kute"))
		{
			animator.Play("Kute");
		}

		if (GUI.Button(new Rect(25, 260, 100, 30), "B_Idle"))
		{
			animator.Play("B_Idle");
		}

		if (GUI.Button(new Rect(25, 300, 100, 30), "B_Dead"))
		{
			animator.Play("B_Dead");
		}

		if (GUI.Button(new Rect(25, 340, 100, 30), "B_Dow"))
		{
			animator.Play("B_Dow");
		}
	
		if (GUI.Button (new Rect (25, 220, 100, 30), "Skill")) 
		{
			animator.Play("Skill");
		}
		if (GUI.Button(new Rect(25, 380, 100, 30), "B_Attack"))
		{
			animator.Play("B_Attack");
		}
		if (GUI.Button(new Rect(25, 140, 100, 30), "Attack"))
		{
			animator.Play("Attack");
		}
	}
}