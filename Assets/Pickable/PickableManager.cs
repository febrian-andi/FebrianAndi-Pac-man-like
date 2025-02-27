using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableManager : MonoBehaviour
{
    [SerializeField]
    private Player player;

    private List<Pickable> pickableList = new List<Pickable>();

    // Start is called before the first frame update
    void Start()
    {
        InitPickableList();
    }

    private void InitPickableList()
    {
        Pickable[] pickableObjects = GameObject.FindObjectsOfType<Pickable>();
        foreach (Pickable pickable in pickableObjects)
        {
            pickableList.Add(pickable);
            pickable.OnPicked += OnPickablePicked;
        }
        // Debug.Log("Pickable list size: " + pickableList.Count);
    }

    private void OnPickablePicked(Pickable pickable)
    {
        pickableList.Remove(pickable);
        // Debug.Log("Pickable list size: " + pickableList.Count);

        if (pickable.pickableType == PickableType.PowerUp)
        {
            player?.PickPowerUp();
        }

        if (pickableList.Count <= 0)
        {
            Debug.Log("Win");
        }
    }
}
