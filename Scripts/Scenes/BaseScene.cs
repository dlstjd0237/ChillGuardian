using BIS.Init;
using static BIS.Core.Define;




public class BaseScene : InitBase
{
    
    public ESceneType SceneType { get; protected set; } = ESceneType.Unknown;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;


        return true;
    }

}
