using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainerController : MonoBehaviour, Interactable
{
    [SerializeField] string _name;
    [SerializeField] Sprite sprite;
    [SerializeField] Dialogue dialogue;
    [SerializeField] Dialogue dialogueAfterBattle;
    [SerializeField] GameObject exlamation;
    [SerializeField] GameObject fov;

    Character character;
    
    //State
    bool battleLost = false;

    private void Awake()
    {
        character = GetComponent<Character>();
    }

    private void Start()
    {
        SetFovRotation(character.Animator.DefaultDirection);
    }

    private void Update()
    {
        character.HandleUpdate();
    }
    public void Interact(Transform initiator)
    {
        character.LookTowards(initiator.position);
        if (!battleLost)
        {
            StartCoroutine(DialogueManager.Instance.ShowDialogue(dialogue, () =>
            {
                GameController.Instance.StartTrainerBattle(this);
            }));
        }
        else
            StartCoroutine(DialogueManager.Instance.ShowDialogue(dialogueAfterBattle));
    }
    public IEnumerator TriggerTrainerBattle(PlayerController player)
    {
        //Show Exclamation
        exlamation.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        exlamation.SetActive(false);

        //Walk towards the player
        var diff = player.transform.position - transform.position;
        var moveVec = diff - diff.normalized;
        moveVec = new Vector2(Mathf.Round(moveVec.x), Mathf.Round(moveVec.y));
        
        yield return character.Move(moveVec);

        //show dialogue
        StartCoroutine(DialogueManager.Instance.ShowDialogue(dialogue, () =>
        {
            GameController.Instance.StartTrainerBattle(this);
        }));
    }

    public void SetFovRotation(FacingDirection dir)
    {
        float angle = 0f;
        if (dir == FacingDirection.Right)
            angle = 90f;
        else if (dir == FacingDirection.Left)
            angle = 270f;
        else if (dir == FacingDirection.Up)
            angle = 180f;

        fov.transform.eulerAngles = new Vector3(0f, 0f, angle);
    }

    public void BattleLost()
    {
        battleLost = true;
        fov.gameObject.SetActive(false);
    }

    public string Name
    {
        get => _name;
    }

    public Sprite Sprite
    {
        get => sprite;
    }
}
