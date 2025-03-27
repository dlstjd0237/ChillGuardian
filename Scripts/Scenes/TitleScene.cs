using BIS.Core;
using BIS.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : BaseScene
{
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        StartLoadAssets();
        SceneType = Define.ESceneType.Title;

        return true;
    }



    private void StartLoadAssets()//���⼭ �����������
    {
        Manager.Resource.LoadAllAsync<Object>("PreLoad", (key, count, totalCount) =>
         {
             Debug.Log($"{key} {count}/{totalCount}");

             if (count == totalCount)
             {
             }
         });
    }
}
