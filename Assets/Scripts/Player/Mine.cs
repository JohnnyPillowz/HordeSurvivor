using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Mine : MonoBehaviour
{
    [SerializeField] private float mineHealth = 10f;
    [SerializeField] private float miningInterval = 0.5f;
    [SerializeField] private int experienceGained = 10;
    [SerializeField] private Image progressBar;
    [SerializeField] private Canvas progressBarCanvas;

    private Coroutine interactionCoroutine = null;
 
    private Player player;
    private ExperienceManager experienceManager;
    private int currentHealth;
    
    private void Start()
    {
        progressBarCanvas.gameObject.SetActive(false);
        currentHealth = Mathf.RoundToInt(mineHealth);
        player = Player.Instance;
        experienceManager = ExperienceManager.Instance;
    }

    private void StartedInteraction()
    {
        progressBarCanvas.gameObject.SetActive(true);
        if (interactionCoroutine == null)
        {
            interactionCoroutine = StartCoroutine(MiningRoutine());
            player.controls.ChangeCanMove(false);
        }
    }

    private void StoppedInteraction()
    {
        if (interactionCoroutine != null)
        {
            StopCoroutine(interactionCoroutine);
            player.controls.ChangeCanMove(true);
            interactionCoroutine = null;
        }
    }

    private IEnumerator MiningRoutine()
    {
        while (true)
        {
            currentHealth -= Mathf.RoundToInt(player.stats.minePower);
            progressBar.fillAmount = (float)currentHealth / (float)mineHealth;
            if (currentHealth <= 0)
            {
                FinishMining();
                yield break;
            }
            yield return new WaitForSeconds(miningInterval);
        }
    }

    private void FinishMining()
    {
        //TODO throw experience balls
        experienceManager.IncreaseExperience(experienceGained);

        player.controls.ChangeCanMove(true);
        Destroy(this.gameObject);
    }
    
    //Subscribing to events
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerControls.OnInteractionStarted += StartedInteraction;
            PlayerControls.OnInteractionStopped += StoppedInteraction;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerControls.OnInteractionStarted -= StartedInteraction;
            PlayerControls.OnInteractionStopped -= StoppedInteraction;
        }
    }

    private void OnDestroy()
    {
        PlayerControls.OnInteractionStarted -= StartedInteraction;
        PlayerControls.OnInteractionStopped -= StoppedInteraction;
    }
}
