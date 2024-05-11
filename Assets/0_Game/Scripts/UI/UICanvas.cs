using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas : MonoBehaviour
{
    public bool IsDestroyOnClose = false;

    public RectTransform m_RectTransform;

    private void Start()
    {
        Init();
    }

    protected void Init()
    {
        
    }

    public virtual void Setup()
    {
        UIManager.ins.AddBackUI(this);
        UIManager.ins.PushBackAction(this, BackKey);
    }

    public virtual void BackKey()
    {

    }

    public virtual void Open()
    {
        gameObject.SetActive(true);
        m_RectTransform.SetAsLastSibling();
        //anim
    }

    public virtual void Close()
    {
        // UIManager.ins.RemoveBackUI(this);
        //anim
        gameObject.SetActive(false);
        if (IsDestroyOnClose)
        {
            Destroy(gameObject);
        }
        
    }

    public virtual void Close(float delayTime)
    {
        // Invoke(nameof(CloseDirectly), delayTime);
        StartCoroutine(waitToClose());
        IEnumerator waitToClose()
        {
            yield return new WaitForSecondsRealtime(delayTime);
            Close();
        }
    }
}
