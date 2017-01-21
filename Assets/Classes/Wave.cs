using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wave : MonoBehaviour {
	private const float TARGET_SIZE = 50;
	private const float SPEED = 5;
	private const int VERTEX_DENSITY = 60 / 2;
	private const float ANGLE = 120 / 2;
	private const int PARTICLE_AMOUNT = 30;
	private const float OPENING_SPACING = 2;
	private static Color[] COLORS = { Color.red, Color.blue, Color.green };

	private float currentSize;
	private LineRenderer[] lines;
	private EdgeCollider2D[] colliders;
	private ParticleSystem[] particles;
	private int opening;
	private Color color;

	void Start () {
		color = COLORS[(int)Random.Range(0, COLORS.Length)];
		currentSize = 0;
		lines = GetComponentsInChildren<LineRenderer>();
		colliders = GetComponentsInChildren<EdgeCollider2D>();
		ParticleSystem p = GetComponentInChildren<ParticleSystem>();
		particles = new ParticleSystem[PARTICLE_AMOUNT];
		p.startColor = color;
		lines[0].SetColors(color, color);
		lines[1].SetColors(color, color);
        lines[0].sortingLayerName = "Foreground";
        lines[1].sortingLayerName = "Foreground";
        for (int i = 0; i < particles.Length; i++) {
			particles[i] = Instantiate(p.gameObject).GetComponent<ParticleSystem>();
			particles[i].transform.SetParent(gameObject.transform);
		}
		opening = (int)Mathf.Round(Random.Range(24, (VERTEX_DENSITY * 2 - 23)));
	}
	
	void Update () {
		currentSize += Time.deltaTime * SPEED;
		RefreshLine();
		if (currentSize >= TARGET_SIZE)
			Destroy(gameObject);
	}

	public void RefreshLine() {
		List<Vector3> vertices = new List<Vector3>();
		List<Vector3> vertices1 = new List<Vector3>();
		List<Vector3> vertices2 = new List<Vector3>();
		int i = 0;
		for (float a = 180 - ANGLE; a <= 180 + ANGLE; a += ANGLE / VERTEX_DENSITY) {
			i++;
			vertices.Add(new Vector2(Mathf.Cos(a * Mathf.Deg2Rad), Mathf.Sin(a * Mathf.Deg2Rad)) * currentSize);
			if (i < opening)
				vertices1.Add(vertices[vertices.Count - 1]);
			else if(i >= opening && i < opening + OPENING_SPACING) {
				//vertices.RemoveAt(vertices.Count - 1);
			} else
				vertices2.Add(vertices[vertices.Count - 1]);
		}
		//if(ANGLE == 180)
		//	list.Add(new Vector2(Mathf.Cos(0), Mathf.Sin(0)) * currentSize);
		lines[0].SetVertexCount(vertices1.Count);
		lines[0].SetPositions(vertices1.ToArray());
		lines[1].SetVertexCount(vertices2.Count);
		lines[1].SetPositions(vertices2.ToArray());
		Vector2[] vec1 = new Vector2[vertices1.Count];
		Vector2[] vec2 = new Vector2[vertices2.Count];
		for (i = 0; i < vertices1.Count; i++)
			vec1[i] = vertices1[i];
		for (i = 0; i < vertices2.Count; i++)
			vec2[i] = vertices2[i];
		colliders[0].points = vec1;
		colliders[1].points = vec2;
		for (i = 0; i < particles.Length; i++) {
			particles[i].transform.localPosition = vertices[i * (vertices.Count / particles.Length)];
		}
	}

	void OnTriggerStay2D(Collider2D col) {
		
	}
}
