using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
public class PickupPlayer : MonoBehaviour
{
    [Header("Links")]
    public Camera playerCamera;
    public Transform holdPoint;           
    public GameObject tochka;
    public GameObject textE;
    public GameObject voidphase;
    public GameObject Hint;
    public TextMeshProUGUI HintText;
    public GameObject HintTextObj;
    [Header("Settings")]
    public float distance = 5f;
    public float throwForce = 5f;
    public KeyCode pickKey = KeyCode.E;
    public KeyCode dropKey = KeyCode.Space;

    private PickupItem lookedItem;
    public PickupItem heldItem;

    public string iteminhand;

    void Update()
    {
        bool isHolding = heldItem != null;
        Hint.SetActive(isHolding);
        voidphase.SetActive(isHolding);

        RaycastLook();

        // Подбор
        if (Input.GetKeyDown(pickKey) && lookedItem != null && heldItem == null && !voidphase.activeSelf)
        {
            PickUp(lookedItem, lookedItem.ObjName);
        }

        // Бросок/дроп
        if (Input.GetKeyDown(dropKey) && heldItem != null)
        {
            Drop(heldItem);
        }
    }

    void RaycastLook()
    {
        lookedItem = null;

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, distance))
        {
            lookedItem = hit.collider.GetComponentInParent<PickupItem>();
        }

        // UI "E"
        bool canPick = (lookedItem != null && heldItem == null);
        tochka.SetActive(!canPick);
        textE.SetActive(canPick);
        HintTextObj.SetActive(canPick);
        if (canPick)
            HintText.text = lookedItem.ObjName;
        else
            HintText.text = "";
    }

    void PickUp(PickupItem item, string ObjName)
    {
        heldItem = item;
        iteminhand = ObjName;
        item.rb.isKinematic = true;
        if (!item.rb.isKinematic)
        {
            item.rb.velocity = Vector3.zero;
            item.rb.angularVelocity = Vector3.zero;
        }

        item.transform.SetParent(holdPoint);
        item.transform.localPosition = item.holdLocalPos;
        item.transform.localEulerAngles = item.holdLocalRot;
    }
    public void Drop(PickupItem item)
    {
        if (item != null)
        {
            item.transform.SetParent(null);
            iteminhand = null;

            item.rb.isKinematic = false;
            item.rb.AddForce(playerCamera.transform.forward * throwForce, ForceMode.Impulse);

            heldItem = null;
        }
    }

    public void removeiteminhand(PickupItem item)
    {
        if (item != null)
        {
            item.transform.SetParent(null);
            iteminhand = null;

            item.rb.isKinematic = false;
            heldItem = null;
        }
    }
}

