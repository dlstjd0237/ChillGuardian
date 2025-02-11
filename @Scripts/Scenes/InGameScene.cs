using BIS.Managers;
using Unity.Cinemachine;
using UnityEngine;
using static BIS.Core.Define;

public class InGameScene : BaseScene
{
    [SerializeField] private CinemachineCamera _mainCame;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        SceneType = ESceneType.InGame;

        Manager.GameScene.SetMainCamera(_mainCame);

        return true;
    }


}
