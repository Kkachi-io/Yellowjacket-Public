using UnityEngine;

public interface IDamagable
{
	int GetHealth();

	void TakeDamage(int damage, AudioClip audioClip);

    void TakeDamage(int damage);
}
