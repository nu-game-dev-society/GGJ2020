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
    [SerializeField] Bubble complaintBubble;
    [SerializeField] Bubble orderBubble;

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
        //If not set in editor, find bubbles
        if (complaintBubble == null) complaintBubble = GetComponentsInChildren<Bubble>()[0];
        if (orderBubble == null) orderBubble = GetComponentsInChildren<Bubble>()[1];

        //-----
        JoinQueue();
        drunkness = Random.Range(0, 0.5f - Random.Range(0.0f, 0.5f));
    }
    private void OnEnable()
    {
        happiness = 1;
    }
    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Idle", agent.velocity.sqrMagnitude < 0.1f);


        if (agent.enabled && agent.remainingDistance <= 0.1f && target != null && target.isSeat)
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
                target.GetComponentInChildren<ServiceTriggers>().cust = this;
            }
        }
        if(atBar && serviceComplete)
        {
            atBar = false;
            SetTarget(CustomerManager.Instance.RequestIdleSpot());
            StartCoroutine(UpdateDrunkness(0.3f));
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
        if (agent.enabled == false)
            return;
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
        complaintBubble.Clear();
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

    // ********************************************************************************
    // Complaints *********************************************************************
    // ********************************************************************************
    #region Complaints
    public void SetComplaint(Complaint complaint)
    {
        //Redundant but defensive
        //At this point, we know customer is ready for new complaint
        ClearComplaint();

        //If new complaint is valid
        if (complaint!=null)
        {
            this.complaint = complaint;
            DrawComplaint();
        }        
    }

    void ClearComplaint()
    {
        this.complaint = null;
        complaintBubble.Clear();        
    }

    void DrawComplaint()
    {
        //Collect materials
        List<Material> materials = new List<Material>() { complaint.material };

        //Send materials to bubble
        complaintBubble.Draw(materials);
    }
    #endregion Complaints

    // ********************************************************************************
    // Orders *************************************************************************
    // ********************************************************************************
    #region Orders
    public void SetOrder(Order order)
    {
        //Redundant but defensive
        //At this point, we know customer is ready for new order
        ClearOrder();

        //If new order is valid
        if (order != null)
        {
            this.order = order;
            DrawOrder();
        }
    }

    void ClearOrder()
    {
        this.order = null;
        orderBubble.Clear();
    }

    public void DrawOrder()
    {
        //Collect materials
        List<Material> materials = new List<Material>();
        order.items.ForEach(i => materials.Add(i.material));

        //Send materials to bubble
        orderBubble.Draw(materials);
    }
    #endregion Orders
}
