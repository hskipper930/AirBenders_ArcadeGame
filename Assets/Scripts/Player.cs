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
    [SerializeField] private int HP;
    [SerializeField] private int damage;
    private Vector3 mousePos;
    [SerializeField] private GameObject meleeHitbox;
    [SerializeField] private GameObject rangedHitbox;
    [SerializeField] private float meleeAttackRange;
    public LayerMask enemyLayer;

    private bool damageUp = false;
    private bool speedUp = false;
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
        Debug.Log("Mouse Position: " + mousePos);

        if (Input.GetMouseButtonDown(0))
        {
            //GameObject shot = Instantiate(rangedHitbox, transform.position, transform.rotation);
            //shot.GetComponent<ProjectileMovement>().SetTarget(mousePos);
            //ProjectileObjectPooling.ActivateProjectile(transform.position, mousePos,);
        }

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log("Mouse Position: " + mousePos);
    }

    private void FixedUpdate()
    {
        //--------Movement-------------
        playerRB.MovePosition(playerRB.position + movement * movementSpeed * Time.fixedDeltaTime);

        //Vector3 lookDir = mousePos - playerRB.position;
        //float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        Quaternion lookRot = Quaternion.LookRotation(mousePos, Vector3.forward);
        playerRB.rotation = lookRot;
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

    public void SetDamageUp()
    {
        StartCoroutine(DamageUpTimer());
    }

    public void SetSpeedUp()
    {
        StartCoroutine(SpeedUpTimer());
    }

    private IEnumerator DamageUpTimer()
    {
        damage = 20;
        int timeLeft = 10;
        while(timeLeft >= 0)
        {
            Debug.Log("PowerUp still going");
            yield return new WaitForSeconds(1.0f);
            timeLeft--;
        }
        damage = 10;
    }

    private IEnumerator SpeedUpTimer()
    {
        float previousMoveSpeed = movementSpeed;
        movementSpeed = 7.5f;
        int timeLeft = 10;
        while (timeLeft >= 0)
        {
            Debug.Log("PowerUp still going");
            yield return new WaitForSeconds(1.0f);
            timeLeft--;
        }
        movementSpeed = previousMoveSpeed;
    }
}
