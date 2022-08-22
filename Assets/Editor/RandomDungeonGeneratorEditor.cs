using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AbstractRandomGenerator), true)]
public class RandomDungeonGeneratorEditor : Editor
{
    AbstractRandomGenerator generator;
    private void Awake()
    {
        generator = (AbstractRandomGenerator)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Create dungeon"))
            generator.GenerateDungeon();
    }
}
