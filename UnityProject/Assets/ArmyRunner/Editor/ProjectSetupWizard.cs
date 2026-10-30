using System.IO;
using ArmyRunner;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ArmyRunner.Editor
{
    public static class ProjectSetupWizard
    {
        private const string ScenePath = "Assets/ArmyRunner/Scenes/Main.unity";

        [MenuItem("Army Runner/Setup/Create Starter Scene")]
        public static void CreateStarterScene()
        {
            EnsureFolder("Assets/ArmyRunner/Scenes");
            EnsureFolder("Assets/ArmyRunner/Materials");
            Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

            CreateMaterial("ArmyGround", new Color(0.22f, 0.42f, 0.25f));
            CreateMaterial("ArmyTrack", new Color(0.22f, 0.24f, 0.27f));
            CreateMaterial("PlayerArmy", new Color(0.95f, 0.28f, 0.12f));
            CreateMaterial("GatePositive", new Color(0.12f, 0.72f, 0.35f));
            CreateMaterial("GateNegative", new Color(0.85f, 0.18f, 0.16f));

            GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Cube);
            ground.name = "Ground";
            ground.transform.position = new Vector3(0f, -0.25f, 55f);
            ground.transform.localScale = new Vector3(40f, 0.5f, 130f);
            ground.GetComponent<Renderer>().sharedMaterial = LoadMaterial("ArmyGround");

            GameObject track = GameObject.CreatePrimitive(PrimitiveType.Cube);
            track.name = "Runner Track";
            track.transform.position = new Vector3(0f, 0.02f, 55f);
            track.transform.localScale = new Vector3(10f, 0.1f, 120f);
            track.GetComponent<Renderer>().sharedMaterial = LoadMaterial("ArmyTrack");

            GameObject player = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            player.name = "Player Army";
            player.transform.position = new Vector3(0f, 1f, 0f);
            player.transform.localScale = new Vector3(1.1f, 1.8f, 1.1f);
            player.GetComponent<Renderer>().sharedMaterial = LoadMaterial("PlayerArmy");
            player.AddComponent<Rigidbody>().isKinematic = true;
            player.AddComponent<CrowdCount>();
            player.AddComponent<RunnerMotor>();

            GameObject cameraObject = new GameObject("Main Camera");
            cameraObject.tag = "MainCamera";
            Camera camera = cameraObject.AddComponent<Camera>();
            camera.fieldOfView = 58f;
            cameraObject.AddComponent<AudioListener>();
            RunnerCameraFollow follow = cameraObject.AddComponent<RunnerCameraFollow>();
            follow.SetTarget(player.transform);
            cameraObject.transform.position = player.transform.position + new Vector3(0f, 8f, -10f);
            cameraObject.transform.LookAt(player.transform.position + Vector3.forward * 8f);

            GameObject lightObject = new GameObject("Directional Light");
            Light light = lightObject.AddComponent<Light>();
            light.type = LightType.Directional;
            light.intensity = 1.2f;
            lightObject.transform.rotation = Quaternion.Euler(50f, -30f, 0f);

            CreateGate(-2.5f, 18f, "+5", "Gate Left");
            CreateGate(2.5f, 18f, "x2", "Gate Right");

            GameObject systems = new GameObject("Game Systems");
            systems.AddComponent<GameBootstrap>();

            EditorSceneManager.SaveScene(scene, ScenePath);
            EditorBuildSettings.scenes = new[] { new EditorBuildSettingsScene(ScenePath, true) };
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("Army Runner starter scene created at " + ScenePath);
        }

        private static void CreateGate(float x, float z, string value, string objectName)
        {
            GameObject gate = GameObject.CreatePrimitive(PrimitiveType.Cube);
            gate.name = objectName;
            gate.transform.position = new Vector3(x, 1.1f, z);
            gate.transform.localScale = new Vector3(0.25f, 2.2f, 2f);
            gate.GetComponent<Renderer>().sharedMaterial = LoadMaterial(value.StartsWith("+") ? "GatePositive" : "GateNegative");
            BoxCollider collider = gate.GetComponent<BoxCollider>();
            collider.isTrigger = true;
            collider.size = new Vector3(18f, 1f, 1f);
            gate.AddComponent<GateChoice>().GateValue = value;

            GameObject label = new GameObject("Gate Label");
            label.transform.SetParent(gate.transform, false);
            label.transform.localPosition = new Vector3(0f, 0.75f, -0.55f);
            label.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
            TextMesh text = label.AddComponent<TextMesh>();
            text.text = value;
            text.fontSize = 72;
            text.characterSize = 0.12f;
            text.anchor = TextAnchor.MiddleCenter;
            text.alignment = TextAlignment.Center;
            text.color = Color.white;
        }

        private static void CreateMaterial(string name, Color color)
        {
            string path = "Assets/ArmyRunner/Materials/" + name + ".mat";
            if (AssetDatabase.LoadAssetAtPath<Material>(path) != null) return;
            Shader shader = Shader.Find("Universal Render Pipeline/Lit");
            if (shader == null) shader = Shader.Find("Standard");
            AssetDatabase.CreateAsset(new Material(shader) { color = color }, path);
        }

        private static Material LoadMaterial(string name) =>
            AssetDatabase.LoadAssetAtPath<Material>("Assets/ArmyRunner/Materials/" + name + ".mat");

        private static void EnsureFolder(string path)
        {
            if (AssetDatabase.IsValidFolder(path)) return;
            string parent = Path.GetDirectoryName(path)?.Replace("\\", "/");
            string folder = Path.GetFileName(path);
            if (!string.IsNullOrEmpty(parent) && !AssetDatabase.IsValidFolder(parent)) EnsureFolder(parent);
            AssetDatabase.CreateFolder(parent, folder);
        }
    }
}
