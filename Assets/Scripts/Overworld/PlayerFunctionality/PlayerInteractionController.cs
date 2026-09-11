using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class PlayerInteractionController : MonoBehaviour 
{
    [SerializeField] private PlayerInputController inputController;
    [SerializeField] private PlayerController plrController;
    [SerializeField] private LayerMask interactables;

    void OnEnable()
    {
        inputController.InteractionRequest += Interact;
    }
    void OnDisable()
    {
        inputController.InteractionRequest -= Interact;
    }

    void Interact()
    {
        var hit = CheckInteractable();
        if(hit != null)
        {
            Debug.Log($"hit interactable! {hit.name}");
            CheckTile(hit);
        }
    }

    private Collider2D CheckInteractable()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, plrController.facingDirection, 0.7f, interactables);
        Debug.DrawRay(transform.position, plrController.facingDirection * 0.7f, Color.red, 2f);
        if(hit.collider != null){
            return hit.collider;
        }
        return null;
    }

    private void CheckTile(Collider2D hitCollider)
    {
        Tilemap tilemap = hitCollider.GetComponent<Tilemap>();
        if(tilemap == null) return;

        Vector3 point = transform.position + (Vector3) (plrController.facingDirection *0.64f);
        TileBase tile = tilemap.GetTile(tilemap.WorldToCell(point));
        if(tile != null)
        {
            Debug.Log($"Tile: {tile.name}");
            // PLACE HOLDER
            var thing = hitCollider.GetComponent<Interactable>();
            if (thing!= null) {
                
                StartCoroutine(thing.RunInteraction(tile));
            }
            else Debug.Log("could not find thing!");
        }
        else Debug.Log("no tile found!");
    }
}
