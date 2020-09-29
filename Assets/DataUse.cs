using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using Firebase.Extensions;
using UnityEngine.UI;
using UnityEngine.Events;
using Proyecto26;

public class DataUse : MonoBehaviour
{
    public UnityEvent OnFriebaseitialized = new UnityEvent();
    DatabaseReference reference;
    [Header("註冊ID InputField")]
    public InputField UserIDInputField;
    [Header("註冊密碼 InputField")]
    public InputField PasswordInputField;
    public Text ScoreText;
    static public string UserID;
    static public string Password;
    static public int Score;
    public List<string> DatabaseData;

    [Header("登入ID InputField")]
    public InputField SignInUserIDInputField;
    [Header("登入密碼 InputField")]
    public InputField SignInPasswordInputField;
    // 如果帳號密碼有錯要顯示錯誤資訊
    public Text ErrorInfo;

    [Header("登入畫面UI物件")]
    public GameObject SignInUI;
    [Header("首頁UI物件")]
    public GameObject MenuUI;

    void Start()
    {
        // Firebase初始化
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                Debug.Log("無法初始化" + task.Exception);
            }
            OnFriebaseitialized.Invoke();
        });
        // 串接Firebase資料表網址(每個資料庫網址不同)
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://jumpgame-6c3fd.firebaseio.com/");
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }
    // 按下註冊按鈕將使用者資料記錄到Firebase中
    public void SaveDataToFirebase()
    {
        UserID = UserIDInputField.text;
        Password = PasswordInputField.text;
        Score = 0;
        PostToDatabase();
    }
    // 發送給Database
    void PostToDatabase()
    {
        User user = new User();
        RestClient.Put("https://jumpgame-6c3fd.firebaseio.com/" + UserID + ".json", user);
    }

    // 從Firebase抓取資料
    public void GetDataFromFirebase()
    {
        DatabaseData.Clear();

        RestClient.Get("https://jumpgame-6c3fd.firebaseio.com/" + SignInUserIDInputField.text + ".json").Then(response =>
          {
              // 如果Firebase有登入ID名稱
              if (response.Text != "null")
              {
                  DatabaseData.Add(response.Text);
                  string[] Datas = DatabaseData[0].Split(',');
                  string[] PassWordDatas = Datas[0].Split(':');
                  string[] PassWordDatas1 = PassWordDatas[1].Split('"');
                  if(SignInPasswordInputField.text == PassWordDatas1[1])
                  {
                      ErrorInfo.text = "帳號密碼正確，登入";
                      StartCoroutine("StartMenu");
                  }
                  else
                  {
                      ErrorInfo.text = "密碼錯誤";
                  }
              }
              // 如果Firebase沒有登入ID名稱
              else
              {
                  ErrorInfo.text = "此帳號尚未註冊";
              }
          });
    }

    IEnumerator StartMenu()
    {
        yield return new WaitForSeconds(3f);
        SignInUI.SetActive(false);
        MenuUI.SetActive(true);
    }
}
