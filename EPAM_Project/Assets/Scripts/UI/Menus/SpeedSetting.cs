using Stats;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menus
{
    public class SpeedSetting : MonoBehaviour
    {
        private Slider slider;

        [SerializeField] private EnemyStats baseStats;
        private void Start()
        {
            slider = GetComponent<Slider>();
            slider.maxValue = baseStats.Speed.maxValue;
            slider.minValue = baseStats.Speed.minValue;
            slider.value = baseStats.Speed.Value;
        }

        public void ChangeSetting(float value)
        {
            baseStats.Speed.Value = value;
        }
    }
}