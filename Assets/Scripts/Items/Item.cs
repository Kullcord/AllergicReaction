using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/NewItem")]

public class Item : ScriptableObject
{
    public enum Ingredient
    {
        None,
        Water = 1 << 0,
        Nut = 1 << 1,
        Milk = 1 << 2,
        Egg = 1 << 3,
        Meat = 1 << 4,
        All = ~0
    }

    public enum ItemType
    {
        Water,
        Ice,
        Cream,
        Pill,
        Inhaler,
        EpiPen,
        Ball,
        Meat,
        Snack,
        Peanut,
        Cashew,
        Wheat,
        Milk
    }

    public Sprite itemSprite;
    public ItemType itemType;

    [HideInInspector] public bool canBuy;
    [HideInInspector] public Ingredient ingredients;
    
    //if it's a remedy
    [HideInInspector] public bool isRemedy;//if ticked yes then show next
    [HideInInspector] public SymptomsScriptableObj.Reaction[] reactionType = new SymptomsScriptableObj.Reaction[5];//how much is it gonna help with each symptom
    [HideInInspector] public float[] successRate = new float[5];//how much is it gonna help with each symptom
    
    //if it's an allergen
    [Space]
    public bool isAllergen;
    
    #region Editor
#if UNITY_EDITOR
    [CustomEditor(typeof(Item))]
    public class ItemEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            Item script = (Item)target;

            script.canBuy = EditorGUILayout.Toggle("Can Buy", script.canBuy);
            if(script.canBuy)
                script.ingredients = (Ingredient)EditorGUILayout.EnumFlagsField("Ingredients",script.ingredients);
            
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Cure Symptoms");
            // draw checkbox for the bool
            script.isRemedy = EditorGUILayout.Toggle("Is Remedy", script.isRemedy);
            
            
            if (script.isRemedy) // if bool is true, show other fields
            {
                for (int i = 0; i < script.reactionType.Length; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    script.reactionType[i] = (SymptomsScriptableObj.Reaction)EditorGUILayout.EnumPopup(script.reactionType[i], GUILayout.MaxWidth(100));
                    script.reactionType[i] = (SymptomsScriptableObj.Reaction) i;
                    script.successRate[i] = EditorGUILayout.Slider("Success rate",script.successRate[i], 0, 100);
                    EditorGUILayout.EndHorizontal();
                }
            }
        }
    }
#endif
    #endregion
}