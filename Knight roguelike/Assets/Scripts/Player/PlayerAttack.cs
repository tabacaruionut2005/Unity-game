using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    private CharacterController controller;
    private Animator animator;
    private InputManager inputManager;

    public float attackDistance = 3f;
    public float attackDelay = 0.4f;
    public float attackSpeed = 1f;
    public int attackDamage = 1;
    public LayerMask attackLayer;

    bool attacking = false;
    bool readyToAttack = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float attackValue = inputManager.onFoot.Attack.ReadValue<float>();
        bool isHeld = attackValue > 0.5f;

        animator.SetBool("isAttacking", isHeld);
        if (isHeld)
        {
            Attack();
        }
    }

    public void Attack()
    {
        animator.SetBool("isWalking", false);

        if (!readyToAttack || attacking) return;

        readyToAttack = false;
        attacking = true;

        if(animator.GetInteger("attackCount") == 0 && animator.GetBool("isAttacking"))
        {
            animator.SetBool("isAttacking", true);
            animator.SetInteger("attackCount",1);
        }
        else if(animator.GetInteger("attackCount") != 0)
        {
            animator.SetBool("isAttacking", true);
            animator.SetInteger("attackCount", 0);
        }

        Invoke(nameof(ResetAttack), attackSpeed);
        Invoke(nameof(AttackRaycast),attackDelay);
    }

    void ResetAttack()
    {
        attacking = false;
        readyToAttack = true;
        animator.SetBool("isAttacking", false);
        animator.SetInteger("attackCount", 0);
    }

    void AttackRaycast()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, attackDistance, attackLayer))
        {
            Debug.Log("Hit " + hit.collider.name);
        }
    }

}
