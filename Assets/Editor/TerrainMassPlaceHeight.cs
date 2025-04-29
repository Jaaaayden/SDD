using UnityEngine;
using UnityEditor;

public class TerrainMassPlaceHeight : EditorWindow
{
    public GameObject prefab;
    public Terrain terrain;
    public int count = 100;
    public float maxSlope = 20f;

    public static void ShowWindow()
    {
        GetWindow<TerrainMassPlaceHeight>("Mass Place");
    }

    void OnGUI()
    {
        prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", prefab, typeof(GameObject), false);
        terrain = (Terrain)EditorGUILayout.ObjectField("Terrain", terrain, typeof(Terrain), true);
        count = EditorGUILayout.IntField("Number of object", count);
        maxSlope = EditorGUILayout.FloatField("Max Slope", maxSlope);

        if (GUILayout.Button("Place Logs"))
        {
            PlaceLogs();
        }
    }

    void PlaceLogs()
    {
        if (prefab == null || terrain == null) return;

        TerrainData data = terrain.terrainData;
        Vector3 terrainPos = terrain.transform.position;

        for (int i = 0; i < count; i++)
        {
            float x = Random.Range(0f, 1f);
            float z = Random.Range(0f, 1f);
            float worldX = terrainPos.x + x * data.size.x;
            float worldZ = terrainPos.z + z * data.size.z;
            float height = terrain.SampleHeight(new Vector3(worldX, 0, worldZ)) + terrainPos.y;

            Vector3 normal = data.GetInterpolatedNormal(x, z);
            float slope = Vector3.Angle(normal, Vector3.up);

            if (slope <= maxSlope)
            {
                Vector3 pos = new Vector3(worldX, height, worldZ);
                GameObject prefab_obj = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
                prefab_obj.transform.position = pos;
                prefab_obj.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0); // random rotation
                Undo.RegisterCreatedObjectUndo(prefab_obj, "Place prefab");
            }
        }
    }
}