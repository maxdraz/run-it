using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using RunIt.Detection;
using RunIt.Global;
using RunIt.Saving;
using RunIt.Settings;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RunIt.UI.Leaderboard
{
    
    [System.Serializable]
    public class LeaderboardEntry
    {
        public string playerName;
        public float time;
    }
    
    public class LeaderboardManager : MonoBehaviour
    {
        [SerializeField] private Transform leaderboardItemParent;
        [SerializeField] private GameObject leaderboardItemPrefab;
        [SerializeField] private int maxNumEntries = 1;
        [SerializeField] private float yOffset = 20;
        [SerializeField] private Detector displayLeaderboardDetector;
        [SerializeField] private List<LeaderboardEntry> entries;
        private bool alreadyDisplaying;
        private string directory = "Leaderboard/";
        private bool levelComplete;

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
            //load entries 
            LoadEntries();

        }

        private void OnDestroy()
        {
            SaveEntries();
        }

        void OnEnableLeaderboard(Collider other)
        {
            //SetChildrenActive(true);
            if (!alreadyDisplaying)
            {
                SetChildrenActive(true);
                if (!levelComplete)
                {
                    GenerateLeaderboard();
                    levelComplete = true;
                }
            }
            alreadyDisplaying = true;
        }

        void OnDisableLeaderboard(Collider other)
        {
            SetChildrenActive(false);

           // for (int i = 0; i < leaderboardItemParent.childCount; i++)
         //   {
          //      var child = leaderboardItemParent.GetChild(i);
          //      Destroy(child.gameObject);
         //   }

            alreadyDisplaying = false;
        }

        private void GenerateLeaderboard()
        {
//            var name = GameSettings.Instance.PlayerSettings.playerName;
            var elapsed = Timer.Instance.Elapsed;
            var currentEntry = new LeaderboardEntry() {playerName = name, time = Timer.Instance.Elapsed};

            entries.Add(currentEntry);
            entries.Sort(SortByTime);
            levelComplete = true;
            

            for (int i = 0; i < maxNumEntries; i++)
            {
                var item = GameObject.Instantiate(leaderboardItemPrefab, leaderboardItemParent);
                var rectTransform = item.GetComponent<RectTransform>();
                var newPos = new Vector2(0, -1* (i + 1) * yOffset);
                rectTransform.position = leaderboardItemParent.transform.TransformPoint(newPos);

                var data = item.GetComponent<LeaderboardItem>();
                data.PlayerName.text = currentEntry.playerName;
                data.Time.text = entries[i].time.ToString();
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

        static int SortByTime(LeaderboardEntry a, LeaderboardEntry b)
        {
            return a.time.CompareTo(b.time);
        }
        
        private void LoadEntries()
        {
            if (!Directory.Exists(SaveSystem.SAVE_DIRECTORY + directory))
            {
                Directory.CreateDirectory(SaveSystem.SAVE_DIRECTORY + directory);
            }
            var files = Directory.GetFiles(SaveSystem.SAVE_DIRECTORY + directory,"*.json");
            if(files.Length <= 0) return;
            
            for (int i = 0; i < files.Length; i++)
            {
                var entry = SaveSystem.Load<LeaderboardEntry>("LeaderboardData/entry_" + i + ".json");
                entries.Add(entry);
            }
        }
        
        private void SaveEntries()
        {
            entries.Sort(SortByTime);
            var entryCount = Mathf.Min(entries.Count, maxNumEntries);
            
            print(entryCount);
            for (int i = 0; i < entryCount; i++)
            {
                SaveSystem.Save(entries[i], directory, "entry_" + i + ".json");
            }
            print("saved");
        }
    }
}