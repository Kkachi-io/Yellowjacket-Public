using UnityEngine;

public class Breakable : MonoBehaviour, IDamagable
{
	#region Variables

        [SerializeField] private int health;

        [SerializeField] private DamageDealer takesDamageFrom;

        [Header("Components")]
        [SerializeField] private HitBox hitBox;

        [SerializeField] private SpriteRenderer spriteRenderer;

        [SerializeField] private Collider col;

        [SerializeField] private AudioSource audioSource;

        [SerializeField] private AudioClip audioClip;

        [SerializeField] private float slideWindow = 0.5f;

        private bool slid = false;

        private float timer;

	#endregion

    private void OnEnable()
    {
        Slide.IsSliding += ToggleSlid;
    }

    private void OnDisable()
    {
        Slide.IsSliding -= ToggleSlid;
    }

    private void Start()
    {
        if(!hitBox)
            hitBox = GetComponent<HitBox>();

        hitBox.AttachCharacter(this, takesDamageFrom);

        if(!spriteRenderer)
            spriteRenderer = GetComponent<SpriteRenderer>();

        if(!col)
            col = GetComponent<Collider>();
    }

    private void Update()
    {
        if(!slid) return;

        timer += Time.deltaTime;

        if(timer >= slideWindow)
        {
            slid = false;
            timer = 0;
        }

    }

    private void ToggleSlid()
    {
        slid = true;
    }

	public int GetHealth()
	{
        return health;
	}

	public void TakeDamage(int damage, AudioClip weaponAudioClip)
	{
		TakeDamage(damage);
	}

	public void TakeDamage(int damage)
	{
        if(!slid) return;

		health -= damage;

        if(health <= 0)
            Break();
	}

    private void Break()
    {
        audioSource.PlayOneShot(audioClip);
        spriteRenderer.enabled = false;
        col.enabled = false;
    }

    [ContextMenu("Unbreak")]
    private void Unbreak()
    {
        spriteRenderer.enabled = true;
        col.enabled = true;
        health = 1;
    }

}
