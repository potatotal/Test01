using System.Collections;
    using System.Collections.Generic;
    using AYellowpaper.SerializedCollections;
    using Unity.VisualScripting;
    using UnityEngine;
    using UnityEngine.Rendering;

    public enum TetrominoType : byte
    {
        None,
        I,
        O,
        Z,
        S,
        J,
        L,
        T,
        Max
    }

    public class TetrisManager : Singleton<TetrisManager>
    {
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private AYellowpaper.SerializedCollections.SerializedDictionary <TetrominoType, string> tetrominoDatas;
        [SerializeField] private float dropTime = 1.0f;
        [SerializeField] private float leftLimitPosition = -5.0f;
        [SerializeField] private float rightLimitPosition = 5.0f;
        [SerializeField] private float bottomLimitPosition = -8.5f;
        
        private TetrominoData _currentTetrominoData;
        private float currentDropTime = 0.0f;
        
        private int[][] grid = null;
        
        public override void OnAwake()
        {
            base.OnAwake();
            
            grid = new int[25][];

            for (int i = 0; i < grid.Length; i++)
            {
                grid[i] = new int[10];
            }
        }

        void Start()
        {
            currentDropTime = dropTime;
            SpawnTetromino();
        }
        
        void Update()
        {
            if (_currentTetrominoData.IsUnityNull())
                return;
            
            if (Input.GetKeyDown(KeyCode.A))
            {
                _currentTetrominoData.transform.position += Vector3.left;
                if (checkBlockCollision())
                {
                    _currentTetrominoData.transform.position -= Vector3.left;
                }
            }
            
            if (Input.GetKeyDown(KeyCode.D))
            {
                _currentTetrominoData.transform.position += Vector3.right;
                if (checkBlockCollision())
                {
                    _currentTetrominoData.transform.position -= Vector3.right;
                }
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                transform.Rotate(new Vector3(0,0,-90));
                if (checkBlockCollision())
                {
                    _currentTetrominoData.transform.Rotate(new Vector3(0,0,90));
                }
            }
            
            currentDropTime -= Time.deltaTime;
            if (currentDropTime <= 0.0f)
            {
                _currentTetrominoData.transform.position += Vector3.down;
                if (checkBlockFinishMove())
                {
                    _currentTetrominoData.transform.position -= Vector3.down;
                    SpawnTetromino();
                }
                currentDropTime = dropTime;
            }
        }

        private void SpawnTetromino()
        {
            GameObject Tetromino_Prefab = null;
            TetrominoType nextBlockIndex = (TetrominoType)Random.Range(0, 7) + 1;
            Tetromino_Prefab = Resources.Load<GameObject>($"Prefab/{tetrominoDatas[nextBlockIndex]}");
            Debug.Assert(Tetromino_Prefab);
                    
            GameObject spawndTetromino = Instantiate(Tetromino_Prefab, spawnPoint.position, Quaternion.identity);
            spawndTetromino.TryGetComponent(out _currentTetrominoData);
        }


        bool checkBlockCollision()
        {
            foreach (var block in _currentTetrominoData.Blocks)
            {
                if (block.position.x < leftLimitPosition)
                {
                    return true;
                }
                
                if (block.position.x > rightLimitPosition)
                {
                    return true;
                }
            }

            return false;
        }

        bool checkBlockFinishMove()
        {
            foreach (var block in _currentTetrominoData.Blocks)
            {
                if (block.position.y < bottomLimitPosition)
                {
                    return true;
                }
            }

            return false;
        }
    }