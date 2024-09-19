using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] Text levelText;
    [SerializeField] HPBar hpBar;
    [SerializeField] Text statusText;

    [SerializeField] Color psnColor;
    [SerializeField] Color brnColor;
    [SerializeField] Color slpColor;
    [SerializeField] Color parColor;
    [SerializeField] Color frzColor;
    [SerializeField] Color toxColor;

    Pokemon _pokemon;

    Dictionary<ConditionID, Color> statusColors;

    public void SetData(Pokemon pokemon)
    {
        _pokemon = pokemon;

        nameText.text = pokemon.Base.Name;
        levelText.text = "Lvl " + pokemon.Level;
        hpBar.SetHP((float)pokemon.HP / pokemon.MaxHp);

        statusColors = new Dictionary<ConditionID, Color>()
        {
            {ConditionID.psn, psnColor},
            {ConditionID.tox, toxColor},
            {ConditionID.brn, brnColor},
            {ConditionID.par, parColor},
            {ConditionID.slp, slpColor},
            {ConditionID.frz, frzColor},
        };

        SetStatusText();
        _pokemon.OnStatusChanged += SetStatusText;
    }

    void SetStatusText()
    {
        if (_pokemon.Status == null)
        {
            statusText.text = "";
        }
        else
        {
            statusText.text = _pokemon.Status.ID.ToString().ToUpper();
            statusText.color = statusColors[_pokemon.Status.ID];
        }
    }

    public IEnumerator UpdateHP()
    {
        if (_pokemon.HPChanged)
        {
            yield return hpBar.SetHPSmooth((float)_pokemon.HP / _pokemon.MaxHp);
            _pokemon.HPChanged = false;
        }
    }
}