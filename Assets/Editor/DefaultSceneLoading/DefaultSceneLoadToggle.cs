using UnityEditor;

[InitializeOnLoad]
public static class DefaultSceneLoadToggle
{

    private const string MENU_NAME = "Tools/Load default scene";

    public static bool isEnable;
    //Called on load thanks to the InitializeOnLoad attribute
    static DefaultSceneLoadToggle()
    {
        isEnable = EditorPrefs.GetBool(MENU_NAME, false);

        //Delaying until first editor tick so that the menu
        //will be populated before setting check state, and
        //re-apply correct action
        EditorApplication.delayCall += () => {
            PerformAction(isEnable);
        };
    }

    [MenuItem(MENU_NAME)]
    private static void ToggleAction()
    {
        //Toggling action
        PerformAction(!isEnable);
    }

    public static void PerformAction(bool enabled)
    {
        //Set checkmark on menu item
        Menu.SetChecked(MENU_NAME, enabled);
        //Saving editor state
        EditorPrefs.SetBool(MENU_NAME, enabled);

        isEnable = enabled;

        //Perform your logic here...
    }
}