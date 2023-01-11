using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Individual : State, IDropHandler
{
    private bool petTendedTo;
    private bool petLoved;
    [HideInInspector]public bool petTreated;
    private bool itemGiven;
    private GridItem itemDropped;
    private Ch_StatsManager chStats;
    private GameViewManager gvm;
    public override State Act(StateManager manager, CharacterStats stats)
    {
        if(!stats.overide && !petTendedTo)
        {
            manager.animControl.SetBool("Idle", true);
            manager.animControl.SetBool("Eat", false);
            manager.animControl.SetBool("Sit", false);
            manager.animControl.SetBool("Walk", false);
            manager.animControl.SetBool("Smell", false);
            manager.animControl.SetBool("Dig", false);
            manager.animControl.SetBool("Play", false);
            manager.animControl.SetBool("Sleep", false);
            manager.animControl.SetBool("Need", false);
            manager.animControl.SetBool("Allergy", false);
            manager.animControl.SetBool("Pet", false);

            manager.agent.isStopped = true;
            manager.agent.velocity = Vector3.zero;
            manager.agent.SetDestination(manager.agent.transform.position);

            manager.currentTime = 0.0f;
            
        }

        //Create multiple checks
        //Check one: if the pet was fed. then play anim and call eat function
        //Check two: if the user played with the pet, then decrease boredom and play playing animation
        //Check three: if an allergy remedy was given, then play animation and deactivate bools
        //Check four: if the pet was pet, then play animation again
        //la sfarsit de fiecare check, trb return manager.restState + manager.needState.doOnce = false; + agent.isStopped = false + manager.agent.updateRotation = true;
        if (itemGiven)
        {
            StartCoroutine(IndividualScreen(stats,manager));
            itemGiven = false;
            
        }

        if (chStats.isLoved && petLoved)
        {
            StartCoroutine(lovePET(stats, manager));
            petLoved = false;
        }

        return this;
    }

    private void Awake()
    {
        chStats = GetComponent<Ch_StatsManager>();
        gvm = FindObjectOfType<GameViewManager>();
    }

    //when food/other items have been dropped on it in the 1 on 1 screen
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && eventData.pointerDrag.gameObject.layer == 5)
        {
            itemGiven = true;
            itemDropped = eventData.pointerDrag.gameObject.GetComponent<GridItem>();
        }
    }
    
    //When mouse is dragging on top of the pet, you're petting it
    private void OnMouseOver()
    {
        if (gvm.switchView)
        {
            if (Input.GetMouseButtonDown(0) && !Inventory.instance.draggingItem)
            {
                petTendedTo = true;
                chStats.isLoved = true;
                petLoved = true;
            }

            if (!Input.GetMouseButton(0))
            {
                chStats.isLoved = false;
                petLoved = false;
            }
        }
        
    }
    
    private void OnMouseEnter()
    {
        if (gvm.switchView)
        {
            if (Input.GetMouseButton(0) && !Inventory.instance.draggingItem)
            {
                petTendedTo = true;
                petLoved = true;
                chStats.isLoved = true;
                print("petting");
            }
            else
            {
                chStats.isLoved = false;
            }
        }
    }

    private void OnMouseExit()
    {
        chStats.isLoved = false;
    }

    IEnumerator IndividualScreen(CharacterStats stats, StateManager manager)
    {
        if (itemDropped.itemObj.isRemedy)
        {
            petTendedTo = true;
            stats.allergicReaction = false;
            stats.overide = false;
            manager.animControl.SetBool("Eat", true);
            manager.animControl.SetBool("Idle", false);
            manager.animControl.SetBool("Sleep", false);
            manager.animControl.SetBool("Sit", false);
            manager.animControl.SetBool("Walk", false);
            manager.animControl.SetBool("Smell", false);
            manager.animControl.SetBool("Dig", false);
            manager.animControl.SetBool("Play", false);
            manager.animControl.SetBool("Need", false);
            manager.animControl.SetBool("Allergy", false);
            manager.animControl.SetBool("Pet", false);
            yield return new WaitForSeconds(1);
            float currAnimLengt = manager.animControl.GetCurrentAnimatorClipInfo(0)[0].clip.length;
            yield return new WaitForSeconds(currAnimLengt * 2);
            stats.allergicReaction = true;
            stats.overide = true;
            
            if (CanTreatPet(itemDropped.itemObj, stats))
            {
                stats.allergicReaction = false;
                stats.overide = false;
                
                manager.animControl.SetBool("Sleep", true);
                manager.animControl.SetBool("Idle", false);
                manager.animControl.SetBool("Eat", false);
                manager.animControl.SetBool("Smell", false);
                manager.animControl.SetBool("Walk", false);
                manager.animControl.SetBool("Sit", false);
                manager.animControl.SetBool("Dig", false);
                manager.animControl.SetBool("Play", false);
                manager.animControl.SetBool("Need", false);
                manager.animControl.SetBool("Allergy", false);
                manager.animControl.SetBool("Pet", false);

                petTreated = true;
                gvm.switchView = false;
                gvm.currentTime = 0;
                manager.currentState = manager.restState;
            }
            else
            {
                stats.allergicReaction = true;
                stats.overide = true;
                manager.animControl.SetBool("Allergy", true);
                manager.animControl.SetBool("Idle", false);
                manager.animControl.SetBool("Eat", false);
                manager.animControl.SetBool("Sleep", false);
                manager.animControl.SetBool("Walk", false);
                manager.animControl.SetBool("Smell", false);
                manager.animControl.SetBool("Dig", false);
                manager.animControl.SetBool("Play", false);
                manager.animControl.SetBool("Need", false);
                manager.animControl.SetBool("Sit", false);
                manager.animControl.SetBool("Pet", false);
            }
            
            petTendedTo = false;
        }
        if (itemDropped.itemObj.isFood || itemDropped.itemObj.isDrink || itemDropped.itemObj.forPlay)
        {
            petTendedTo = true;
            TendToPet(stats,itemDropped.itemObj);
            Inventory.instance.RemoveItem(itemDropped.gameObject);
            if(/*!stats.overide &&*/ petTendedTo)
            {
                if (itemDropped.itemObj.isFood || itemDropped.itemObj.isDrink)
                {
                    if (!itemDropped.itemObj.isRemedy)
                    {
                        manager.animControl.SetBool("Eat", true);
                        manager.animControl.SetBool("Idle", false);
                        manager.animControl.SetBool("Sit", false);
                        manager.animControl.SetBool("Walk", false);
                        manager.animControl.SetBool("Smell", false);
                        manager.animControl.SetBool("Dig", false);
                        manager.animControl.SetBool("Play", false);
                        manager.animControl.SetBool("Sleep", false);
                        manager.animControl.SetBool("Need", false);
                        manager.animControl.SetBool("Allergy", false);
                        manager.animControl.SetBool("Pet", false);
                    }
                }
                else
                {
                    manager.animControl.SetBool("Play", true);
                    manager.animControl.SetBool("Idle", false);
                    manager.animControl.SetBool("Eat", false);
                    manager.animControl.SetBool("Sit", false);
                    manager.animControl.SetBool("Walk", false);
                    manager.animControl.SetBool("Smell", false);
                    manager.animControl.SetBool("Dig", false);
                    manager.animControl.SetBool("Sleep", false);
                    manager.animControl.SetBool("Need", false);
                    manager.animControl.SetBool("Allergy", false);
                    manager.animControl.SetBool("Pet", false);
                }
                
                
                yield return new WaitForSeconds(1);//wait until animation changes
                float currAnimLength = manager.animControl.GetCurrentAnimatorClipInfo(0)[0].clip.length;
                yield return new WaitForSeconds(currAnimLength * 2);
                petTendedTo = false;
                chStats.eating = false;
                chStats.drinking = false;
            }
        }
    }

    IEnumerator lovePET(CharacterStats stats, StateManager manager)
    {
        if (chStats.isLoved)
        {
            int iterations = 0;
            
            manager.animControl.SetBool("Pet", true);
            manager.animControl.SetBool("Sleep", false);
            manager.animControl.SetBool("Idle", false);
            manager.animControl.SetBool("Eat", false);
            manager.animControl.SetBool("Sit", false);
            manager.animControl.SetBool("Walk", false);
            manager.animControl.SetBool("Smell", false);
            manager.animControl.SetBool("Dig", false);
            manager.animControl.SetBool("Play", false);
            manager.animControl.SetBool("Need", false);
            manager.animControl.SetBool("Allergy", false);
            while (chStats.isLoved && iterations < 1000)
            {
                iterations++;
                
                stats.love += 2 * chStats.loveMultiplier * Time.deltaTime ;
                print("stats.love");
                if (stats.love > 100)
                    stats.love = 100;
                chStats.loveLevel = stats.love;
                if (stats.love > 25)
                {
                    stats.wantsLove = false;
                    if(!stats.isThirsty && !stats.isHungry && !stats.isBored)
                        stats.overide = false;
                }
                yield return null;
            }

            yield return new WaitForSeconds(0.5f);
            petTendedTo = false;
        }
    }

    public void TendToPet(CharacterStats stats, ItemScriptObj item)
    {
        if (item.isFood)
        {
            chStats.eating = true;
            stats.hunger += item.relievesHunger;
            if (stats.hunger > 100)
                stats.hunger = 100;
            chStats.hungerLevel = stats.hunger;
            if (stats.hunger > 25)
            {
                stats.isHungry = false;
                if(!stats.isThirsty && !stats.isBored && !stats.wantsLove)
                    stats.overide = false;
            }
                
        }else if (item.isDrink)
        {
            chStats.drinking = true;
            stats.thirst += item.relievesThirst;
            if (stats.thirst > 100)
                stats.thirst = 100;
            chStats.thirstLevel = stats.thirst;
            if (stats.thirst > 25)
            {
                stats.isThirsty = false;
                if(!stats.isBored && !stats.isHungry && !stats.wantsLove)
                    stats.overide = false;
            }
                
        }else if (item.forPlay)
        {
            chStats.playing = true;
            stats.boredome += item.relievesBoredom;
            if (stats.boredome > 100)
                stats.boredome = 100;
            chStats.boredomeLevel = stats.boredome;
            if (stats.boredome > 25)
            {
                stats.isBored = false;
                if(!stats.isThirsty && !stats.isHungry && !stats.wantsLove)
                    stats.overide = false;
            }
                
        }
    }
    
    public bool CanTreatPet(ItemScriptObj item, CharacterStats stats)
    {
        if (!stats.allergicReaction)
            return false;

        //check what type of reaction you are having
        Symptoms.Reactions currentReaction = stats.currentReaction.symptom;

        switch (currentReaction)
        {
            case Symptoms.Reactions.Itching:
                if (item.successRate[0] > 0)
                    return true;
                break;
            case Symptoms.Reactions.Wheezing:
                if (item.successRate[1] > 0)
                    return true;
                break;
            case Symptoms.Reactions.Vomiting:
                if (item.successRate[2] > 0)
                    return true;
                break;
            case Symptoms.Reactions.Swelling:
                if (item.successRate[3] > 0)
                    return true;
                break;
            case Symptoms.Reactions.Anaphylaxis:
                if (item.successRate[4] > 0)
                    return true;
                break;
        }

        return false;
    }
    
}
