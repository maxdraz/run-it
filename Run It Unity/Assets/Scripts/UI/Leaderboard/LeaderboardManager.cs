using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using RunIt.Detection;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RunIt.UI.Leaderboard
{
    public class LeaderboardItemData
    {
        public float time;
        public string playerName;
    }
    
    public class LeaderboardManager : MonoBehaviour
    {
        [SerializeField] private Transform leaderboardItemParent;
        [SerializeField] private GameObject leaderboardItemPrefab;
        [SerializeField] private int numEntries = 1;
        [SerializeField] private float yOffset = 20;
        [SerializeField] private Detector displayLeaderboardDetector;
        private List<LeaderboardItemData> leaderboard;

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
            SetChildrenActive(false);
            TestJson();
        }

        void OnEnableLeaderboard(Collider other)
        {
            SetChildrenActive(true);
            GenerateLeaderboard();
        }

        void OnDisableLeaderboard(Collider other)
        {
            SetChildrenActive(false);

            for (int i = 0; i < leaderboardItemParent.childCount; i++)
            {
                var child = leaderboardItemParent.GetChild(i);
                Destroy(child.gameObject);
            }
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

        static int SortByTime(LeaderboardItemData a, LeaderboardItemData b)
        {
            return a.time.CompareTo(b.time);
        }

        private List<LeaderboardItemData> TestSorting()
        {
            leaderboard = new List<LeaderboardItemData>();
            //add random items
            for (int i = 0; i < 10; i++)
            {
                var item = new LeaderboardItemData() {playerName = Utilities.Utilities.RandomName(5), time = Random.Range(10,5000)};
                leaderboard.Add(item);
            }
            leaderboard.Sort(SortByTime);

            return leaderboard;
        }

        private void TestJson()
        {
            var items = TestSorting();
            string json = "";
            foreach (var item in items)
            {
                json += JsonUtility.ToJson(item);
            }
            
            File.WriteAllText(Application.dataPath+"/savefile.json", json);
            
            //json = JsonUtility.ToJson
           
        }

        //private LeaderboardItemData loadLeaderboardItemData()
        //{
            //JsonUtility.FromJson<LeaderboardItemData>()
           // string json = File.ReadAllText(Application.dataPath + "/SaveData/LeaderboardData.json");
            //File.
        //}
    }
}