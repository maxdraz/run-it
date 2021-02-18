using System;
using System.Collections.Generic;
using RunIt.Detection;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RunIt.UI.Leaderboard
{
    public class LeaderboardManager : MonoBehaviour
    {
        [SerializeField] private Transform leaderboardItemParent;
        [SerializeField] private GameObject leaderboardItemPrefab;
        [SerializeField] private int numEntries = 1;
        [SerializeField] private float yOffset = 20;
        [SerializeField] private Detector displayLeaderboardDetector;

        private List<LeaderboardItem> entries;

        private void OnEnable()
        {
            displayLeaderboardDetector.Enter += OnEnableLeaderboard;
            displayLeaderboardDetector.Exit += OnDisableLeaderboard;
        }

        private void OnDisable()
        {
            displayLeaderboardDetector.Enter -= OnEnableLeaderboard;
            displayLeaderboardDetector.Exit -= OnDisableLeaderboard;
        }

        private void Start()
        {
            GenerateLeaderboard();
        }

        void OnEnableLeaderboard(Collider other)
        {
            SetChildrenActive(true);
            GenerateLeaderboard();
        }

        void OnDisableLeaderboard(Collider other)
        {
            SetChildrenActive(false);
        }
        
        private void GenerateLeaderboard()
        {
            for (int i = 0; i < numEntries; i++)
            {
                var item = GameObject.Instantiate(leaderboardItemPrefab, leaderboardItemParent);
                var rectTransform = item.GetComponent<RectTransform>();
                var newPos = new Vector2(0, -1* (i + 1) * yOffset);
                rectTransform.position = leaderboardItemParent.transform.TransformPoint(newPos);

                var data = item.GetComponent<LeaderboardItem>();
                data.PlayerName.text = Utilities.Utilities.RandomName(Random.Range(3,6));
                data.Time.text = Random.Range(200, 5000).ToString();
                data.Rank.text = (i + 1).ToString();
            }
        }

        private void SetChildrenActive(bool value)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(value);
            }
        }
    }
}