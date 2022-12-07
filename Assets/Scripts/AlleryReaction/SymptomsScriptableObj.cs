using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Symptoms", menuName = "Symptoms/SymptomType")]
public class SymptomsScriptableObj : ScriptableObject
{
    public enum Reaction
    {
        Itching,
        Wheezing,
        Vomiting,
        Swelling,
        Anaphylaxis
    }

    public enum Remedy
    {
        Water,
        Ice,
        Cream,
        Pill,
        Rest,
        Inhaler,
        EpiPen,
        Ambulance
    }

    public int dangerLvl;
    public Reaction symptomType;
    public List<Remedy> remedies;
}
