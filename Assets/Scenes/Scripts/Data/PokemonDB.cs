using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonDB 
{
    static Dictionary<string, PokemonBase> pokemons;

    public static void Init()
    {
        pokemons = new Dictionary<string, PokemonBase>();

       var pokemonArray = Resources.LoadAll<PokemonBase>("");
       foreach (var pokemon in pokemonArray)
       {
           if(pokemons.ContainsKey(pokemon.Name))
            {
                Debug.LogError($"2+ pokemon of the name {pokemon.Name} exist");
                continue;
            }
           pokemons[pokemon.Name] = pokemon;
       }
    }

    public static PokemonBase GetPokemonByName(string name)
    {
        if (!pokemons.ContainsKey(name))
        {
            Debug.LogError($"No Pokemon with name {name} exists in database");
            return null;
        }

        return pokemons[name];

    }
}
