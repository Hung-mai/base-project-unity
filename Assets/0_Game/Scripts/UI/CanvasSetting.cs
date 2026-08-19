using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasSetting : UICanvas
{
    public GameObject obj_soundOn;
    public GameObject obj_soundOff;
    public GameObject obj_musicOn;
    public GameObject obj_musicOff;
    public GameObject obj_vibrationOn;
    public GameObject obj_vibrationOff;

    public GameObject obj_ingame;
    public GameObject obj_menu;
    public bool isBackHome = false;

    public override void Open()
    {
        base.Open();

        obj_soundOn.SetActive(DataManager.ins.dt.isSound);
        obj_soundOff.SetActive(!DataManager.ins.dt.isSound);
        obj_musicOn.SetActive(DataManager.ins.dt.isMusic);
        obj_musicOff.SetActive(!DataManager.ins.dt.isMusic);
        obj_vibrationOn.SetActive(DataManager.ins.dt.isVibrate);
        obj_vibrationOff.SetActive(!DataManager.ins.dt.isVibrate);

        // obj_ingame.SetActive(IngameManager.ins != null);
        // obj_menu.SetActive(IngameManager.ins == null);

        // obj_settings.SetActive(true);
        // obj_areYouSure.SetActive(false);

    }
    
    public override void Close()
    {
        base.Close();
        // if(IngameManager.ins != null)
        // {
        //     IngameManager.ins.pause = false;
        // }
    }

    public void Btn_sound()
    {
        DataManager.ins.dt.isSound = !DataManager.ins.dt.isSound;
        obj_soundOn.SetActive(DataManager.ins.dt.isSound);
        obj_soundOff.SetActive(!DataManager.ins.dt.isSound);

        // VibrationManager.ins.MediumButton();
        // SoundManager.PlayEfxSound(SoundManager.ins.UIClick);
    }

    public void Btn_music()
    {
        DataManager.ins.dt.isMusic = !DataManager.ins.dt.isMusic;
        obj_musicOn.SetActive(DataManager.ins.dt.isMusic);
        obj_musicOff.SetActive(!DataManager.ins.dt.isMusic);

        if(DataManager.ins.dt.isMusic)
        {
            SoundManager.PlayMusicBg(SoundManager.ins.bgMusic);
        }
        else
        {
            SoundManager.StopMusicBg();
        }

        // VibrationManager.ins.MediumButton();
        // SoundManager.PlayEfxSound(SoundManager.ins.UIClick);
    }

    public void Btn_vibration()
    {
        DataManager.ins.dt.isVibrate = !DataManager.ins.dt.isVibrate;
        obj_vibrationOn.SetActive(DataManager.ins.dt.isVibrate);
        obj_vibrationOff.SetActive(!DataManager.ins.dt.isVibrate);

        // VibrationManager.ins.MediumButton();
        // SoundManager.PlayEfxSound(SoundManager.ins.UIClick);
    }

    public void Btn_close()
    {
        // VibrationManager.ins.MediumButton();
        // SoundManager.PlayEfxSound(SoundManager.ins.UIClick);

        // Close();
        // if(IngameManager.ins != null)
        // {
        //     // IngameManager.ins.pause = false;
        // }
    }

    public void Btn_home()
    {
        // VibrationManager.ins.MediumButton();
        // SoundManager.PlayEfxSound(SoundManager.ins.UIClick);
        // GameManager.ins.LoadScene(Constant.SCENE_HOME);
        Close();
    }

    public void Btn_replay()
    {
        // VibrationManager.ins.MediumButton();
        // SoundManager.PlayEfxSound(SoundManager.ins.UIClick);
        // GameManager.ins.LoadScene(Constant.SCENE_GAMEPLAY);
        Close();
    }
}
