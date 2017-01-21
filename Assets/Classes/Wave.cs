using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wave : MonoBehaviour {
	private const float TARGET_SIZE = 50;
	private const float SPEED = 5;
	private const int VERTEX_DENSITY = 100 / 2;
	private const float ANGLE = 350 / 2;
	private const int PARTICLE_AMOUNT = 72;

	private float currentSize;
	private LineRenderer line;
	private ParticleSystem[] particles;
	private float rotation;
	private List<Vector3> vertices;

	void Start () {
		currentSize = 0;
		line = GetComponent<LineRenderer>();
		ParticleSystem p = GetComponentInChildren<ParticleSystem>();
		particles = new ParticleSystem[PARTICLE_AMOUNT];
		for(int i = 0; i < particles.Length; i++) {
			particles[i] = Instantiate(p.gameObject).GetComponent<ParticleSystem>();
			particles[i].transform.SetParent(gameObject.transform);
		}
		rotation = Random.Range(0, 360);
	}
	
	void Update () {
		currentSize += Time.deltaTime * 5;
		RefreshLine();
		if (currentSize >= TARGET_SIZE)
			Destroy(gameObject);
	}

	public void RefreshLine() {
		vertices = new List<Vector3>();
		for (float a = rotation - ANGLE; a <= rotation + ANGLE; a += ANGLE / VERTEX_DENSITY) {
			vertices.Add(new Vector2(Mathf.Cos(a * Mathf.Deg2Rad), Mathf.Sin(a * Mathf.Deg2Rad)) * currentSize);
		}
		//if(ANGLE == 180)
		//	list.Add(new Vector2(Mathf.Cos(0), Mathf.Sin(0)) * currentSize);
		line.SetVertexCount(vertices.Count);
		line.SetPositions(vertices.ToArray());
		for (int i = 0; i < particles.Length; i++) {
			particles[i].transform.localPosition = vertices[i * (vertices.Count / particles.Length)];
		}
	}

	void OnTriggerStay2D(Collider2D col) {
		if (col.gameObject.layer == gameObject.layer)
			return;
		if(col.OverlapPoint(vertices[0]) || col.OverlapPoint(vertices[vertices.Count - 1])) {
			Debug.Log("Morph!");
		}
	}
}
