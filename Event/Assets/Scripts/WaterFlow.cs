using UnityEngine;
using System.Collections;

public class WaterFlow : MonoBehaviour {

	public Renderer rend;

	public float scrollSpeedX = 0.5f;
	public float scrollSpeedY = 0.5f;

	// Use this for initialization
	void Start () 
	{
		rend = GetComponent<Renderer> ();
		rend.enabled = true;
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		var offsetX = Time.time * scrollSpeedX;
		var offsetY = Time.time * scrollSpeedY;
		rend.material.mainTextureOffset = new Vector2 (offsetX, offsetY);	
	}
}
