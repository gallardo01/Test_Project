using MarchingBytes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;


    public static class Cache
    {

        private static Dictionary<Collider, Character> characters = new Dictionary<Collider, Character>();

        public static Character GetCharacter(Collider collider)
        {
            if (!characters.ContainsKey(collider))
            {
                characters.Add(collider, collider.GetComponent<Character>());
            }

            return characters[collider];
        }
    }
    public class Character : ColorObject
    {
        [SerializeField] protected LayerMask GroundLayer;
        [SerializeField] protected LayerMask StairLayer;
        [SerializeField] protected Animator animator;
        [SerializeField] private Transform BoxBrick;
        [SerializeField] private PlayerBrick playerBrickPref;
        [SerializeField] protected GameObject PlayerSkin;


        [HideInInspector] public Stage stage;
        public float lenghtRaycast;
        private bool isCanMove;
        private string currentAnim;

        List<PlayerBrick> ListBrick = new List<PlayerBrick>();
        public int BrickCounts => ListBrick.Count;
        private void Start()
        {

            changAnim(Constants.ANIM_IDLE);
            clearBrick();

        }

        public Vector3 checkGround(Vector3 nextPoint)
        {
            RaycastHit hit;
            if (Physics.Raycast(nextPoint, Vector3.down, out hit, lenghtRaycast, GroundLayer))
            {
                //Debug.Log(hit.point + Vector3.up);
                return hit.point + Vector3.up;
            }

            return transform.position;
        }

        public bool CanMove(Vector3 nextPoint)
        {
            isCanMove = true;
            RaycastHit hit;
            if (Physics.Raycast(nextPoint, Vector3.down, out hit, lenghtRaycast, StairLayer))
            {
                Stair stair = hit.collider.gameObject.GetComponent<Stair>();
                if (stair.colorType != colorType && ListBrick.Count > 0)
                {
                    isCanMove = true;
                    stair.changColor(colorType);
                    removeBrick();
                }
                if (stair.colorType != colorType && ListBrick.Count == 0 && PlayerSkin.transform.forward.z > 0f)
                {
                    isCanMove = false;
                }
            }
            return isCanMove;
        }
        public void addBrick()
        {
            int index = ListBrick.Count;
            PlayerBrick playerBrick = EasyObjectPool.instance.GetObjectFromPool("BrickPlayer", Vector3.zero, Quaternion.identity).GetComponent<PlayerBrick>();
            playerBrick.changColor(colorType);
            playerBrick.transform.SetParent(BoxBrick);
            playerBrick.transform.localRotation = Quaternion.Euler(Vector3.zero);
            playerBrick.transform.localPosition = Vector3.back * 0.5f + index * 0.25f * Vector3.up + Vector3.up * 1.5f;
            ListBrick.Add(playerBrick);
        }

        public void removeBrick()
        {
            int index = ListBrick.Count - 1;
            if (index >= 0)
            {
                PlayerBrick playerBrick = ListBrick[index];
                ListBrick.RemoveAt(index);
                EasyObjectPool.instance.ReturnObjectToPool(playerBrick.gameObject);
                playerBrick.transform.SetParent(null);
            }
        }

        public void clearBrick()
        {
            for (int i = 0; i < ListBrick.Count; i++)
            {
                Destroy(ListBrick[i].gameObject);
            }
            ListBrick.Clear();
        }


        public void changAnim(string animName)
        {
            if (currentAnim != animName)
            {
                animator.ResetTrigger(currentAnim);
                currentAnim = animName;
                animator.SetTrigger(animName);
            
            }
        }
        private void OnTriggerEnter(Collider other)
        {

            if (other.CompareTag(Constants.TAG_BRICK))
            {
                Brick brick = other.GetComponent<Brick>();
                if (brick.colorType == colorType)
                {
                    stage.RemoveBrick(brick);
                    addBrick();

                }
            }
            if (other.CompareTag(Constants.TAG_PLAYER) || other.CompareTag(Constants.TAG_BOT))
            {
            Character character = Cache.GetCharacter(other);
               if(BrickCounts > character.BrickCounts)
            {
                character.clearBrick();

       
            }
            else
            {
                clearBrick();

            }

        }

        }

    }

