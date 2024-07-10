using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody rb;
    public Animator animator;
    public GameObject bullet;
    public GameObject gameManager;

    float bulletTimer = 0;

    private GameManagerScript gameManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        // Script���擾����
        gameManagerScript = gameManager.GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveSpeed = 2.0f;
        float stageMax = 4.0f;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if(transform.position.x<stageMax)
            {
                rb.velocity = new Vector3(moveSpeed, 0, 0);
                animator.SetBool("mode", true);
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (transform.position.x > -stageMax)
            {
                rb.velocity = new Vector3(-moveSpeed, 0, 0);
                animator.SetBool("mode", true);
            }
        }
        else
        {
            rb.velocity = new Vector3(0, 0, 0);
            animator.SetBool("mode", false);
        }
    }

    void FixedUpdate()
    {
        if (bulletTimer == 0)
        {
            // �e����
            if (Input.GetKey(KeyCode.Space))
            {
                Vector3 position = transform.position;
                position.y += 0.8f;
                position.z += 1.0f;
                Instantiate(bullet, position, Quaternion.identity);
                // ���˂�����^�C�}�[��1�ɂ���
                bulletTimer = 1;
            }
        }
        else
        {
            bulletTimer++;
            if(bulletTimer > 20)
            {
                bulletTimer = 0;
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            gameManagerScript.GameOverStart();
        }
    }
}
