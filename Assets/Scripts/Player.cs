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
    [SerializeField] private float Damage;
    private Vector3 mousePos;
    [SerializeField] private GameObject meleeHitbox;
    [SerializeField] private GameObject rangedHitbox;
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
        movement.z = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                mousePos = hit.point;
            }
            Debug.Log("Mouse Position: " + mousePos);

            GameObject shot = Instantiate(rangedHitbox, transform.position, transform.rotation);
            shot.GetComponent<ProjectileMovement>().SetTarget(mousePos);
        }

        
    }

    private void FixedUpdate()
    {
        //--------Movement-------------
        playerRB.MovePosition(playerRB.position + movement * movementSpeed * Time.fixedDeltaTime);
    }
}
