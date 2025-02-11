using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace BIS.Stats
{
    [CreateAssetMenu(menuName = "SO/BIS/Stat/EntityStat")]
    public class EntityStat : CharactersStat
    {
        [SerializeField] private string _displayName;
        protected override void OnEnable()
        {
            base.OnEnable();

            Type playerStatType = typeof(EntityStat);

            foreach (StatType statType in Enum.GetValues(typeof(StatType)))
            {
                string statName = LowerFirstChar(statType.ToString());

                try
                {
                    FieldInfo playerStatField = playerStatType.GetField(statName);
                    Stat stat = playerStatField.GetValue(this) as Stat;
                    _statDictionary.Add(statType, stat);
                }
                catch
                {
                    Debug.Log("Error : Stat Don't Add");
                }
            }
        }
        public Stat GetStatByType(StatType statType)
        {
            return _statDictionary[statType];
        }
        private string LowerFirstChar(string input)
        {
            return char.ToLower(input[0]) + input.Substring(1);
        }


#if UNITY_EDITOR
        private void OnValidate()
        {
            if (name != $"[{_displayName}]Stat")
            {
                string newName = $"[{_displayName}]Stat";

                // 실제로 Asset의 이름을 변경
                AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(this), newName);

                // AssetDatabase를 강제로 갱신
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }
#endif
    }
}