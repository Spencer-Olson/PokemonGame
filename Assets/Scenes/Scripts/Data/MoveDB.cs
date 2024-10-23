using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDB 
{
    static Dictionary<string, MoveBase> moves;
    public static void Init()
    {
        moves = new Dictionary<string, MoveBase>();

        var moveList = Resources.LoadAll<MoveBase>("");
        foreach (var move in moveList)
        {
            if (moves.ContainsKey(move.Name))
            {
                Debug.LogError($"2+ moves of the name {move.Name} exist");
                continue;
            }
            moves[move.Name] = move;
        }
    }

    public static MoveBase GetMoveByName(string name)
    {
        if (!moves.ContainsKey(name))
        {
            Debug.LogError($"No Move with name {name} exists in database");
            return null;
        }

        return moves[name];

    }
}
