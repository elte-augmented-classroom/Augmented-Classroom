using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawInBoard : MonoBehaviour {

	public GameObject prefabDrawing;
	GameObject linesHolder;
	List<TrailRenderer> lines;
	GameObject draw_instance;
	
	void Awake(){
		lines = new List<TrailRenderer>();
	}

	void OnMouseDown(){
		if(linesHolder == null){
			linesHolder = new GameObject("Lines holder");
			linesHolder.transform.parent = transform;
		}
		Vector3 mouse_pos = Input.mousePosition;
		mouse_pos.z = 2;
		Vector3 draw_instance_pos = Camera.main.ScreenToWorldPoint(mouse_pos);
		draw_instance = GameObject.Instantiate(prefabDrawing, draw_instance_pos, transform.parent.rotation) as GameObject;
		draw_instance.transform.parent = linesHolder.transform;
		lines.Add(draw_instance.GetComponent<TrailRenderer>());
	}
	
	void OnMouseDrag(){
		if(draw_instance != null){
			draw_instance.transform.position = Input.mousePosition;
		}
	}

}
