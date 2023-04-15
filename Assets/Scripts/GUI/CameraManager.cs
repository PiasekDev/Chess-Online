using TMPro;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    float targetRight = 8f;
    float targetLeft = -1f;
    float horizontalSize;
    Vector3 startingPosition;
    void Start() {
        Camera main = GetComponent<Camera>();
        horizontalSize = main.aspect * main.orthographicSize;
        startingPosition = transform.position;
        AdjustCameraPositionForWhitePlayer(); //default adjust
    }

    public void AdjustCameraPositionForWhitePlayer() {
        transform.position = new Vector3(startingPosition.x + (targetRight - (startingPosition.x + horizontalSize)), startingPosition.y, startingPosition.z);
    }

    public void ApplyBlackPlayerViewFixes() {
        transform.SetPositionAndRotation(
            new Vector3(startingPosition.x + (targetLeft - (startingPosition.x - horizontalSize)), startingPosition.y, startingPosition.z), 
            Quaternion.Euler(0, 0, 180));
        // Correctly mirror side text
		Transform sideTextTransform = BoardManager.Instance.transform.GetChild(0);
		sideTextTransform.SetPositionAndRotation(
			new Vector3(sideTextTransform.position.x + 8, sideTextTransform.position.y + 8, sideTextTransform.position.z),
			Quaternion.Euler(180, 180, 0));
        // Reverse text direction
        foreach (var tmpText in sideTextTransform.GetComponentsInChildren<TMP_Text>()) {
			char[] textArray = tmpText.text.ToCharArray();
			System.Array.Reverse(textArray);
			tmpText.text = new string(textArray);
		}
	}
}
