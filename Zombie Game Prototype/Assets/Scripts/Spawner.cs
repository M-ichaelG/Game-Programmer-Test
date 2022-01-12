using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    public List<Transform> normalSpawnPoints;
    public List<GameObject> normalEnemies;
    public Transform bossSpawnPoint;
    public GameObject specialEnemy;
    public GameObject bossEnemy;

    private Coroutine crSpawning;
    private int countNormalEnemy;

    private void Start()
    {
        StartCoroutine(_Spawning());
    }

    IEnumerator _Spawning ()
    {
        //first enemy
        yield return new WaitForSeconds(5f);
        int randEnemyType = Random.Range(0, normalEnemies.Count);
        int randEnemyPosIndex = Random.Range(0, normalSpawnPoints.Count);
        GameObject enemy = Instantiate(normalEnemies[randEnemyType]);
        enemy.transform.position = normalSpawnPoints[randEnemyPosIndex].position;
        yield return new WaitForEndOfFrame();
        enemy.SetActive(true);
        yield return new WaitForEndOfFrame();
        enemy.GetComponent<NavMeshAgent>().enabled = true;
        GameManager.instance.countEnemy += 1;
        //next enemies
        while (GameManager.instance.countEnemy < 10) {
            yield return new WaitForSeconds(3f);
            randEnemyType = Random.Range(0, normalEnemies.Count);
            randEnemyPosIndex = Random.Range(0, normalSpawnPoints.Count);
            while (Vector3.Distance (normalSpawnPoints[randEnemyPosIndex].position, GameManager.instance.trPlayer.position) < 5f)
            {
                randEnemyPosIndex = Random.Range(0, normalSpawnPoints.Count);
            }
            GameObject nextEnemy = Instantiate(normalEnemies[randEnemyType]);
            nextEnemy.transform.position = normalSpawnPoints[randEnemyPosIndex].position;
            yield return new WaitForEndOfFrame();
            nextEnemy.SetActive(true);
            yield return new WaitForEndOfFrame();
            nextEnemy.GetComponent<NavMeshAgent>().enabled = true;
            GameManager.instance.countEnemy += 1;
        }

        while (GameManager.instance.countEnemy > 0)
        {
            yield return new WaitForSeconds(1f);
        }

        //first wave
        int firstRandSpecialPosIndex = Random.Range(0, normalSpawnPoints.Count);
        int secondRandSpecialPosIndex = Random.Range(0, normalSpawnPoints.Count);
        while (secondRandSpecialPosIndex == firstRandSpecialPosIndex)
        {
            secondRandSpecialPosIndex = Random.Range(0, normalSpawnPoints.Count);
        }
        int thirdRandSpecialPosIndex = Random.Range(0, normalSpawnPoints.Count);
        while (thirdRandSpecialPosIndex == secondRandSpecialPosIndex || thirdRandSpecialPosIndex == secondRandSpecialPosIndex)
        {
            thirdRandSpecialPosIndex = Random.Range(0, normalSpawnPoints.Count);
        }

        Coroutine crFirstWaveFirstSpawnPoint = StartCoroutine(_SpawningWaveNormalEnemies(firstRandSpecialPosIndex, 5));
        yield return new WaitForEndOfFrame();
        Coroutine crFirstWaveSecondSpawnPoint = StartCoroutine(_SpawningWaveNormalEnemies(secondRandSpecialPosIndex, 5));

        GameObject firstWaveSpecialEnemy = Instantiate(specialEnemy);
        firstWaveSpecialEnemy.transform.position = normalSpawnPoints[thirdRandSpecialPosIndex].position;
        yield return new WaitForEndOfFrame();
        firstWaveSpecialEnemy.SetActive(true);
        yield return new WaitForEndOfFrame();
        firstWaveSpecialEnemy.GetComponent<NavMeshAgent>().enabled = true;
        GameManager.instance.countEnemy += 1;

        while (GameManager.instance.countEnemy > 0)
        {
            yield return new WaitForSeconds (1f);
        }

        GameManager.instance.AddNormalEnemyHitpoints(50);
        
        //spawning after first wave
        while (GameManager.instance.specialCountEnemy < 15)
        {
            while (GameManager.instance.countEnemy < 10)
            {
                yield return new WaitForSeconds(2f);
                randEnemyType = Random.Range(0, normalEnemies.Count);
                randEnemyPosIndex = Random.Range(0, normalSpawnPoints.Count);
                GameObject moreEnemy = Instantiate(normalEnemies[randEnemyType]);
                moreEnemy.transform.position = normalSpawnPoints[randEnemyPosIndex].position;
                yield return new WaitForEndOfFrame();
                moreEnemy.SetActive(true);
                yield return new WaitForEndOfFrame();
                moreEnemy.GetComponent<NavMeshAgent>().enabled = true;
                GameManager.instance.countEnemy += 1;
                GameManager.instance.specialCountEnemy += 1;
            }
        }

        while (GameManager.instance.countEnemy > 0)
        {
            yield return new WaitForSeconds(1f);
        }

        //second wave
        firstRandSpecialPosIndex = Random.Range(0, normalSpawnPoints.Count);
        secondRandSpecialPosIndex = Random.Range(0, normalSpawnPoints.Count);
        while (secondRandSpecialPosIndex == firstRandSpecialPosIndex)
        {
            secondRandSpecialPosIndex = Random.Range(0, normalSpawnPoints.Count);
        }
        thirdRandSpecialPosIndex = Random.Range(0, normalSpawnPoints.Count);
        while (thirdRandSpecialPosIndex == secondRandSpecialPosIndex || thirdRandSpecialPosIndex == secondRandSpecialPosIndex)
        {
            thirdRandSpecialPosIndex = Random.Range(0, normalSpawnPoints.Count);
        }

        crFirstWaveFirstSpawnPoint = StartCoroutine(_SpawningWaveNormalEnemies(firstRandSpecialPosIndex, 10));
        yield return new WaitForEndOfFrame();
        crFirstWaveSecondSpawnPoint = StartCoroutine(_SpawningWaveNormalEnemies(secondRandSpecialPosIndex, 5));

        GameObject secondWaveSpecialEnemy = Instantiate(specialEnemy);
        secondWaveSpecialEnemy.transform.position = normalSpawnPoints[secondRandSpecialPosIndex].position;
        yield return new WaitForEndOfFrame();
        secondWaveSpecialEnemy.SetActive(true);
        yield return new WaitForEndOfFrame();
        secondWaveSpecialEnemy.GetComponent<NavMeshAgent>().enabled = true;
        GameManager.instance.countEnemy += 1;

        for (int i = 0; i < 2; i++)
        {
            GameObject secondWaveLastSpawnPosEnemy = Instantiate(specialEnemy);
            secondWaveLastSpawnPosEnemy.transform.position = normalSpawnPoints[thirdRandSpecialPosIndex].position;
            yield return new WaitForEndOfFrame();
            secondWaveLastSpawnPosEnemy.SetActive(true);
            yield return new WaitForEndOfFrame();
            secondWaveLastSpawnPosEnemy.GetComponent<NavMeshAgent>().enabled = true;
            GameManager.instance.countEnemy += 1;
        }

        while (GameManager.instance.countEnemy > 0)
        {
            yield return new WaitForSeconds(1f);
        }

        GameManager.instance.AddNormalEnemyHitpoints(100);

        //spawning after second wave
        while (GameManager.instance.specialCountEnemy < 20)
        {
            while (GameManager.instance.countEnemy < 15)
            {
                yield return new WaitForSeconds(2f);
                randEnemyType = Random.Range(0, normalEnemies.Count);
                randEnemyPosIndex = Random.Range(0, normalSpawnPoints.Count);
                GameObject moreEnemy = Instantiate(normalEnemies[randEnemyType]);
                moreEnemy.transform.position = normalSpawnPoints[randEnemyPosIndex].position;
                yield return new WaitForEndOfFrame();
                moreEnemy.SetActive(true);
                yield return new WaitForEndOfFrame();
                moreEnemy.GetComponent<NavMeshAgent>().enabled = true;
                GameManager.instance.countEnemy += 1;
                GameManager.instance.specialCountEnemy += 1;
            }
        }

        GameObject theBoss = Instantiate(bossEnemy);
        theBoss.transform.position = bossSpawnPoint.position;
        yield return new WaitForEndOfFrame();
        theBoss.SetActive(true);

    }

    IEnumerator _SpawningWaveNormalEnemies (int waveSpawnPositionIndex, int countSpawnEnemy)
    {
        for (int i = 0; i < countSpawnEnemy; i++)
        {
            int waveRandEnemyType = Random.Range(0, normalEnemies.Count);
            GameObject waveEnemy = Instantiate(normalEnemies[waveRandEnemyType]);
            waveEnemy.transform.position = normalSpawnPoints[waveSpawnPositionIndex].position;
            yield return new WaitForEndOfFrame();
            waveEnemy.SetActive(true);
            yield return new WaitForEndOfFrame();
            waveEnemy.GetComponent<NavMeshAgent>().enabled = true;
            GameManager.instance.countEnemy += 1;
        }
    }
}
