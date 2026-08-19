using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TimeManager : MonoBehaviour
{
    public static TimeManager ins;
    [HideInInspector] public bool isTest = false;
    [HideInInspector] public float realtimeGetInternet = -9999;//Lưu thời điểm get time internet gần nhất (Tính theo Time.realtimeSinceStartup)
    [HideInInspector] public DateTime datetimeInternet;//Thời gian lấy từ internet
    public bool getTimeInternetSuccess = false;
    private bool chekPlaying = true;
    // public CanvasDailyChallenge canvasDailyChallenge;

    private void Awake()
    {
        ins = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public int GetTotalDaysNow()
    {
        DateTime timeNow = GetTimeLocal();
        return (int)timeNow.Subtract(Constant.baseTime).TotalDays;
    }

    public int GetTotalSecondsNow()
    {
        DateTime timeNow = GetTimeLocal();
        return (int)timeNow.Subtract(Constant.baseTime).TotalSeconds;
    }

    public void StartGet()
    {
        StartCoroutine(GetTimeInternet(3, CheckTime, null));
    }

    public void CheckTime()
    {
        DateTime timeNow = GetTimeLocal();
        StartCountInDay();
        // BigEventTreasureManager.ins.CheckWhatEvent();

        // GameManager.ins.dayChallengeFirebase = timeNow.Day.ToString();
    }

    private void StartCountInDay()
    {
        StartCoroutine(CountdownToday());
    }

    private IEnumerator CountdownToday()
    {
        yield return null;
        // Lấy số giây còn lại trong hôm nay
        DateTime now = GetTimeLocal();
        DateTime endOfDay = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);
        int secondsLeft = (int)(endOfDay - now).TotalSeconds + 1;

        // reset chỗ này
        int dayNow = TimeNowByDays();
        // if (dayNow > DataManager.ins.dt.lastOnline)
        // {
        //     DataManager.ins.dt.claimFreeToday = false;
        //     DataManager.ins.dt.dailyRewardIndex = 0;

        //     // check daily challenge
        //     if (DataManager.ins.dt.curYearChallenge < now.Year)
        //     {
        //         DataManager.ins.dt.curYearChallenge = now.Year;
        //         DataManager.ins.dt.curMonthChallenge = now.Month;

        //         for (int i = 0; i < DataManager.ins.dt.challengeStatus.Length; i++)
        //         {
        //             DataManager.ins.dt.challengeStatus[i] = 0;
        //         }

        //         for (int i = 0; i < DataManager.ins.dt.challengeRewardStatus.Length; i++)
        //         {
        //             DataManager.ins.dt.challengeRewardStatus[i] = false;
        //         }

        //         for (int i = 0; i < DataManager.ins.dt.challengeRetry.Length; i++)
        //         {
        //             DataManager.ins.dt.challengeRetry[i] = -1;
        //         }
        //     }
        //     else if (DataManager.ins.dt.curMonthChallenge < now.Month)
        //     {
        //         DataManager.ins.dt.curYearChallenge = now.Year;
        //         DataManager.ins.dt.curMonthChallenge = now.Month;

        //         for (int i = 0; i < DataManager.ins.dt.challengeStatus.Length; i++)
        //         {
        //             DataManager.ins.dt.challengeStatus[i] = 0;
        //         }

        //         for (int i = 0; i < DataManager.ins.dt.challengeRewardStatus.Length; i++)
        //         {
        //             DataManager.ins.dt.challengeRewardStatus[i] = false;
        //         }

        //         for (int i = 0; i < DataManager.ins.dt.challengeRetry.Length; i++)
        //         {
        //             DataManager.ins.dt.challengeRetry[i] = -1;
        //         }
        //     }
        // }

        // DataManager.ins.dt.lastOnline = dayNow;

        // // Đếm ngược bằng int--
        // while (secondsLeft > 0)
        // {
        //     canvasDailyChallenge.txt_timeLeft.text = GameHelper.FormatTimeHHMMSS(secondsLeft);
        //     yield return new WaitForSeconds(1f);
        //     secondsLeft--;
        // }

        // Khi hết ngày → chạy tiếp cho ngày tiếp theo
        StartCoroutine(CountdownToday());
    }

    public int TimeNowByDays()
    {
        DateTime timeNow = GetTimeLocal();
        return (int)timeNow.Subtract(Constant.baseTime).TotalDays;
    }

    public DateTime GetTimeLocal()
    {
        return DateTime.Now;
        if (realtimeGetInternet > 0)
        {
            //Đã từng lấy thời gian từ Internet
            datetimeInternet = datetimeInternet.AddSeconds(Time.realtimeSinceStartup - realtimeGetInternet);
            realtimeGetInternet = Time.realtimeSinceStartup;
            return datetimeInternet;
        }
        else
        {
            return DateTime.Now;
        }
    }

    public IEnumerator GetTimeInternet(int timeOut = 3, Action OnFinish = null, Action OnFail = null)
    {
        while (realtimeGetInternet < 0)
        {
            bool isFinish = false;
            UnityWebRequest requestGoogle = new UnityWebRequest("https://www.google.com");
            UnityWebRequest requestWTA = UnityWebRequest.Get("https://worldtimeapi.org/api/ip");
            requestGoogle.timeout = requestWTA.timeout = timeOut;//Giới hạn thời gian get time từ Internet
            yield return requestGoogle.SendWebRequest();
#pragma warning disable CS0618
            if (requestGoogle.isHttpError || requestGoogle.isNetworkError || requestGoogle.error != null)
            {//Nếu get time lỗi thì lấy time từ worldtimeapi
#pragma warning restore CS0618
                Debug.LogError("Lỗi: GetGoogleTime_1 " + requestGoogle.error);
            }
            else if (requestGoogle.result == UnityWebRequest.Result.ConnectionError || requestGoogle.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error in Google request: " + requestGoogle.error);
            }
            else
            {//Nếu lấy đc time từ Google
                string dateString = requestGoogle.GetResponseHeader("Date");
                if (!string.IsNullOrEmpty(dateString))
                {
                    if (DateTime.TryParse(dateString, out DateTime dateTime))
                    {
                        Debug.Log("Google's time format: " + dateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                        datetimeInternet = dateTime;
                        realtimeGetInternet = Time.realtimeSinceStartup;
                        isFinish = true;
                        this.getTimeInternetSuccess = true;
                        OnFinish?.Invoke();
                        Debug.LogWarning("GetGoogleTime: " + datetimeInternet.ToShortDateString());
                    }
                    else
                    {
                        Debug.LogError("Failed to parse date string.");
                    }
                }
                else
                {
                    Debug.LogError("Date header not found in the response.");
                }
            }
            ///Nếu vẫn chưa lấy đc time từ Google thì sẽ lấy time của Worldtimeapi
            if (isFinish == false)
            {
                yield return requestWTA.SendWebRequest();
#pragma warning disable CS0618
                if (requestWTA.isNetworkError || requestWTA.downloadHandler == null || requestWTA.downloadHandler.text == null)
                {
#pragma warning restore CS0618
                    Debug.LogError("Lỗi: GetTimeFromWorldTimeApi_1 " + requestWTA.error);
                    isFinish = true;
                    OnFail?.Invoke();
                }
                else if (requestWTA.result == UnityWebRequest.Result.ConnectionError || requestWTA.downloadHandler == null || string.IsNullOrEmpty(requestWTA.downloadHandler.text))
                {
                    Debug.LogError("Error in World Time API request: " + requestWTA.error);
                    OnFail?.Invoke();
                }
                else
                {

                    DateTimeResponse response = JsonUtility.FromJson<DateTimeResponse>(requestWTA.downloadHandler.text);
                    string dateString = response.datetime;
                    Debug.LogWarning("String GetTimeFromWorldTimeApi: " + dateString);
                    if (!string.IsNullOrEmpty(dateString))
                    {
                        if (DateTime.TryParse(dateString, out DateTime dateTime))
                        {
                            Debug.Log("GetTimeFromWorldTimeApi format: " + dateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                            datetimeInternet = dateTime;
                            realtimeGetInternet = Time.realtimeSinceStartup;
                            isFinish = true;
                            this.getTimeInternetSuccess = true;
                            OnFinish?.Invoke();
                            Debug.LogWarning("GetTimeFromWorldTimeApi: " + datetimeInternet.ToShortDateString());
                        }
                        else
                        {
                            Debug.LogError("Failed to parse date string.");
                        }
                    }
                    else
                    {
                        Debug.LogError("Date header not found in the response.");
                    }

                    //datetimeInternet = DateTime.Parse(stringToday);
                    //realtimeGetInternet = Time.realtimeSinceStartup;
                    //isFinish = true;
                    //this.getTimeInternetSuccess = true;
                    //OnFinish?.Invoke();
                    //Debug.LogWarning("GetTimeFromWorldTimeApi: " + datetimeInternet.ToShortDateString());

                }
            }
            yield return new WaitForSeconds(5f);
        }
    }


}

[System.Serializable]
public class DateTimeResponse
{
    public string datetime;
}
