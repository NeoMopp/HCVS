using UnityEngine;
using System.Collections;

public class Deformable : MonoBehaviour 
{
	// Mesh filter with mesh to deform
	public MeshFilter meshFilter;
	// Impact resistance to calculate amount of deformation applied to the mesh
	public float Hardness = 0.5f;
	// Configure if the mesh at collider must also be deformed (only works if a MeshCollider is in use)
	// Warning: High CPU usage - in low cpu environments disable it or combine with fewer mesh updates per second (below)
	public bool DeformMeshCollider = true;
	// Configure the mesh (and mesh collider) update frequency in times per second. 0 for real time (high CPU usage)
	public float UpdateFrequency = 0;
	// Maximum movement allowed for a vertex from its original position (0 means no limit)
	public float MaxVertexMov = 0;
	// Color to be applied at deformed vertices (only works for shaders that handle vertices colors)
	public Color32 DeformedVertexColor = Color.gray;
	// Texture to modulate maximum movement allowed (uses alpha channel)
	public Texture2D HardnessMap;

	// Mesh to deform
	Mesh mesh;
	// Cached access
	MeshCollider meshCollider;
	// Backup of original mesh form
	Vector3[] baseVertices;
	// Backup of original mesh vertices colors
	Color32[] baseColors;
	// Size factor of mesh
	float sizeFactor;
	// Vertex and color array that will suffer changes
	Vector3[] vertices;
	Color32[] colors;
	float[] map;
	// Flag requesting mesh update
	bool meshUpdate;
	// Mesh update timer
	float lastUpdate;
	// Applied hardness map
	Texture2D appliedMap;

	void Awake () 
	{
		// Capture mesh collider
		meshCollider = GetComponent<MeshCollider>();

		// Capture local mesh filter if not assigned
		if (!meshFilter)
			meshFilter = GetComponent<MeshFilter>();
		
		// Load mesh data
		if (meshFilter)
			LoadMesh();
		else
			// Warning if no mesh filter assigned at start
			Debug.LogWarning("Deformable component warning: No mesh filter assigned for object " + gameObject.ToString());

	}

	void LoadMesh ()
	{
		// Capture mesh
		if (meshFilter)
			mesh = meshFilter.mesh;
		else
			mesh = null;
		
		// Check mesh assigned
		if (!mesh)
		{
			// Warning if no mesh assigned
			Debug.LogWarning("Deformable component warning: Mesh at mesh filter is null " + gameObject.ToString());		
			return;
		}
		
		// Capture mesh arrays
		vertices = mesh.vertices;
		colors = mesh.colors32;
		
		// Save original mesh form and colors
		baseVertices = mesh.vertices;
		baseColors = mesh.colors32;
		
		// Calc size factor
		Vector3 s = mesh.bounds.size;
		sizeFactor = Mathf.Max(1, Mathf.Min(s.x, s.y, s.z));
	}
	
	void LoadMap ()
	{
		appliedMap = HardnessMap;
		
		// Load hardness map
		if (HardnessMap)
		{
			Vector2[] uvs = mesh.uv;
			map = new float[uvs.Length];
			int c = 0;
			foreach (Vector2 uv in uvs)
			{
				try
				{
					map[c] = HardnessMap.GetPixelBilinear(uv.x, uv. y).a;
				}
				catch 
				{
					Debug.LogWarning("Deformable component warning: Texture at HardnessMap must be readable (check Read/Write Enabled at import settings). Hardness map not applied.");		
					map = null;
					return;
				}
				c++;
			}
		}
		else
			map = null;
	}

	void Deform (Collision collision)
	{
		// Check mesh assigned
		if ((!mesh) || (!meshFilter))
			return;
		
		// Calc collision force	
		float colForce = Mathf.Min(1, collision.relativeVelocity.sqrMagnitude / 1000);
		
		// Ignore weak collisions
		if (colForce < 0.01)
			return;
		
		// distFactor is the amount of deforming in local coordinates 
		float distFactor = colForce * (sizeFactor * (0.1f / Mathf.Max(0.1f, Hardness)));
		
		// Deform process
		foreach (ContactPoint contact in collision.contacts)
		{
			for (int i = 0; i < vertices.Length; i++)
			{
				// Apply deformation only on vertex inside "blast area"
				Vector3 p = meshFilter.transform.InverseTransformPoint(contact.point);
				float d = (vertices[i] - p).sqrMagnitude;
				if (d <= distFactor)
				{
					// Deformation is the collision normal with local deforming ratio
					// Vertices near the impact point must also be more deformed
					Vector3 n = meshFilter.transform.InverseTransformDirection(contact.normal * (1 - (d / distFactor)) * distFactor);
					
					// Apply hardness map if any
					if (map != null)
						n *= 1 - map[i];
					
					// Deform vertex
					vertices[i] += n;
					
					// Apply max vertex movement if configured
					// Here the deformed vertex position is just scaled down to keep the best deformation while respecting limits
					if (MaxVertexMov > 0)
					{
						float max = MaxVertexMov;
						n = vertices[i] - baseVertices[i];
						d = n.magnitude;
						if (d > max)
							vertices[i] = baseVertices[i] + (n * (max / d));
						
						// Apply vertex color deformation
						// Deform color is applied in proportional amount with vertex distance and max deform
						if (colors.Length > 0)
						{
							d = (d / MaxVertexMov);
							colors[i] = Color.Lerp(baseColors[i], DeformedVertexColor, d);
						}
					}
					else
					{
						if (colors.Length > 0)
						{
							// Apply vertex color deformation
							// Deform color is applied in proportional amount with vertex distance and mesh distance factor
							colors[i] = Color.Lerp(baseColors[i], DeformedVertexColor, (vertices[i] - baseVertices[i]).magnitude / (distFactor * 10));				
						}
					}
				}			
			}
		}
		
		// Request mesh update
		RequestUpdateMesh();
	}

	public void Repair (float repair)
	{
		Repair(repair, Vector3.zero, 0);
	}
	
	public void Repair (float repair, Vector3 point, float radius)
	{
		// Check mesh assigned
		if ((!mesh) || (!meshFilter))
			return;
		
		// Transform world point to mesh local
		point = meshFilter.transform.InverseTransformPoint(point);
		
		// Repairing is done returning vertices position and color to original positions by requested amount
		int i = 0;
		foreach (Vector3 vertex in vertices)
		{
			try
			{
				// Check for repair inside radius
				if (radius > 0)
				{
					if ((point - vertex).magnitude >= radius)
						continue;
				}
				
				// Repair
				Vector3 n = vertex - baseVertices[i];
				vertices[i] = baseVertices[i] + (n * (1 - repair));
				if (colors.Length > 0)
					colors[i] = Color.Lerp(colors[i], baseColors[i], repair);
			}
			finally
			{
				i++;
			}
		}
		
		// Request mesh update
		RequestUpdateMesh();
	}

	void RequestUpdateMesh ()
	{	
		// Check immediate update mode
		if (UpdateFrequency == 0)
			UpdateMesh();
		else
			// Delayed mode
			meshUpdate = true;
	}

	void UpdateMesh ()
	{
		// Update vertices and colors
		mesh.vertices = vertices;	
		if (colors.Length > 0)
			mesh.colors32 = colors;
		
		// Recalc normals and bounds
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
		
		// Update mesh collider if requested
		if ((meshCollider) && (DeformMeshCollider))
		{	
			meshCollider.sharedMesh = null;
			meshCollider.sharedMesh = mesh;
		}
		
		// Done
		lastUpdate = Time.time;
		meshUpdate = false;
	}

	void OnCollisionEnter (Collision collision) 
	{
		Deform(collision);
	}
	
	void OnCollisionStay (Collision collision)
	{
		Deform(collision);
	}
	
	void FixedUpdate ()
	{
		// Check mesh change
		if (((meshFilter) && (mesh != meshFilter.mesh)) || ((!meshFilter) && (mesh)))
			LoadMesh();
		
		// Check hardness map change
		if (HardnessMap != appliedMap)
			LoadMap();
		
		// Delayed mesh update
		if ((meshUpdate) && (Time.time - lastUpdate >= (1 / UpdateFrequency)))
			UpdateMesh();
	}

}
