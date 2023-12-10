using System.Collections.Generic;
using Sources.Services.PersistentProgress.Services;
using UnityEngine;
using UnityEngine.UI.Extensions;

namespace Sources.UI.Windows.LevelSelection
{
    public class LevelsList : MonoBehaviour
    {
        [SerializeField] private float _verticalOffset = 300;
        [SerializeField] private LevelButton _levelButtonPrefab;
        [SerializeField] private Transform _parent;
        [SerializeField] private UILineRenderer _pathLine;
        [SerializeField] private Vector2[] _pathMap;
        [SerializeField] private int _levelsCount = 20;

        private readonly List<Vector2> _points = new List<Vector2>();
        private readonly List<LevelButton> _levelButtons = new List<LevelButton>();

        private IPersistentProgressService _progressService;

        public void Construct(IPersistentProgressService progressService) => 
            _progressService = progressService;

        public void Init()
        {
            BuildPoints();
            BuildLevels();

            _pathLine.transform.position = (Vector2)_pathLine.transform.position + Vector2.up * _verticalOffset;
            _pathLine.Points = _points.ToArray();
        }

        public void Cleanup() => 
            CleanLevels();

        private void OnLevelSelected(int id)
        {
            var data = _progressService.ProgressContainer.PersistentData;

            if (data.CompletedLevel < id)
            {
                data.CompletedLevel = id;
                _progressService.SaveProgress();
                
                BuildLevels();
            }
        }

        private void BuildLevels()
        {
            CleanLevels();
            
            int completedLevelId = _progressService.ProgressContainer.PersistentData.CompletedLevel;
            
            for (int i = 0; i < _points.Count; i++)
            {
                bool isOpened = completedLevelId + 1 >= i;
                
                LevelButton levelButton = Instantiate(_levelButtonPrefab, _parent);
                levelButton.Init(i, isOpened);
                levelButton.Selected += OnLevelSelected;

                //Vector2 offset = new Vector2((float)Screen.width / 2, -((float)Screen.height / 2 + _verticalOffset));
                levelButton.transform.localPosition = _points[i] + Vector2.up * _verticalOffset;

                _levelButtons.Add(levelButton);
            }
        }

        private void BuildPoints()
        {
            _points.Clear();
            
            int j = 0;
            int g = 0;
            
            for (int i = 0; i < _levelsCount; i++, j++)
            {
                Vector2 position;

                if (j >= _pathMap.Length)
                {
                    j = 0;
                    g++;
                }

                float aspect = (float)Screen.width / (float)Screen.height;
                
                position = new Vector2(_pathMap[j].x * aspect, _pathMap[j].y + _pathMap[^1].y * g);
                _points.Add(position);
            }
        }

        private void CleanLevels()
        {
            foreach (LevelButton levelButton in _levelButtons)
            {
                levelButton.Selected -= OnLevelSelected;
                Destroy(levelButton.gameObject);
            }
            
            _levelButtons.Clear();
        }
    }
}