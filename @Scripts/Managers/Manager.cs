using UnityEngine;

namespace BIS.Managers
{
    public class Manager : MonoBehaviour
    {
        private static Manager s_inctance;
        private static Manager Instacne
        {
            get
            {
                Init();
                return s_inctance;
            }
        }

        public static GameObject GO { get; private set; }

        private GameSceneManager _gameScene = new GameSceneManager();
        private ResourceManager _resource = new ResourceManager();
        private CameraManager _camera = new CameraManager();
        private SaveManager _save = new SaveManager();
        private UIManager _ui = new UIManager();

        public static GameSceneManager GameScene { get { return Instacne._gameScene; } }
        public static ResourceManager Resource { get { return Instacne._resource; } }
        public static CameraManager Camera { get { return Instacne._camera; } }
        public static SaveManager Save { get { return Instacne._save; } }
        public static UIManager UI { get { return Instacne._ui; } }

        private static void Init()
        {
            if (s_inctance == null)
            {
                GO = GameObject.Find("@Manager");
                if (GO == null)
                {
                    GO = new GameObject { name = "@Manager" };
                    GO.AddComponent<Manager>();
                }

                DontDestroyOnLoad(GO);

                //√ ±‚»≠
                s_inctance = GO.GetComponent<Manager>();
            }
        }
    }

}
