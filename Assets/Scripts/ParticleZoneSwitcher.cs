using UnityEngine;

public class ParticleZoneSwitcher : MonoBehaviour
{
    [Header("Partículas del Jugador (Se detienen dentro)")]
    public ParticleSystem playerParticle1;
    public ParticleSystem playerParticle2;

    [Header("Partículas de la Casa / Exterior (Se inician dentro)")]
    public ParticleSystem houseParticle1;
    public ParticleSystem houseParticle2;

    [Header("Ajustes de Transición")]
    [Tooltip("Tiempo en segundos para pre-calentar la niebla al cambiar de zona")]
    public float prewarmTime = 2.0f;

    [Header("Filtro")]
    public bool onlyPlayer = true;

    private void OnTriggerEnter(Collider other)
    {
        if (onlyPlayer && !other.CompareTag("Player")) return;

        // Entró a la casa: apagamos las del jugador y FORZAMOS las de la casa
        StopSmooth(playerParticle1);
        StopSmooth(playerParticle2);

        PlayInstant(houseParticle1);
        PlayInstant(houseParticle2);
    }

    private void OnTriggerExit(Collider other)
    {
        if (onlyPlayer && !other.CompareTag("Player")) return;

        // Salió de la casa: FORZAMOS las del jugador y apagamos las de la casa
        PlayInstant(playerParticle1);
        PlayInstant(playerParticle2);

        StopSmooth(houseParticle1);
        StopSmooth(houseParticle2);
    }

    private void PlayInstant(ParticleSystem ps)
    {
        if (ps == null) return;

        // 1. Detenemos cualquier proceso anterior inmediatamente
        ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        
        // 2. Pre-calentamos para que el volumen no arranque vacío
        ps.Simulate(prewarmTime, true, true, false);

        // 3. Encendemos la emisión continua
        ps.Play();
    }

    private void StopSmooth(ParticleSystem ps)
    {
        if (ps == null) return;

        // Simplemente dejamos de emitir. Las partículas vivas se desvanecen
        // pero no bloquean al sistema si el jugador regresa de golpe.
        ps.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }
}