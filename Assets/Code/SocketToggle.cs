using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;


public class SocketToggle : MonoBehaviour {

	public GameObject parent;
	public Material onMaterial;
	public Material offMaterial;

	void OnMouseDown(){
		bool stateLight = false;
		foreach (Light light in parent.GetComponentsInChildren<Light>()) {
			light.enabled = !light.enabled;
			stateLight = light.enabled;
		}
		if(stateLight) renderer.material = onMaterial;
		else renderer.material = offMaterial;
	}


}
