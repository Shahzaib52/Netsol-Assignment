using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DroneController : MonoBehaviour, PlayerInfo
{

    [Range(-1, 1)]
    [SerializeField] private float Thrust, Tilt, Lift, Rotate;

    private Rigidbody rb;
    private Animator anim;

    [Space(10)]
    [SerializeField] private float lift = 5;
    [SerializeField] private float speed = 5;

    [SerializeField] private float rotationSpeed = 5;
    [SerializeField] private float blendSpeed = 2;

    [Space(10)]
    [SerializeField] private float angle;

    private Quaternion rotation;

    private void OnEnable()
    {
        Events.OnGetPlayerTarget += GetPlayerInfo;
    }

    private void OnDisable()
    {
        Events.OnGetPlayerTarget -= GetPlayerInfo;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Lift = Input.GetAxis("Lift");
        Rotate = Input.GetAxis("Rotate");
        Thrust = Input.GetAxis("Vertical");
        Tilt = Input.GetAxis("Horizontal");

        var v = anim.GetFloat("Vertical");
        var h = anim.GetFloat("Horizontal");
        var r = anim.GetFloat("Rotate");

        anim.SetFloat("Vertical", Mathf.Clamp(Mathf.Lerp(v, Thrust, blendSpeed * Time.deltaTime), -1, 1));
        anim.SetFloat("Horizontal", Mathf.Clamp(Mathf.Lerp(h, Tilt, blendSpeed * Time.deltaTime), -1, 1));
        anim.SetFloat("Rotate", Mathf.Clamp(Mathf.Lerp(r, Rotate, blendSpeed * Time.deltaTime), -1, 1));

        var dir = new Vector3(Tilt * speed, Lift * lift, Thrust * speed);

        rb.AddRelativeForce(dir, ForceMode.Impulse);

        var cv3 = transform.InverseTransformDirection(rb.velocity);

        cv3.x = Mathf.Clamp(cv3.x, -speed, speed);
        cv3.y = Mathf.Clamp(cv3.y, -lift, lift);
        cv3.z = Mathf.Clamp(cv3.z, -speed, speed);

        rb.velocity = transform.TransformDirection(cv3);

        angle += Input.GetAxis("Rotate") * rotationSpeed;

        if (angle >= 360)
            angle = 0;
        else if (angle <= -360)
            angle = 0;

        rotation = Quaternion.AngleAxis(angle, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    public Vector3 Position() => this.transform.position;
    public Vector3 Rotation() => this.transform.eulerAngles;
    public PlayerInfo GetPlayerInfo() => this;

    public void DealDamage(float damage)
    {
        //TODO: implement player health
    }

    public bool IsAlive() => true;
}
