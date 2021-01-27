using GameRules;
using GameRules.AI.SM;
using UnityEngine;
using Util;
using Random = UnityEngine.Random;

namespace Interactions
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(LooseChild))]
    [RequireComponent(typeof(AudioSource))]
    public class Item : MonoBehaviour, IInteractable
    {
        public Rigidbody Rigidbody { get; private set; }
        public MeshRenderer targetMesh;
        public int materialID;
        public ItemStats stats;

        public int Rarity
        {
            get => _rarity;
            set
            {
                _rarity = value;
                UpdateColorOnMesh();
            }
        }

        public string id;
        public float alertDistance = 2;
        public AudioClip[] hitSounds;
        public Vector3 holdPositionOffset;
        public Quaternion holdRotationOffset;

        public Vector3 OriginalPosition { get; private set; }
        public Quaternion OriginalRotation { get; private set; }

        private int _rarity;
        private AudioSource _source;
        private LooseChild _parentRel;
        private Collider _col;
        private static readonly int BaseColor = Shader.PropertyToID("_Color");

        //This needs to be done in Start because the ItemManager loads stats in Awake
        private void Start()
        {
            stats = GameStatus.Instance.Items.GetItemStats(id);

            Rigidbody = GetComponent<Rigidbody>();
            Rigidbody.isKinematic = true;
            _source = GetComponent<AudioSource>();
            _col = GetComponent<Collider>();
            if (!_col) _col = GetComponentInChildren<Collider>();

            _parentRel = GetComponent<LooseChild>();
            _parentRel.copyPosition = true;
            _parentRel.copyRotation = true;
            _parentRel.rotationOffset = holdRotationOffset.eulerAngles;
            _parentRel.positionOffset = holdPositionOffset;

            OriginalPosition = transform.position;
            OriginalRotation = transform.rotation;

            if (id.Length == 0)
                Debug.LogError($"Item '{gameObject.name}' does not have an id.");
        }

        private void OnCollisionEnter(Collision other)
        {
            //Ignore the player
            if (other.collider.gameObject.layer == 9) return;

            //Alert all nearby Helpers
            RaycastHit[] hits = new RaycastHit[8];
            Physics.SphereCastNonAlloc(transform.position, alertDistance, Vector3.up, hits, 1f, 1 << 11);
            foreach (RaycastHit h in hits)
            {
                if (!h.rigidbody) continue;
                AIInputProducer producer = h.rigidbody.gameObject.GetComponentInChildren<AIInputProducer>();
                if (producer) producer.AlertSound(transform);
            }

            //Make a random sound
            if (hitSounds.Length == 0 || _source == null || _source.isPlaying) return;

            int rand = Random.Range(0, hitSounds.Length);
            _source.clip = hitSounds[rand];
            _source.Play();
        }

        public void ClearParent()
        {
            _parentRel.enabled = false;
            _parentRel.parentObject = null;
            _col.isTrigger = false;
        }

        //The logic behind this is that when an object becomes a child of an object marked "DontDestroyOnLoad", it also gets
        //marked "DontDestroyOnLoad", which is bad for scene loading.
        public void SetParent(Transform parent)
        {
            _parentRel.parentObject = parent;
            _parentRel.enabled = true;
            _col.isTrigger = true;
        }

        public string GetTooltip()
        {
            return stats.friendlyName;
        }

        public void StartLookAt()
        {
            //maybe highlight effect?
        }

        public void EndLookAt()
        {
            //maybe highlight effect?
        }

        public void Interact()
        {
            PlayerStatus.Instance.Hand.InitiateGrabItem();
        }

        private void UpdateColorOnMesh()
        {
            if (materialID < 0) return;
            if (targetMesh == null)
                targetMesh = GetComponentInChildren<MeshRenderer>();
            targetMesh.materials[materialID].SetColor(BaseColor, GameStatus.Instance.Items.db.rarities[_rarity].color);
        }
    }
}