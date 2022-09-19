using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEditorInternal;
using JetBrains.Annotations;

/**
 *  Script that gives access to a new Editor Window allowing for the creation of Factories
 * 
 *  @author : Henri 'Biscuit Prime' Nomico
 */
public class FactoryCreatorWindow : EditorWindow
{
    public string factoryName="Modular Factory"; //name given to the created Factory gameobject
    private GameObject factoryObject; //created factory gameobject
    
    //Function used to allow the creation of the window :
    [MenuItem("Window/FactoryCreator")]
    public static void DisplayWindow()
    {
        GetWindow<FactoryCreatorWindow>("Factory Creator Window");
    } 

    //Main function containing the code of the Editor window used to create factories :
    private void OnGUI()
    {
        GUILayout.Label("Factory Creator Window",EditorStyles.boldLabel); //label of the window
        //basic explanatory text
        GUILayout.TextArea("You can create a Factory entity in the scene by clicking the following button. The factory comes with a Factory Component modifiable to your desires");
        
        factoryName = EditorGUILayout.TextField("Factory Name",factoryName);

        GUILayout.TextArea("Once you press the button, a factory gameObject with the name" + factoryName + "will be created in the scene, containing a Factory component, ready for use. You will still have to create the associated object pools in the editor of said gameobject");
        
        //button that will instantiate the prefab in the scene with the corresponding factory :
        if (GUILayout.Button("Press to create object"))
        {
            factoryObject = new GameObject(factoryName);
            factoryObject.AddComponent<Factory>();
        }
    }

    //function that keeps updating the editor window :
    private void OnInspectorUpdate()
    {
        Repaint();
    }
}
