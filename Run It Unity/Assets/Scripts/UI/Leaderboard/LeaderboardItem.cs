using TMPro;
using UnityEngine;

namespace RunIt.UI.Leaderboard
{
    public class LeaderboardItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI playerName;
        public TextMeshProUGUI PlayerName
        {
            get => playerName;
            set => playerName = value;
        }
        [SerializeField] private TextMeshProUGUI rank;

        public TextMeshProUGUI Rank
        {
            get => rank;
            set => rank = value;
        }
        [SerializeField] private TextMeshProUGUI time;

        public TextMeshProUGUI Time
        {
            get => time;
            set => time = value;
        }
        
    }
}