using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class ClampToCircle : MonoBehaviour {
	
	
	
	private Vector3 centerPt;
	private Joystick TheJoystick;
	private int radius;
	
	void Start(){
		
		TheJoystick = GetComponent<Joystick> ();
		radius = TheJoystick.MovementRange;
		centerPt = GetComponent<RectTransform> ().position;
		TheJoystick.MovementRange = radius;
		TheJoystick.MovementRange += 200;
		
	}
	
	void Update() {
		Vector3 newPos = transform.position;
		Vector3 offset = newPos - centerPt;
		transform.position = centerPt + Vector3.ClampMagnitude(offset, radius);
	}
}
