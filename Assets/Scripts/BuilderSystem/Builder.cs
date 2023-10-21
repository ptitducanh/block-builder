using System;
using System.Collections;
using Data;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BuilderSystem
{
    public class Builder : MonoBehaviour, IBuilder, IBootstrapLoader
    {
        /// <summary>This contains all the block meta data</summary>
        [SerializeField] private BlockData blockData;
        
        public Action<Block> OnPlayerPlacedBlock    { get; set; }
        public Action<Block> OnPlayerRetrievedBlock { get; set; }

        private Camera _camera;
        private Camera MainCamera
        {
            get
            {
                if (_camera == null)
                {
                    _camera = Camera.main;
                }

                return _camera;
            }
        }
        private Block  _currenBlock; 
        
        private IBlockPool _blockPool;
        private IWorld    _world;
        private IWorld    World
        {
            get
            {
                if (_world == null)
                {
                    _world = ServiceLocator.Get<IWorld>();
                }

                return _world;
            }
        }

        private ISoundSystem _soundSystem;

        private ISoundSystem SoundSystem
        {
            get
            {
                if (_soundSystem == null)
                {
                    _soundSystem = ServiceLocator.Get<ISoundSystem>();
                }
                
                return _soundSystem;
            }
        }
        
        public IEnumerator Load()
        {
            ServiceLocator.Register<IBuilder>(this);
            _blockPool = ServiceLocator.Get<IBlockPool>();
            yield return null;
        }

        private void Update()
        {
            ShowBlockPreview();
            PlaceTheBlock();
            RetrieveBlock();
        }

        #region public functions
        public void SelectBlock(long blockId)
        {
            if (_currenBlock != null)
            {
                _currenBlock.GetComponent<BoxCollider>().enabled = false;
                _blockPool.ReturnBlock(_currenBlock);
            }
            
            _currenBlock = _blockPool.GetBlock(blockId);
        }


        #endregion

        #region private functions
        /// <summary>If player select a block from the inventory, then we will show a preview of that block in the world.</summary>
        private void ShowBlockPreview()
        {
            if (_currenBlock != null)
            {
                var ray = MainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hit))
                {
                    var block = hit.collider.GetComponent<Block>();
                    if (block != null && block != _currenBlock)
                    {
                        _currenBlock.transform.position = block.transform.position + hit.normal * (block.Size.y + _currenBlock.Size.y ) / 2;
                    }
                }
            }
        }

        /// <summary>
        /// Place the current block at the position of the preview block.
        /// </summary>
        private void PlaceTheBlock()
        {
            if (Input.GetMouseButtonUp(0) && _currenBlock != null)
            {
                OnPlayerPlacedBlock?.Invoke(_currenBlock);
                World.AddBlock(_currenBlock);
                
                // pretty simple. For now the preview block is the actual block.
                // So we just need to stop controlling that block
                _currenBlock.GetComponent<BoxCollider>().enabled = true;
                _currenBlock = null;
                
                SoundSystem.PlaySFX(SFX.PlaceBlock);
            }
        }
        
        /// <summary>
        /// If player right click on a block. Then we will retrieve that block from the world.
        /// The block will be added to the inventory. And returned to the object pool.
        /// </summary>
        /// <param name="block"></param>
        private void RetrieveBlock()
        {
            if (_currenBlock == null && Input.GetMouseButtonDown(1))
            {
                var ray = MainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hit) && !hit.collider.CompareTag("PlaygroundObject"))
                {
                    var hitBlock = hit.collider.GetComponent<Block>();
                    if (hitBlock != null)
                    {
                        
                        _blockPool.ReturnBlock(hitBlock);
                    }
                    OnPlayerRetrievedBlock?.Invoke(hitBlock);
                    World.RemoveBlock(hitBlock);
                    
                    SoundSystem.PlaySFX(SFX.RetrieveBlock);
                }
            }
        }

        #endregion

    }
}