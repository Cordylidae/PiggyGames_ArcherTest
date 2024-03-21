using UnityEngine;
using UnityEngine.UI;

public class Archer : MonoBehaviour
{
    [SerializeField] private Transform bowNeedPosition;
    [SerializeField] private Transform bowTransform;
    [SerializeField] private FindTarget FindTarget;
    
    [SerializeField] private GameObject Arrow;
    [SerializeField] private Transform Arrows;
   
    [SerializeField] private float AngleInDegrees;
    private Transform Target = null;

    private float g;
    [SerializeField] private float gravityScaler = 1.4f;
    [SerializeField] Toggle isFire;

    [Header("Shot time")]
    [SerializeField] private float shotDelta = 0.917f;
    [SerializeField] private float speedUpShot = 1.0f;
    private float t = 0.0f;

    [Header("Hero")]
    //[SerializeField] private int faceDir = 1;
    [SerializeField] private Transform Roteted;
    [SerializeField] private Transform allObject;
    [SerializeField] private Animator heroAnim;

    [Header("Traectory View")]
    [SerializeField] private GameObject dot;
    private GameObject[] dots;
    [SerializeField] private int countDot;
    [SerializeField] private float distBetween;
    [SerializeField] private GameObject Traectory;

    private Vector3 direction;

    private void Start()
    {
        g = Physics2D.gravity.y;

        dots = new GameObject[countDot];
        for (int i = 0; i < countDot; i++)
        {
            dots[i] = Instantiate(dot, bowTransform.position, Quaternion.identity);
            dots[i].transform.parent = Traectory.transform;
        }
    }
    
    void Update()
    {
        if (Target != null)
        {
            //if (faceDir == -1) allObject.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            //else if (faceDir == 1) allObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            
            if (!ICanShot()) return;

            AngleInDegrees = Vector2.Angle(Vector2.right, (Target.position - allObject.position).normalized);

            if (Target.position.x < allObject.position.x)
            {
                Roteted.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

                bowTransform.localEulerAngles = new Vector3(0.0f, 0.0f, AngleInDegrees);
                Roteted.localEulerAngles = new Vector3(0.0f, 0.0f, 180 + AngleInDegrees);
            }
            else
            {
                Roteted.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                bowTransform.localEulerAngles = new Vector3(0.0f, 0.0f, AngleInDegrees);
                Roteted.localEulerAngles = new Vector3(0.0f, 0.0f, AngleInDegrees);
            }

            Traectory.SetActive(true);
            MakeShot();
        }
        else
        {
            Target = FindTarget.GetTarget();

            EndShot();
        }
    }

    private bool ICanShot()
    {
        if (t > 0.0f)
        {
            t -= Time.deltaTime; return false;
        }
        else t = shotDelta / speedUpShot; return true;
    }

    private void MakeShot()
    {
        heroAnim.SetBool("endShot", false);
        heroAnim.speed = speedUpShot;
        heroAnim.Play("Shot");

        transform.position = new Vector3(bowNeedPosition.position.x, bowNeedPosition.position.y, transform.position.z);

        float v = Shot();

        for (int i = 0; i < countDot; i++)
        {
            dots[i].transform.position = DotPosition(i * distBetween, v);
        }

    }

    private void EndShot()
    {
        heroAnim.SetBool("shot", false);
        heroAnim.SetBool("endShot", true);

        Roteted.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);

        heroAnim.speed = 1;

        Traectory.SetActive(false);
    }

    public float Shot()
    {
        direction = Target.position - transform.position;
        float x = direction.x;
        float y = direction.y;

        float AngleInRadians = AngleInDegrees * Mathf.PI / 180;

        float v2 = (gravityScaler * g * x * x) / (2 * (y - Mathf.Tan(AngleInRadians) * x) * Mathf.Pow(Mathf.Cos(AngleInRadians), 2));
        float v = Mathf.Sqrt(Mathf.Abs(v2));

        GameObject arrow = Instantiate(Arrow, bowTransform.position, Quaternion.identity);
        arrow.GetComponent<Rigidbody2D>().velocity = bowTransform.right * v;
        arrow.GetComponent<Rigidbody2D>().gravityScale = gravityScaler;
        
        arrow.GetComponent<Arrow>().inFire = isFire.isOn;
        arrow.GetComponent<IDamage>().isFire = isFire.isOn;

        arrow.transform.parent = Arrows;

        return v;
    }

    private Vector2 DotPosition(float t, float v)
    {
        Vector2 position = (Vector2)bowTransform.position + (Vector2)(bowTransform.right * t * v) + 0.5f * Physics2D.gravity * (t*t) * gravityScaler;
        return position;
    }
}
