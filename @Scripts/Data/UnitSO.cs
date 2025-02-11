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

            // ������ Asset�� �̸��� ����
            AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(this), newName);

            // AssetDatabase�� ������ ����
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
#endif
}
