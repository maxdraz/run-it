using System;
using System.Collections.Generic;
using System.IO;
using RunIt.Saving;
using RunIt.UI.Leaderboard;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace RunIt.Testing
{
    public class SaveTest : MonoBehaviour
    {
        public int maxNumEntries = 5;
        public bool addNew;
        public List<LeaderboardEntry> entries;
        private string dir = "LeaderboardData/";

        private void Awake()
        {
            LoadEntries();   
        }

        private void Update()
        {
            if (addNew)
            {
                var e = RandomEntry();
                entries.Add(e);
                entries.Sort(SortByTime);
                addNew = false;
            }
        }

        private void OnDestroy()
        {
            SaveEntries();
        }

        private void SaveRandomEntries()
        {
            for (int i = 0; i < maxNumEntries; i++)
            {
                var name = Utilities.Utilities.RandomName(3);
                var time = UnityEngine.Random.Range(1f, 10f);
                var entry = new LeaderboardEntry() {time = time};

                entries.Add(entry);

                if (i == maxNumEntries - 1)
                {
                    var num = 0;
                    entries.Sort(SortByTime);
                    foreach (var e in entries)
                    {
                        SaveSystem.Save(e, "LeaderboardData/", "entry_" + num + ".json");
                        num++;
                    }
                }
            }
        }
        
        private void SaveEntries()
        {
            entries.Sort(SortByTime);
            var entryCount = Mathf.Min(entries.Count, maxNumEntries);
            
            print(entryCount);
            for (int i = 0; i < entryCount; i++)
            {
                SaveSystem.Save(entries[i], "LeaderboardData/", "entry_" + i + ".json");
            }
            
            print("saved");
        }

        private LeaderboardEntry RandomEntry()
        {
            var name = Utilities.Utilities.RandomName(3);
            var time = UnityEngine.Random.Range(1f, 10f);
            return new LeaderboardEntry() {time = time};
        }

        void LoadEntries()
        {
            var files = Directory.GetFiles(SaveSystem.SAVE_DIRECTORY + dir,"*.json");
            if(files.Length < 1) return;
            for (int i = 0; i < files.Length; i++)
            {
                var entry = SaveSystem.Load<LeaderboardEntry>("LeaderboardData/entry_" + i + ".json");
                entries.Add(entry);
            }
        }

        static int SortByTime(LeaderboardEntry a, LeaderboardEntry b)
        {
            return a.time.CompareTo(b.time);
        }
    }
}
    