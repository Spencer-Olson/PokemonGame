using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionsDB
{
    public static void Init()
    {
        foreach (var kvp in Conditions)
        {
            var conditionID = kvp.Key;
            var condtion = kvp.Value;

            condtion.ID = conditionID;
        }
    }

    public static Dictionary<ConditionID, Condition> Conditions { get; set; } = new Dictionary<ConditionID, Condition>()
    {
        {
            ConditionID.psn,
            new Condition()
            { 
                Name = "Poison",
                StartMessage = "has been poisoned.",
                OnAfterTurn = (Pokemon pokemon) => 
                {
                    pokemon.UpdateHP(pokemon.MaxHp / 8);
                    pokemon.StatusChanges.Enqueue($"{pokemon.Base.Name} is hurt by the poison.");
                }
            }
        },

        {
           ConditionID.brn,
           new Condition()
           {
               Name = "Burn",
               StartMessage = "has been burned.",
               OnAfterTurn = (Pokemon pokemon) =>
               {
                   pokemon.UpdateHP(pokemon.MaxHp / 16);
                   pokemon.StatusChanges.Enqueue($"{pokemon.Base.Name} is hurt by the burn.");
               }
           }
        },

       {
           ConditionID.par,
           new Condition()
           {
               Name = "Paralyzed",
               StartMessage = "is paralyzed! It may be unable to move!",
               OnBeforeMove = (Pokemon pokemon) =>
               {
                   if (Random.Range(1,5) == 1)
                   {
                       pokemon.StatusChanges.Enqueue($"{pokemon.Base.Name} is paralyzed! It can't move!");
                       return false;
                   }
                   return true;
               }
           }
        },

       {
           ConditionID.frz,
           new Condition()
           {
               Name = "Frozen",
               StartMessage = "was frozen solid!",
               OnBeforeMove = (Pokemon pokemon) =>
               {
                   if (Random.Range(1,5) == 1)
                   {
                       pokemon.CureStatus();
                       pokemon.StatusChanges.Enqueue($"{pokemon.Base.Name} thawed out!");
                       return true;
                   }
                   return false;
               }
           }
        },

       {
           ConditionID.slp,
           new Condition()
           {
               Name = "Sleep",
               StartMessage = "fell asleep!",
               OnStart = (Pokemon pokemon) =>
               {
                   //Sleep for 1-3 turns
                   pokemon.StatusTime = Random.Range(1, 4);
                   Debug.Log($"{pokemon.Base.Name} will be asleep for {pokemon.StatusTime} turns");
               },
               OnBeforeMove = (Pokemon pokemon) =>
               {
                   if (pokemon.StatusTime <= 0)
                   {
                       pokemon.CureStatus();
                       pokemon.StatusChanges.Enqueue($"{pokemon.Base.Name} woke up!");
                       return true;
                   }

                   pokemon.StatusTime--;
                   pokemon.StatusChanges.Enqueue($"{pokemon.Base.Name} is fast asleep.");
                   return false;
               }
           }
        },


       //Volatile Conditions
       {
           ConditionID.confusion,
           new Condition()
           {
               Name = "Confusion",
               StartMessage = "became confused!",
               OnStart = (Pokemon pokemon) =>
               {
                   //Confused for 1 - 4 turns
                   pokemon.VolatileStatusTime = Random.Range(1, 5);
                   Debug.Log($"{pokemon.Base.Name} will be confused for {pokemon.VolatileStatusTime} turns");
               },
               OnBeforeMove = (Pokemon pokemon) =>
               {
                   if (pokemon.VolatileStatusTime <= 0)
                   {
                       pokemon.CureVolatileStatus();
                       pokemon.StatusChanges.Enqueue($"{pokemon.Base.Name} snapped out of its confusion!");
                       return true;
                   }
                   pokemon.VolatileStatusTime--;


                   pokemon.StatusChanges.Enqueue($"{pokemon.Base.Name} is confused.");
                   //50% change to act
                   if(Random.Range(1,3) == 1)
                    return true; 

                   //Hurt by confusion
                   pokemon.UpdateHP(pokemon.MaxHp / 8);
                   pokemon.StatusChanges.Enqueue($"It hurt itself in its confusion!");
                   return false;
               }
           }
        }
    };

    public static float GetStatusBonus(Condition condition)
    {
        if (condition == null)
            return 1f;
        else if (condition.ID == ConditionID.slp || condition.ID == ConditionID.frz)
            return 2f;
        else if (condition.ID == ConditionID.brn || condition.ID == ConditionID.par || condition.ID == ConditionID.psn || condition.ID == ConditionID.tox)
            return 1.5f;

        return 1f;
    }
}

public enum ConditionID 
{
    none, psn, brn, slp, par, frz,
    confusion, tox
}