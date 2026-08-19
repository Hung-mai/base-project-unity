using System.Collections;
using System.Collections.Generic;
using AssetKits.ParticleImage;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasClaimReward_2 : UICanvas
{
    public CanvasGroup _canvasGroupClaim;
    public Animator _animator;
    public GameObject[] allObjects;
    public Transform[] trans_1;
    public Transform[] trans_2;
    public Transform[] trans_3;
    public Transform[] trans_4;
    public Transform[] trans_5;
    public Transform goldTrans;
    public Transform hintTrans;
    public Transform wandTrans;
    public Transform shadowTrans;
    public TMP_Text txt_gold;
    public TMP_Text txt_hint;
    public TMP_Text txt_wand;
    public TMP_Text txt_shadowLine;
    public List<Transform> listTrans = new List<Transform>();
    public GameObject obj_buttonClaim;
    public AnimationCurve curve;
    public ParticleImage[] particleImages;
    public ParticleImage[] fx_done;
    public Transform dichDen;
    public GameObject obj_buttonClose;
    // private bool isClaimed = false;
    public Image img_bg;
    public int valueGold;
    public int valueHint;
    public int valueWand;
    public int valueShadowLine;
    public string placement = "big_event";
    public string type_event = "claim_big_event";
    // private bool noAddHeart = false;
    private bool _levelReward = false;

    public override void Open()
    {
        // UIManager.ins.UnBlockUI();
        // SoundManager.PlayEfxSound(SoundManager.ins.reward_claim);
        base.Open();
        _canvasGroupClaim.alpha = 1;
        img_bg.color = new Color(0, 0, 0, 0);
        img_bg.DOFade(0.9f, 0.2f);
        goldTrans.gameObject.SetActive(false);
        hintTrans.gameObject.SetActive(false);
        wandTrans.gameObject.SetActive(false);
        shadowTrans.gameObject.SetActive(false);

        obj_buttonClose.SetActive(false);

        StartCoroutine(ie_animClaim());
    }

    public override void Close()
    {
        base.Close();
    }

    public void SetUp(string _placement, string _type_event, bool levelReward = false)
    {
        placement = _placement;
        type_event = _type_event;

        valueGold = 0;
        valueHint = 0;
        valueWand = 0;
        valueShadowLine = 0;

        listTrans = new List<Transform>();
        _levelReward = levelReward;
    }

    // public void AddReward(RewardType rewardType, int value)
    // {
    //     switch (rewardType)
    //     {
    //         case RewardType.gold:
    //             valueGold += value;
    //             txt_gold.text = valueGold.ToString();
    //             goldTrans.gameObject.SetActive(true);
    //             if (listTrans.Contains(goldTrans) == false) listTrans.Add(goldTrans);

    //             break;
    //         case RewardType.hint:
    //             valueHint += value;
    //             txt_hint.text = "x" + valueHint.ToString();
    //             hintTrans.gameObject.SetActive(true);
    //             if (listTrans.Contains(hintTrans) == false) listTrans.Add(hintTrans);
    //             break;
    //         case RewardType.wand:
    //             valueWand += value;
    //             txt_wand.text = "x" + valueWand.ToString();
    //             wandTrans.gameObject.SetActive(true);
    //             if (listTrans.Contains(wandTrans) == false) listTrans.Add(wandTrans);
    //             break;
    //         case RewardType.shadowLine:
    //             valueShadowLine += value;
    //             txt_shadowLine.text = "x" + valueShadowLine.ToString();
    //             shadowTrans.gameObject.SetActive(true);
    //             if (listTrans.Contains(shadowTrans) == false) listTrans.Add(shadowTrans);
    //             break;
    //     }
    // }

    public void Btn_close()
    {
        // SoundManager.ClickUI();
        _canvasGroupClaim.DOFade(0, 0.2f).OnComplete(() =>
        {
            Close();

            if(_levelReward)
            {
                // UIManager.ins.GetUI<CanvasMainMenu>(UIID.UICMainMenu).AnimNextLevel();
            }

            if(placement.Equals("starTreasure"))
            {
                // BigEventTreasureManager.ins.runningAnim = false;
                // UIManager.ins.GetUI<CanvasMainMenu>(UIID.UICMainMenu).CheckLevelReward();
            }
        });
    }

    private IEnumerator ie_animClaim()
    {
        yield return Cache.GetWFS(0.2f);
        if (listTrans.Count == 1)
        {
            for (int i = 0; i < listTrans.Count; i++)
            {
                listTrans[i].transform.position = trans_1[i].position;
            }
        }
        else if (listTrans.Count == 2)
        {
            for (int i = 0; i < listTrans.Count; i++)
            {
                listTrans[i].transform.position = trans_2[i].position;
            }
        }
        else if (listTrans.Count == 3)
        {
            for (int i = 0; i < listTrans.Count; i++)
            {
                listTrans[i].transform.position = trans_3[i].position;
            }
        }
        else if (listTrans.Count == 4)
        {
            for (int i = 0; i < listTrans.Count; i++)
            {
                listTrans[i].transform.position = trans_4[i].position;
            }
        }
        else if (listTrans.Count == 5)
        {
            for (int i = 0; i < listTrans.Count; i++)
            {
                listTrans[i].transform.position = trans_5[i].position;
            }
        }
        float _scale = 1;
        if (listTrans.Count == 1)
        {
            _scale = 1.5f;
        }
        else if (listTrans.Count == 2)
        {
            _scale = 1.2f;
        }
        else if (listTrans.Count == 3)
        {
            _scale = 1.0f;
        }
        else if (listTrans.Count == 4)
        {
            _scale = 1;
        }
        for (int i = 0; i < listTrans.Count; i++)
        {
            listTrans[i].gameObject.SetActive(true);
            listTrans[i].localScale = Vector3.one * _scale;
        }

        obj_buttonClose.SetActive(true);
    }
}
