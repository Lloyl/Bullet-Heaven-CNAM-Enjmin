using UnityEngine;
using UnityEditor;

// Adding a button to the inspector to generate a new noise map
 
[CustomEditor(typeof(MapGenerator))] 
public class NewMonoBehaviourScript : Editor {
    public override void OnInspectorGUI(){
        MapGenerator mapGen = (MapGenerator)target; 

        if (DrawDefaultInspector()){ // if any scale, width, height value is changed
            if (mapGen.autoUpdate){ // and autoUpdate is true we redraw the map
                mapGen.GenerateMap();
            }
        };

        if (GUILayout.Button("Generate")){
            mapGen.GenerateMap();   
        }
    }
}
