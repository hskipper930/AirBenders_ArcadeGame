using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //General Parameters
    private Rigidbody playerRB;
    [SerializeField] private Camera cam;

    //--------Movement Parameters------------
    #region MovementParameters
    [SerializeField] private float movementSpeed;
    private Vector3 movement;
    #endregion MovementParameters

    //----------Combat Parameters--------------
    #region CombatParameters
    [SerializeField] private int health;
    [SerializeField] private int damage;
    [SerializeField] private int defense;
    private Vector3 mousePos;
    [SerializeField] private float meleeAttackRange;
    public LayerMask enemyLayer;
    #endregion CombatParameters

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //----------Movement--------------
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        Ray lookRay = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit look;
        if (Physics.Raycast(lookRay, out look, 100))
        {
            mousePos = look.point;
        }

        if (Input.GetMouseButtonDown(0))
        {
            //GameObject shot = Instantiate(rangedHitbox, transform.position, transform.rotation);
            //shot.GetComponent<ProjectileMovement>().SetTarget(mousePos);
            ProjectileObjectPooling.ActivateProjectile(transform.position, mousePos, "friendly", damage);
        }

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        //--------Movement-------------
        playerRB.MovePosition(playerRB.position + movement * movementSpeed * Time.fixedDeltaTime);

        if(transform.position.x > 35)
        {
            playerRB.AddForce(new Vector3 (-35, 0));
        }
        if (transform.position.x < -35)
        {
            playerRB.AddForce(new Vector3(35, 0));
        }
        if (transform.position.y > 22)
        {
            playerRB.AddForce(new Vector3(0, -35));
        }
        if (transform.position.y < -22)
        {
            playerRB.AddForce(new Vector3(0, 35));
        }

        //Vector3 lookDir = mousePos - playerRB.position;
        //float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        //Quaternion lookRot = Quaternion.LookRotation(mousePos, Vector3.forward);
        //playerRB.rotation = lookRot;
    }

    private void Attack()
    {
        float mR = meleeAttackRange;
        Vector3 meleeRange;
        Vector3 attackPoint = transform.position + new Vector3(mR, 0, 0);
        Collider[] hitEnemies = Physics.OverlapBox(attackPoint, meleeRange = (attackPoint + new Vector3(mR / 2, mR / 2, 0)), transform.rotation);

        foreach (var enemy in hitEnemies)
        {
            Debug.Log("EnemyHit");
            enemy.GetComponent<EnemyAI>().TakeDamage(damage);
        }
    }

    //------------TAKE DAMAGE FUNCTION-----------------
    public void TakeDamage(int dmg)
    {
        dmg -= defense;
        health -= dmg;
        if(health <= 0)
        {
            //GAME OVER (Call Game Manager for Game Over)
        }
    }

    //-----------PowerUps

    //Collisiion
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit Trigger");
        switch (other.tag)
        {
            case "PowerUp/SpeedUp":
                movementSpeed += 0.1f;
                Destroy(other.gameObject);
                break;
            case "PowerUp/DamageUp":
                damage += 2;
                Destroy(other.gameObject);
                break;
        }
    }
}
