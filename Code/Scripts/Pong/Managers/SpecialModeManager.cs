using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    public class SpecialModeManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> spawnItems;
        [SerializeField] private BoxCollider2D spawnZone;
        [SerializeField] private List<GameObject> spawnableItems;

        [SerializeField] private float spawnAt, spawnDelay, spawnDestroyAfter;
        [SerializeField] private int maxSpawnItems;

        public void Enable()
        {
            gameObject.SetActive(true);
            InvokeRepeating(nameof(SpawnSingleItem), spawnAt, spawnDelay);
        }

        private void SpawnSingleItem()
        {
            if (spawnItems.Count >= maxSpawnItems) return;
            GameObject randomItem = spawnableItems[Random.Range(0, spawnableItems.Count)];

            GameObject item = Instantiate(randomItem, GetRandomPosition(randomItem), Quaternion.identity);
            spawnItems.Add(item);
            item.GetComponent<Bonus>()._specialModeManager = this;
            item.GetComponent<Bonus>().Invoke(nameof(Bonus.TryToDestroy), spawnDestroyAfter);
        }

        public void RemoveItem(GameObject gameObject)
        {
            GameObject item = spawnItems.Find(item => item.GetInstanceID() == gameObject.GetInstanceID());
            spawnItems.Remove(item);
            Destroy(item);
        }

        private Vector2 GetRandomPosition(GameObject item)
        {
            Bounds colliderBounds = spawnZone.bounds;
            Vector3 colliderCenter = colliderBounds.center;
            Vector3 spawnableItemLocalScale = item.transform.localScale;

            float spawnableItemSizeX = spawnableItemLocalScale.x / 2;
            float spawnableItemSizeY = spawnableItemLocalScale.y / 2;

            float[] ranges = {
            (colliderCenter.x - colliderBounds.extents.x) + spawnableItemSizeX,
            (colliderCenter.x + colliderBounds.extents.x) - spawnableItemSizeX,
            (colliderCenter.y - colliderBounds.extents.y) + spawnableItemSizeY,
            (colliderCenter.y + colliderBounds.extents.y) - spawnableItemSizeY,
        };

            float randomX = Random.Range(ranges[0], ranges[1]);
            float randomY = Random.Range(ranges[2], ranges[3]);

            Vector2 randomPos = new Vector2(randomX, randomY);

            return randomPos;
        }
    }
}