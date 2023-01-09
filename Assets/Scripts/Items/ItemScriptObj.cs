using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/NewItem")]

public class ItemScriptObj : ScriptableObject
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
        Pill1,
        Pill2,
        Inhaler,
        EpiPen,
        Ball,
        Meat,
        Snack,
        Peanut,
        Cashew,
        Wheat,
        Milk,
        Bone,
        Bacon
    }

    public Sprite itemSprite;
    public GameObject itemGameObject;
    public ItemType itemType;

    [HideInInspector] public bool canBuy;
    [HideInInspector] public Ingredient ingredients;
    
    //if it's a remedy
    [HideInInspector] public bool isRemedy;//if ticked yes then show next
    [HideInInspector] public Symptoms.Reactions[] reactionType = new Symptoms.Reactions[5];//how much is it gonna help with each symptom
    [HideInInspector] public float[] successRate = new float[5];//how much is it gonna help with each symptom
    
    //if it's an allergen
    [Space]
    public bool isAllergen;

    [Space] 
    [HideInInspector] public bool isFood;
    [HideInInspector] public float relievesHunger;
    
    [Space] 
    [HideInInspector] public bool isDrink;
    [HideInInspector] public float relievesThirst;
    #region Editor
#if UNITY_EDITOR
    
    /// <summary>
    /// Making the script easier to understand in the editor. Nothing else that changes the actual script
    /// </summary>
    [CustomEditor(typeof(ItemScriptObj))]
    public class ItemEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            ItemScriptObj script = (ItemScriptObj)target;

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
                    script.reactionType[i] = (Symptoms.Reactions)EditorGUILayout.EnumPopup(script.reactionType[i], GUILayout.MaxWidth(100));
                    script.reactionType[i] = (Symptoms.Reactions) i;
                    script.successRate[i] = EditorGUILayout.Slider("Success rate",script.successRate[i], 0, 100);
                    EditorGUILayout.EndHorizontal();
                }
            }
            
            script.isFood = EditorGUILayout.Toggle("Is Food", script.isFood);
            
            if (script.isFood) // if bool is true, show other fields
            {
                    EditorGUILayout.BeginHorizontal();
                    script.relievesHunger = EditorGUILayout.Slider("Relieves Hunger",script.relievesHunger, 0, 100);
                    EditorGUILayout.EndHorizontal();
                
            }
            
            script.isDrink = EditorGUILayout.Toggle("Is Drink", script.isDrink);
            
            if (script.isDrink) // if bool is true, show other fields
            {
                EditorGUILayout.BeginHorizontal();
                script.relievesThirst = EditorGUILayout.Slider("Relieves Thirst",script.relievesThirst, 0, 100);
                EditorGUILayout.EndHorizontal();
                
            }
        }
    }
#endif
    #endregion
}