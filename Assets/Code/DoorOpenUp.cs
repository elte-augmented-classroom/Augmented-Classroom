using UnityEngine;
using System.Collections;

public enum DoorId{
	Entrance, Room1, Exit
};

public class DoorOpenUp : MonoBehaviour {

//	public DoorId mDoorId;
//	public delegate void DoorIdHandler(DoorId doorId);
//	public static event DoorIdHandler onDoorEvent; 

	Transform parent;
	Vector3 new_door_position;
	
	void Start(){
		parent = transform.parent;
		new_door_position = new Vector3(transform.position.x, 10f, transform.position.z);
	}
	
	void OnTriggerEnter(Collider other) {
		if(other.tag == "Player"){
			StopAllCoroutines();
			StartCoroutine(openDoor());
		}
	}
	
			
	IEnumerator openDoor(){
		new_door_position.y = 10f;
		while(parent.position.y + 2 <= new_door_position.y){
			parent.position = Vector3.Lerp(parent.position, new_door_position, Time.deltaTime * 1f);
			yield return null;
		}
		yield return new WaitForSeconds(3f);
		yield return StartCoroutine(closeDoor());
	}
	
	IEnumerator closeDoor(){
	new_door_position.y = 3.4f;
		while(parent.position.y >= new_door_position.y + 0.3f){
			parent.position = Vector3.Lerp(parent.position, new_door_position, Time.deltaTime * 1f);
			yield return null;
		}
		parent.position = new_door_position;
		yield return null;
	}
}
