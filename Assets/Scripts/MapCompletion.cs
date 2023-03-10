using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class MapCompletion : MonoSingleton<MapCompletion>
    {
        public const string filename = "completion.dat";

        [Serializable]
        private class EpisodeScore
        {
            public Episode Episode;
            public int Score;
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

        private void SaveResult(Episode currentEpisode, int levelScore)
        {
            foreach (var item in m_ComplitionData)
            {
                if (item.Episode == currentEpisode)
                {
                    if (levelScore > item.Score)
                    {
                        item.Score = levelScore;
                        Saver<EpisodeScore[]>.Save(filename, m_ComplitionData);
                    }
                }
            }
        }

        [SerializeField] private EpisodeScore[] m_ComplitionData;

        private int m_TotalScore;
        public int TotalScore => m_TotalScore;

        private void Start() // тут у ментора Awake
        {
            Saver<EpisodeScore[]>.TryLoad(filename, ref m_ComplitionData);
            foreach (var episodeScore in m_ComplitionData)
            {
                m_TotalScore += episodeScore.Score;
            }
        }

        public bool TryIndex(int id, out Episode episode, out int score)
        {
            if (id >= 0 && id < m_ComplitionData.Length)
            {
                episode = m_ComplitionData[id].Episode;
                score = m_ComplitionData[id].Score;
                return true;
            }

            episode = null;
            score = 0;
            return false;
        }
    }
}

