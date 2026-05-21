using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Tilemaps;
using Unity.VisualScripting;

public class SortOrderManager : MonoBehaviour
{
    //[SerializeField] private Dictionary<SpriteRenderer, float> _spritesRegistry = new Dictionary<SpriteRenderer, float>();
    private List<(SpriteRenderer sprite, float yValue)> _spritesRegistry = new List<(SpriteRenderer sprite, float yValue)> ();
    //private List<(Tilemap tilempa, Vector3Int cell, float yValue)> _tilesRegistry = new List<(Tilemap tilempa, Vector3Int cell, float yValue)>();
    [SerializeField] private LayerMask _ignoredLayers;

    private void Update()
    {
        RemoveDestroyedObjects();
        AddSpritesToRegistry();
        GetYValues();
        SortByValues();
        ApplyOrderInLayer();
    }

    private void AddSpritesToRegistry()
    {
        var spriteRenderers = FindObjectsByType<SpriteRenderer>(FindObjectsSortMode.None);
        foreach (SpriteRenderer sprite in spriteRenderers)
        {
            if (!_spritesRegistry.Any(entry => entry.sprite == sprite) && ((1 << sprite.gameObject.layer) & _ignoredLayers) == 0)
                _spritesRegistry.Add((sprite, 0f));
        }
        foreach (var sprite in _spritesRegistry)
        {
            Debug.Log(sprite);
        }
    }

    //private void AddTilesToRegistry()
    //{
    //    _tilesRegistry.Clear ();

    //    var tilemaps = FindObjectsByType<Tilemap>(FindObjectsSortMode.None);
    //    foreach (Tilemap tilemap in tilemaps)
    //    {
    //        foreach (Vector3Int cell in tilemap.cellBounds.allPositionsWithin)
    //        {
    //            if (!tilemap.HasTile(cell)) continue;

    //            float yValue = tilemap.CellToWorld(cell).y;
    //            _tilesRegistry.Add((tilemap, cell, yValue));
    //        }
    //    }
    //}

    private void GetYValues()
    {
        //foreach (SpriteRenderer sprite in _spritesRegistry.Keys.ToList())
        //{
        //    _spritesRegistry[sprite] = sprite.transform.position.y;
        //}
        _spritesRegistry = _spritesRegistry.Select(entry => (entry.sprite, entry.sprite.transform.position.y)).ToList();
    }

    private void SortByValues()
    {
        //Dictionary<SpriteRenderer, float> sortedDictionnary = (Dictionary<SpriteRenderer, float>)(from yValue in _spritesRegistry orderby yValue.Value ascending select yValue);
        //_spritesRegistry = sortedDictionnary;
        _spritesRegistry = _spritesRegistry.OrderByDescending(entry => entry.yValue).ToList();
        foreach (var entry in _spritesRegistry)
        {
            //Debug.Log(entry);
        }
    }

    private void ApplyOrderInLayer()
    {
        int order = 2;
        foreach (var entry in _spritesRegistry)
        {
            entry.sprite.sortingOrder = order;
            order++;
        }
    }

    private void RemoveDestroyedObjects()
    {
        var _spritesClone = new List<(SpriteRenderer sprite, float yValue)>(_spritesRegistry);
        foreach (var entry in _spritesClone)
        {
            if (entry.sprite == null)
                _spritesRegistry.Remove(entry);
        }
    }


}
