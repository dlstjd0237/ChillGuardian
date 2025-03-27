using BIS.Managers;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BIS.UI
{
    public class SlotUI : UIBase
    {
        private enum Images
        {
            Icon,
            Fill
        }

        private enum Texts
        {
            FillText,
            CostText
        }

        private float _time = 0;
        private bool _fill;
        private UnitSO _unitSO;

        [SerializeField] private Transform _spawnTrm;

        public void SetUpData(UnitSO unitSO)
        {
            BindImages(typeof(Images));
            BindTexts(typeof(Texts));

            GetImage((int)Images.Icon).sprite = unitSO.UnitIcon;
            GetImage((int)Images.Icon).color = Color.white;
            GetText((int)Texts.CostText).text = unitSO.Stat.cost.GetValue().ToString();

            BindEvent(gameObject, HandleClickEvent, Core.Define.EUIEvent.Click);

            _unitSO = unitSO;
        }

        private void HandleClickEvent(PointerEventData obj)
        {
            if (_fill == true || _unitSO == null)
                return;
            if (GameManager.Instance.SpendMoney((int)_unitSO.Stat.cost.GetValue()) == false)
                return;

            CoolTimePlay();
            Manager.Resource.Instantiate(_unitSO.UnitName).transform.position = _spawnTrm.position;
        }

        private void CoolTimePlay()
        {
            _fill = true;
            _time = 0;
            GetText((int)Texts.FillText).text = _unitSO.Stat.spawnCool.GetValue().ToString();
        }

        private void Update()
        {
            if (_fill == true)
            {
                _time += Time.deltaTime;

                float statValue = _unitSO.Stat.spawnCool.GetValue();

                GetText((int)Texts.FillText).text = (statValue - _time).ToString("F1");

                float fillAmount = Mathf.Clamp01(1.0f - (_time / statValue));
                GetImage((int)Images.Fill).DOFillAmount(fillAmount, 0.05f);

                if (statValue <= _time)
                {
                    GetText((int)Texts.FillText).text = string.Empty;
                    GetImage((int)Images.Fill).fillAmount = 0;

                    _fill = false;
                }
            }
        }
    }

}