using UnityEngine;
using System.Collections;

public class LeapMotionToggleDrawingBoard : MonoBehaviour {

	public Material leapMotionNoticeCtrlMat;
	public Material leapMotionOnUseMat;
	public GameObject handsGO;
	public GameObject playerGO;
	public Transform mainCameraPosition;
	bool toggleUseLeapMotion;
	
	void OnMouseDown(){
		if(toggleUseLeapMotion){
			renderer.material = leapMotionOnUseMat;
			toggleUseLeapMotion = false;
			
			StartCoroutine(MoveCamera());
			
			
		}else{
			renderer.material = leapMotionNoticeCtrlMat;
			toggleUseLeapMotion = true;
		}
		handsGO.SetActive(!toggleUseLeapMotion);
		playerGO.SetActive(toggleUseLeapMotion);
		
	}
	
	IEnumerator MoveCamera(){
		float time_moving = Time.deltaTime * 2f;
//		Vector3 diff = mainCameraPosition.position - drawingBoardGO.transform.position;
//		Debug.Log(diff);
		Quaternion dest_rotation = Quaternion.Inverse(transform.localRotation);
		Vector3 dest_position = transform.position;
		dest_position.x += 10f;
		while(transform.position.x+10f < mainCameraPosition.position.x){
			mainCameraPosition.position = Vector3.Lerp(mainCameraPosition.position, dest_position, time_moving);
			mainCameraPosition.rotation = Quaternion.Lerp(mainCameraPosition.rotation, dest_rotation, time_moving);
			yield return null;
		}
		mainCameraPosition.position = dest_position;
		yield return null;
	}
}
