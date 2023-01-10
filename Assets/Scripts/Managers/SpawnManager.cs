using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject petPrefab;
    [SerializeField] private List<GameObject> petsObj;
    [SerializeField] private List<Vector3> previousPositions;

    [SerializeField] private GameObject statsBarParent;
    [SerializeField] private GameObject statsBarPrefab;

    [SerializeField] private LayerMask groundMask;

    public GameObject camHolder;

    [SerializeField] private int intervals = 5;

    private void Start()
    {
        for(int i = 0; i <= MyPets.petsChosen.Count - 1; i++)
        {
            var currentPet = MyPets.petsChosen[i];
            var petClone = Instantiate(petPrefab, transform.position, Quaternion.identity);
            var cloneStats = petClone.GetComponent<CharacterStats>();
            var agent = petClone.GetComponent<NavMeshAgent>();

            cloneStats.petID = currentPet.petID;
            cloneStats.atention = currentPet.attentionSpan;
            cloneStats.energy = currentPet.energy;
            cloneStats.curiosity = currentPet.curiosity;

            cloneStats.allergies = currentPet.allergies;

            //Vector3 pos = new Vector3(i * intervals, 0, 0);
            //petClone.transform.position = pos;

            var statsManager = petClone.GetComponent<Ch_StatsManager>();
            statsManager.id = currentPet.petID;
            statsManager.hungerMultiplier = currentPet.hunger;
            statsManager.thirstMultiplier = currentPet.thirst;
            statsManager.boredomeMultiplier = currentPet.boredom;
            statsManager.loveMultiplier = currentPet.love;

            var statsBarClone = Instantiate(statsBarPrefab, Vector3.zero, Quaternion.identity, statsBarParent.transform);
            var bar = statsBarClone.GetComponent<StatsUIManager>();
            bar.id = currentPet.petID;
            bar.petStats = cloneStats;
            bar.pet = petClone.GetComponent<StateManager>();
            bar.camHolder = camHolder.GetComponent<CameraHandler>();

            petClone.GetComponent<StateManager>().petMenu = bar;

            petsObj.Add(petClone);
        }
    }
}
