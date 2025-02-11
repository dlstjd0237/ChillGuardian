using System.Collections.Generic;
using UnityEngine;
using BIS.Core;
using System;

namespace BIS.UI
{
    public class SlotBoxUI : UIBase
    {
        [SerializeField] private List<SlotUI> _slotUI;
        [SerializeField] GameEventChannelSO _gameEventChannelSO;
        [SerializeField] private UnitSO _defualtSO;
        [SerializeField] private List<UnitSO> _unitSOList;

        private int _slotCell = 1;
        public override bool Init()
        {
            if (base.Init() == false)
                return false;

            _slotUI[0].SetUpData(_defualtSO);
            _gameEventChannelSO.AddListener<AddChillEvent>(HandleAddChill);
            return true;
        }

        private void OnDestroy()
        {
            _gameEventChannelSO.RemoveListener<AddChillEvent>(HandleAddChill);

        }

        private void HandleAddChill(AddChillEvent evt)
        {
            if (_slotCell >= 5)
                return;

            UnitSO unitSO = _unitSOList[0];
            switch (evt.addType)
            {
                case Define.EGuyType.Chill:
                    unitSO = _unitSOList[1];
                    break;
                case Define.EGuyType.Winston_S_Churchill:
                    unitSO = _unitSOList[3];
                    break;
                case Define.EGuyType.CaChill:
                    unitSO = _unitSOList[4];
                    break;
                case Define.EGuyType.Chill_Mk_1:
                    unitSO = _unitSOList[2];
                    break;
            }
            _slotUI[_slotCell++].SetUpData(unitSO);
        }
    }
}