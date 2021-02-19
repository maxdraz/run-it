using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using RunIt.Detection;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RunIt.UI.Leaderboard
{
    [System.Serializable]
    public class LeaderboardSaveData
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
        private List<LeaderboardSaveData> leaderboard;

        private List<LeaderboardItem> entries;
        private string savePath;

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
            
            savePath = Application.dataPath + "/SaveData/";
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
            
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

        static int SortByTime(LeaderboardSaveData a, LeaderboardSaveData b)
        {
            return a.time.CompareTo(b.time);
        }

        private List<LeaderboardSaveData> TestSorting()
        {
            leaderboard = new List<LeaderboardSaveData>();
            //add random items
            for (int i = 0; i < 10; i++)
            {
                var item = new LeaderboardSaveData() {playerName = Utilities.Utilities.RandomName(5), time = Random.Range(10,5000)};
                leaderboard.Add(item);
            }
            leaderboard.Sort(SortByTime);

            return leaderboard;
        }

        private void TestJson()
        {
            var items = TestSorting();
            string json = JsonUtility.ToJson(items);
            
            File.WriteAllText(savePath +  "LeaderboardData.json", json);
            //json = JsonUtility.ToJson


            var t = 5;
            var leaderboard = new LeaderboardSaveData(){playerName = "max", time = t};
        }

        //private LeaderboardItemData loadLeaderboardItemData()
        //{
            //JsonUtility.FromJson<LeaderboardItemData>()
           // string json = File.ReadAllText(Application.dataPath + "/SaveData/LeaderboardData.json");
            //File.
        //}
    }
}