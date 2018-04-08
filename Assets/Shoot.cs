using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Shoot : NetworkBehaviour
{
    public Transform bulletSpawn;

    public Pool pool;

    public float speed;
    private float force = 0;
    //public float deathTimer    
    public float travelX;

    private float ySpeed;
    private float xSpeed;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        if (!Input.GetButton("Fire1") && force > 0)
        {
            if (force < 0.5)
            {
                force = 1f;
            }
            Vector3 mousePos = Input.mousePosition;
            Vector3 objectPos = Camera.main.WorldToScreenPoint(bulletSpawn.transform.position);
            CmdFire(force, mousePos, objectPos);
            force = 0;
        }
        else if (Input.GetButton("Fire1"))
        {
            force += speed * Time.deltaTime;
        }
    }
    [Command]
    private void CmdFire(float force, Vector3 mousePos, Vector3 objectPos)
    { 
        GameObject bullet = pool.GetObject();
 //       Vector3 mousePos = Input.mousePosition;
        
 //       Vector3 objectPos = Camera.main.WorldToScreenPoint(bulletSpawn.transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;
        
        float relativeX = mousePos.x - bulletSpawn.position.x;
        float relativeY = mousePos.y - bulletSpawn.position.y;
        
        bullet.transform.position = bulletSpawn.position;
        bullet.transform.rotation = bulletSpawn.rotation;
        bullet.SetActive(true);
        NetworkServer.Spawn(bullet);
        bool xPositive;
        bool yPositive;
        if (relativeX < 0){
            relativeX = relativeX * -1;
            xPositive = false;
        }
        else{
            xPositive = true;
        }
        if (relativeY < 0){
            relativeY = relativeY * -1;
            yPositive = false;
        }
        else{
            yPositive = true;
        }

        float yRelativeX = relativeY / relativeX;
 
        if (yRelativeX == 1 || yRelativeX>2)
        {
            ySpeed = CalculateFastestSpeed(force, yRelativeX);
            xSpeed = force - ySpeed;
        }
        else if (yRelativeX > 1)
        {
            xSpeed = CalculateFastestSpeed(force, 1 / yRelativeX);
            ySpeed = force - xSpeed;
        }
        else if (yRelativeX < 1)
        {
            if (yRelativeX <0.5) {
                xSpeed = CalculateFastestSpeed(force, 1 / yRelativeX);
                ySpeed = force - xSpeed;
            }
            else
            {
                xSpeed = CalculateFastestSpeed(force, 1 / yRelativeX);
                ySpeed = force - xSpeed;
            }
        }
        /*
        if (relativeX < 0)
        {
            bullet.GetComponent<Rigidbody>().velocity = new Vector3(-speed, ySpeed*-speed);
        }
        else
        {
            bullet.GetComponent<Rigidbody>().velocity = new Vector3(speed, ySpeed* speed);
        }*/
        if (!xPositive)
        {
            xSpeed = xSpeed * -1;
        }
        if (!yPositive)
        {
            ySpeed = ySpeed * -1;
        }
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(xSpeed, ySpeed);
        //bullet.transform.forward * 100;
        float traveldistance = travelX / force;
        StartCoroutine(LateCall(traveldistance, bullet));
    }
    private float CalculateFastestSpeed(float force, float highestNumber)
    {
        return force - (force / (highestNumber + 1));
    }

    IEnumerator LateCall(float sec, GameObject bullet)
    {
        yield return new WaitForSeconds(sec);
        bullet.SetActive(false);
    }

}
