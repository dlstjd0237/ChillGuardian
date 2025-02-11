using BIS.Stats;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[CreateAssetMenu(menuName = "SO/BIS/Unit")]
public class UnitSO : ScriptableObject
{
    [SerializeField] private string _unitName; public string UnitName { get { return _unitName; } }
    [SerializeField] private string _unitDisplayName; public string UnitDisplayName { get { return _unitDisplayName; } }
    [SerializeField] private Sprite _unitIcon; public Sprite UnitIcon { get { return _unitIcon; } }
    [SerializeField] private CharactersStat _stat; public CharactersStat Stat { get { return _stat; } }

#if UNITY_EDITOR
    private void OnValidate()
    {


        if (name != $"[{_unitDisplayName}]Unit")
        {
            string newName = $"[{_unitDisplayName}]Unit";

            // 실제로 Asset의 이름을 변경
            AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(this), newName);

            // AssetDatabase를 강제로 갱신
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
#endif
}
