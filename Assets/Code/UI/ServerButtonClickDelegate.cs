using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum ButtonType{
	StartStopServer, RefreshHosts, JoinServer
};

public class ServerButtonClickDelegate : MonoBehaviour {

	public ButtonType mButtonType;
	public delegate void ServerButtonClickHandler(ButtonType btType, Button button);
	public static event ServerButtonClickHandler onClickHandler = delegate {};
	
	void Start(){
		Button button = GetComponent<Button>();
		button.onClick.AddListener( () => {
			onClickHandler(mButtonType, button);
		});
	}

}
