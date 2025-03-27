using UnityEngine;

namespace BIS.Data
{
    [CreateAssetMenu(menuName = "BIS/SO/Data/SaveID")]
    public class SaveIDSO : ScriptableObject
    {
        public int saveId;
        public string saveName;
    }
}
