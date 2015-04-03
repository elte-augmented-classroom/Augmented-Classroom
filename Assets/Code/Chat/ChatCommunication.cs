using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChatCommunication : MonoBehaviour {

	public GameObject chatPanelGO;
	public GameObject chatMessagesHolder;
	public InputField inputChat;
	public Button submitButton;
	public GameObject messagePrefab;
	bool toggleChat = false;
	
	void Start(){
		if(inputChat != null && submitButton != null){
			InputField.SubmitEvent submitEvent = new InputField.SubmitEvent();
			submitEvent.AddListener(SubmitMessage);
			inputChat.onEndEdit = submitEvent;
			submitButton.onClick.AddListener(() => SubmitMessage(inputChat.text));
		}
	}
	
	void Update() {
		if(Input.GetButton("ChatToggle")){
			toggleChat = !toggleChat;
			chatPanelGO.SetActive(toggleChat);
			if(toggleChat){
				EventSystem.current.SetSelectedGameObject(inputChat.gameObject, null);
				inputChat.OnPointerClick(new PointerEventData(EventSystem.current));
			}
		}
	}
	
	void SubmitMessage(string content){
		if(content.Length != 0){
			networkView.RPC("createMessage", RPCMode.OthersBuffered, content);
		}
	}
	
	[RPC]
	void createMessage(string message, NetworkMessageInfo info){
		GameObject messageGo = GameObject.Instantiate(messagePrefab) as GameObject;
		Text text = messageGo.GetComponent<Text>();
		text.text = message;
		if(!info.networkView.isMine) text.color = Color.yellow;
		messageGo.transform.parent = chatMessagesHolder.transform;
	}
}
