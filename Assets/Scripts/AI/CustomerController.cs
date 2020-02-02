using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerController : MonoBehaviour
{
    public Order order;
    public Complaint complaint;
    Animator animator;
    NavMeshAgent agent;
    public Waypoint target;
    public bool serviceComplete;
    public bool atBar;
    [Range(0.0f,1.0f)]
    public float drunkness;
    public float happiness;
    public float timeSinceLastDrink;
    [SerializeField]GameObject complaintBubble;

    public SkinnedMeshRenderer[] skinnedMeshs;

    public Waypoint exitWP;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        skinnedMeshs = GetComponentsInChildren<SkinnedMeshRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //-----
        JoinQueue();
    }
    private void OnEnable()
    {
        happiness = 1;
    }
    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Idle", agent.velocity.sqrMagnitude < 0.1f);


        if (agent.enabled && agent.remainingDistance <= 0.1f && target.isSeat)
        {
            Seat[] seats = target.GetComponent<Seating>().seats;
            foreach (Seat s in seats)
            {
                if (!s.occupied)
                {
                    Sit(s);
                    break;
                }
            }
            agent.enabled = false;
            
            //target = null;
        }

        if (target!=null)
        {
            Waypoint wp = target.EvaluateNext();

            if (wp != null)
                SetTarget(wp);

            if (agent.enabled && target.barSpot && agent.remainingDistance < 0.1f && !atBar)
            {
                atBar = true;
                target.GetComponentInChildren<ServiceTriggers>().cust = GetComponentInChildren<OrderBubble>();
            }
        }
        if(atBar && serviceComplete)
        {
            atBar = false;
            SetTarget(CustomerManager.Instance.RequestIdleSpot());
            StartCoroutine(UpdateDrunkness(0.08f));
        }
        if(!serviceComplete)
        {
            happiness -= (Time.deltaTime / 200);
            if (happiness < 0.3f)
                LeaveBar();
        }
        if(!atBar && serviceComplete)
        {
            timeSinceLastDrink += Time.deltaTime;
            if(timeSinceLastDrink > 120)
            {
                if (happiness > 0.3f)
                    FinishedDrink();
                else
                    LeaveBar();
            }
        }

    }
    Seat seat;
    void Sit(Seat s)
    {
        animator.SetBool("Sitting", true);
        transform.position = s.t.position;
        transform.rotation = s.t.rotation;
        s.occupied = true;
        target.Occupied = false;
        seat = s;
    }
    void Stand()
    {
        animator.SetBool("Sitting", false);
        transform.position = target.transform.position;
        agent.enabled = true;
        seat.occupied = false;
        seat = null;
    }
    IEnumerator UpdateDrunkness(float drunkDelta)
    {
        yield return new WaitForSeconds(5);
        drunkness += drunkDelta;
        drunkness = Mathf.Clamp(drunkness, 0, 1);
    }
    void SetTarget(Waypoint wp)
    {
        //Leave old wp
        if (target)
            target.Occupied = false;

        //Join new wp
        target = wp;
        agent.SetDestination(target.transform.position);
        target.Occupied = true;
    }
    void SetTargetIgnoreOccupied(Waypoint wp)
    {
        target = wp;
        agent.SetDestination(target.transform.position);
        target.Occupied = true;
    }

    void LeaveBar()
    {
        complaintBubble.SetActive(false);
        Destroy(GetComponentInChildren<OrderBubble>().gameObject);
        SetTargetIgnoreOccupied(exitWP);
    }
    IEnumerator Wait(float t)
    {
        yield return new WaitForSeconds(t);
        JoinQueue();
    }

    public void JoinQueue()
    {
        Waypoint wp = CustomerManager.Instance.RequestQueueSpot();
        if (wp == null)
        {
            wp = CustomerManager.Instance.RequestIdleSpot();
            StartCoroutine(Wait(10f));
        }
        SetTarget(wp);
    }
    public void SetDrunkness(float v)
    {
        drunkness = v;
        animator.SetFloat("Drunkness", drunkness);
    }



    [ContextMenu("Finish Drink")]
    public void FinishedDrink()
    {
        if(seat != null)
            Stand();
        serviceComplete = false;
        JoinQueue();
    }

    
    //Force update drunk
#if UNITY_EDITOR
    [ContextMenu("Update Drunkness")]
    public void UpdateDrunkness()
    {
        animator.SetFloat("Drunkness", drunkness);
    }
#endif

    public void SetComplaint(Complaint complaint)
    {
        if(complaint!=null)
        {
            this.complaint = complaint;
            for (int i = 0; i < complaintBubble.transform.childCount; i++)
                complaintBubble.transform.GetChild(i).gameObject.SetActive(false);

            complaintBubble.SetActive(true);            
            complaintBubble.transform.GetChild(complaint.id).gameObject.SetActive(true);            
        }
        else
        {
            for (int i = 0; i < complaintBubble.transform.childCount; i++)
                complaintBubble.transform.GetChild(i).gameObject.SetActive(false);
            complaintBubble.SetActive(false);
        }        
    }
}
