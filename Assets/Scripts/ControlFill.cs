using UnityEngine;
using UnityEngine.UI;

public class ControlFill : MonoBehaviour {

    void Start () {
        gameObject.GetComponentInChildren<Text>().text = InputManager.GrabKey(name).ToString();
    }

}
