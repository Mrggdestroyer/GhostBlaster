using UnityEngine;

public class RayGun : MonoBehaviour
{
    public LayerMask layerMask;
    public OVRInput.RawButton shootingButton;
    public LineRenderer linePrefab;
    public GameObject rayImpactPrefab;
    public Transform shootingPoint;
    public float maxLineDistance;
    public float lineShowTimer = 0;
    public float rayImpactShowTimer = 1;

    public AudioSource Source;
    public AudioClip Clip;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(OVRInput.GetDown(shootingButton))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        Source.PlayOneShot(Clip);

        Ray ray =  new Ray(shootingPoint.position, shootingPoint.forward);
        bool hasHit = Physics.Raycast(ray, out RaycastHit hit, maxLineDistance, layerMask);

        Vector3 endPoint = Vector3.zero;

        if(hasHit)
        {
            endPoint = hit.point;

            Ghost ghost = hit.transform.GetComponentInParent<Ghost>();

            if(ghost)
            {
                ghost.TakeDamage();
            }
            else
            {
                Quaternion rayImpactRotation = Quaternion.LookRotation(-hit.normal);

                GameObject rayImpact = Instantiate(rayImpactPrefab, hit.point, rayImpactRotation);
                Destroy(rayImpact, rayImpactShowTimer);
            }


        }
        else
        {
            endPoint = shootingPoint.position + shootingPoint.forward * maxLineDistance;
            Quaternion rayImpactRotation = Quaternion.LookRotation(-hit.normal);
            GameObject rayImpact = Instantiate(rayImpactPrefab, hit.point, rayImpactRotation);
        }



        LineRenderer line = Instantiate(linePrefab);
        line.positionCount = 2;
        line.SetPosition(0, shootingPoint.position);

        

        line.SetPosition(1, endPoint);

        Destroy(line.gameObject, lineShowTimer);
    }
}
