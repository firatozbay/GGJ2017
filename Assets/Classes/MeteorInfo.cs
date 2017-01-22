using UnityEngine;
using System.Collections;

public class MeteorInfo : MonoBehaviour {
	private GameObject warningPrefab;
	private SpriteRenderer meteorWarning;
	private Camera camera;
	private bool approaching;
	private float prevDist;

	void Start() {
		warningPrefab = Resources.Load<GameObject>("Warning");
		camera = Camera.main;
		meteorWarning = ((GameObject)Instantiate(warningPrefab, 
			camera.ScreenToWorldPoint(new Vector3(-1000, -1000, 0)), Quaternion.identity)).GetComponent<SpriteRenderer>();
		prevDist = 0;
	}

	void Update() {
		Vector2 point = camera.WorldToScreenPoint(transform.position);
		Rect view = new Rect(0, 0, Screen.width, Screen.height);
		float dist = DistancePointToRectangle(point, view);
		Vector3 warningPos;
        if (prevDist > dist && dist < 500 && !view.Contains(point)) {
			warningPos = camera.ScreenToWorldPoint(new Vector3(
				Mathf.Max(Mathf.Min(point.x, view.width - 50), view.x + 50),
				Mathf.Max(Mathf.Min(point.y, view.height - 50), view.y + 50)));
		} else {
			warningPos = camera.ScreenToWorldPoint(new Vector3(-1000, -1000));
        }
		warningPos.z = 0;
		meteorWarning.transform.position = warningPos;
		prevDist = dist;
	}

	public static float DistancePointToRectangle(Vector2 point, Rect rect) {
		//  Calculate a distance between a point and a rectangle.
		//  The area around/in the rectangle is defined in terms of
		//  several regions:
		//
		//  O--x
		//  |
		//  y
		//
		//
		//        I   |    II    |  III
		//      ======+==========+======   --yMin
		//       VIII |  IX (in) |  IV
		//      ======+==========+======   --yMax
		//       VII  |    VI    |   V
		//
		//
		//  Note that the +y direction is down because of Unity's GUI coordinates.

		if (point.x < rect.xMin) { // Region I, VIII, or VII
			if (point.y < rect.yMin) { // I
				Vector2 diff = point - new Vector2(rect.xMin, rect.yMin);
				return diff.magnitude;
			} else if (point.y > rect.yMax) { // VII
				Vector2 diff = point - new Vector2(rect.xMin, rect.yMax);
				return diff.magnitude;
			} else { // VIII
				return rect.xMin - point.x;
			}
		} else if (point.x > rect.xMax) { // Region III, IV, or V
			if (point.y < rect.yMin) { // III
				Vector2 diff = point - new Vector2(rect.xMax, rect.yMin);
				return diff.magnitude;
			} else if (point.y > rect.yMax) { // V
				Vector2 diff = point - new Vector2(rect.xMax, rect.yMax);
				return diff.magnitude;
			} else { // IV
				return point.x - rect.xMax;
			}
		} else { // Region II, IX, or VI
			if (point.y < rect.yMin) { // II
				return rect.yMin - point.y;
			} else if (point.y > rect.yMax) { // VI
				return point.y - rect.yMax;
			} else { // IX
				return 0f;
			}
		}
	}
}
