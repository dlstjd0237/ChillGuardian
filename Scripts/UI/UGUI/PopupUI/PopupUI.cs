using UnityEngine;
using BIS.Managers;
using BIS.Core.Utility;
using DG.Tweening;
using System;
using BIS.Shared.Interface;

namespace BIS.UI.Popup
{
    public abstract class PopupUI : UIBase, IPopupUI
    {
        public GameObject PopupGO => gameObject;

        public override bool Init()
        {
            if (base.Init() == false)
                return false;


            Manager.UI.SetCanvas(gameObject);
            return true;
        }

        public virtual void ClosePopup(Action callBack = null)
        {
            Util.UIFadeOut(gameObject, true, 0.2f, callBack);
        }

        public virtual void OpenPopup()
        {
            Util.UIFadeOut(gameObject, false);
        }
    }
}
