using System;
using UnityEngine;

namespace TowerDefense
{
    public class MapCompletion : MonoSingleton<MapCompletion>
    {
        public const string Filename = "completion.dat";

        [Serializable]
        private class EpisodeScore
        {
            public Episode Episode;
            public int Score;
        }

        [SerializeField] private EpisodeScore[] m_ComplitionData;

        private int m_TotalScore = 30;
        public int TotalScore => m_TotalScore;

        private new void Awake()
        {
            base.Awake();

            Saver<EpisodeScore[]>.TryLoad(Filename, ref m_ComplitionData);

            foreach (var episodeScore in m_ComplitionData)
            {
                m_TotalScore += episodeScore.Score;
            }
        }

        public static void SaveEpisodeResult(int levelScore)
        {
            if (Instance)
            {
                Instance.SaveResult(LevelSequenceController.Instance.CurrentEpisode, levelScore);
            }
            else
            {
                Debug.Log($"Episode complete witg score{levelScore}");
            }
        }

        public int GetEpisodeScore(Episode episode)
        {
            foreach (var data in m_ComplitionData)
            {
                if (data.Episode == episode)
                {
                    return data.Score;
                }
            }
            return 0;
        }

        private void SaveResult(Episode currentEpisode, int levelScore)
        {
            foreach (var item in m_ComplitionData)
            {
                if (item.Episode == currentEpisode)
                {
                    if (levelScore > item.Score)
                    {
                        m_TotalScore += levelScore - item.Score;
                        item.Score = levelScore;
                        Saver<EpisodeScore[]>.Save(Filename, m_ComplitionData);
                    }
                }
            }
        }
    }
}

