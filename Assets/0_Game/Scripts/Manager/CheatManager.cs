using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CheatManager : MonoBehaviour
{
    public GameObject obj_popupCheat;
    public CanvasGroup canvasGroup;
    public Toggle toggleHideUI;
    public Toggle toggle5Cot;
    public Toggle toggletatItem;
    public TMP_InputField level_InputField;
    // public TMP_InputField zoom_InputField;
    public TMP_InputField gold_InputField;

    public void Btn_OpenCheat()
    {
        obj_popupCheat.SetActive(true);
        gold_InputField.text = DataManager.ins.dt.gold.ToString();
        level_InputField.text = "";
    }

    public void Btn_CloseCheat()
    {
        obj_popupCheat.SetActive(false);
    }

    public void Btn_okeCheat()
    {
        int level = -1;
        if(int.TryParse(level_InputField.text, out level))
        {
            if(level >= 1) 
            {
                DataManager.ins.dt.level = level - 1;
                SceneManager.LoadScene(Constant.SCENE_GAMEPLAY);
                UIManager.ins.CloseAllUI();
                UIManager.ins.OpenUI(UIID.UICGamePlay);
            }
        }

        if(int.TryParse(gold_InputField.text, out int gold))
        {
            DataManager.ins.dt.gold = gold;
        }

        canvasGroup.alpha = toggleHideUI.isOn ? 0 : 1;
        obj_popupCheat.SetActive(false);
    }
}
