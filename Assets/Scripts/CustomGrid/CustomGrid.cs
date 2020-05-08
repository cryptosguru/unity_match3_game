using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Loafwad.Ulto
{
    public class CustomGrid : MonoBehaviour
    {
        [Header("Grid Size")]
        /*Top right corner of grid*/
        [SerializeField] private Vector2Int _vector2Int = Vector2Int.zero;

        [Header("Custom Level")]
        [SerializeField] private bool _useCustom = false;
        [SerializeField] private TextAsset _customLevel = null;
        //the symbol for end of row
        [SerializeField] private char _rowSymbol = ';';
        //symbol between each tile
        [SerializeField] private char _tileSplitSymbol = ',';
        [SerializeField] private string _tileNormalSymbol = "0";
        [SerializeField] private string _tileBlockedSymbol = "x";

        [Header("Tile Prefabs")]
        [SerializeField] private GameObject _normalTilePrefab = default;
        [SerializeField] private GameObject _blockedTilePrefab = default;

        [Header("Animal Prefabs")]
        [SerializeField] private List<GameObject> _animalPrefabs = default;

        private Dictionary<Vector2Int, CustomTile> _customGrid = null;

        private void Start()
        {
            if (_useCustom)
            {
                CreateGrid(_customLevel);
            }
            else
            {
                CreateGrid();
            }

            _FillGridWithAnimals();
        }

        public void CreateGrid()
        {
            _customGrid = new Dictionary<Vector2Int, CustomTile>();

            for (int x = 0; x < _vector2Int.x; x++)
            {
                for (int y = 0; y < _vector2Int.y; y++)
                {
                    CreateTile(x, y, _normalTilePrefab);
                }
            }
        }

        public void CreateGrid(TextAsset customLevel)
        {
            _customGrid = new Dictionary<Vector2Int, CustomTile>();
            //get rid of carriage + newline
            string text = customLevel.text
                .Replace("\r", "")
                .Replace("\n","")
                .Replace("\r\n","");

            string[] levelRows = text.Split(_rowSymbol);
            Debug.Log($"NumRows: {levelRows.Length}");
            for (int y =  0; y < levelRows.Length; y++)
            {
                int actualY = levelRows.Length - y - 1;
                Debug.Log($"Row: {actualY} :: {levelRows[actualY]}");
                string[] tileSymbols = levelRows[actualY].Split(_tileSplitSymbol);

                for (int x = 0; x < tileSymbols.Length; x++)
                {
                    Debug.Log($"Symbol: {x} :: {tileSymbols[x]} :: {_tileNormalSymbol == tileSymbols[x]} // {_tileBlockedSymbol == tileSymbols[x]}");

                    if (_tileNormalSymbol == tileSymbols[x])
                    {
                        CreateTile(x, y, _normalTilePrefab);
                    }
                    else if (_tileBlockedSymbol == tileSymbols[x])
                    {
                        CreateTile(x, y, _blockedTilePrefab);
                    }
                }
            }
        }

        public void CreateTile(int x, int y, GameObject prefab)
        {
            Vector2 vector = new Vector2(x, y);
            GameObject tileGO = Instantiate(prefab, vector, Quaternion.identity, transform);
            var tileCO = tileGO.GetComponent<CustomTile>();

            _customGrid.Add(Vector2Int.RoundToInt(vector), tileCO);
        }

        private void _FillGridWithAnimals()
        {
            foreach (CustomTile item in _customGrid.Values)
            {
                _CreateAnimal(item);
            }
        }
        private void _CreateAnimal(CustomTile customTile)
        {
            if (!customTile.IsBlocked)
            {
                GameObject animalPrefabGO = _animalPrefabs[Random.Range(0, _animalPrefabs.Count - 1)];
                GameObject animalGO = 
                    Instantiate(animalPrefabGO, customTile.transform.position, Quaternion.identity, customTile.transform);
                customTile.Animal = animalGO.GetComponent<Animal>();
            }
        }
    }
}