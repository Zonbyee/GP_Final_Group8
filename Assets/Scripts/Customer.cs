using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    private NavMeshAgent agent;

    private CustomerSpot targetSpot;
    private bool hasArrived = false;

    public GameObject burgerRecipeUI;
    public GameObject salmonRecipeUI;

    private int expectedMealIndex = -1;




    private bool isLeaving = false;
    private CustomerSpot assignedSpot;  // ⭐ 修正：加入編號的 spot reference
    private FoodArea myFoodArea;

    public void SetFoodArea(FoodArea area)
    {
        myFoodArea = area;
    }

    public void SetExpectedMeal(int mealIndex)
    {
        expectedMealIndex = mealIndex;
    }



    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // -------------------------------
    //   進場：走到指定 CustomerSpot
    // -------------------------------
    public void SetDestination(CustomerSpot spot)
    {
        assignedSpot = spot;
        targetSpot = spot;

        agent.SetDestination(spot.transform.position);
        spot.OccupySpot(this);
    }

    void Update()
    {
        // =============================
        // 抵達座位 (尚未抵達時才檢查)
        // =============================
        if (!isLeaving && !hasArrived && targetSpot != null)
        {
            if (!agent.pathPending &&
                agent.remainingDistance <= agent.stoppingDistance &&
                (!agent.hasPath || agent.velocity.sqrMagnitude == 0f))
            {
                OnReachedSpot();
            }
        }

        // =============================
        // 離場：抵達 leave point
        // =============================
        if (isLeaving)
        {
            if (!agent.pathPending &&
                agent.remainingDistance <= 0.2f &&
                (!agent.hasPath || agent.velocity.sqrMagnitude == 0f))
            {
                Debug.Log("[Customer] 到達離開點 → 客人消失");
                CustomerManager.Instance.RemoveCustomer(this);
            }
        }
    }

    // 客人坐下時會被呼叫
    private void OnReachedSpot()
    {
        hasArrived = true;
        Debug.Log("Customer reached spot: " + targetSpot.name);
        targetSpot.OnCustomerArrived();

        ShowCorrectRecipe();
    }

    private void ShowCorrectRecipe()
    {
        // 關閉全部（安全防呆）
        if (burgerRecipeUI != null) burgerRecipeUI.SetActive(false);
        if (salmonRecipeUI != null) salmonRecipeUI.SetActive(false);

        // 根據餐點顯示
        if (expectedMealIndex == 0)
        {
            // 顧客想吃漢堡
            if (burgerRecipeUI != null) burgerRecipeUI.SetActive(true);
        }
        else if (expectedMealIndex == 1)
        {
            // 顧客想吃鮭魚香菇三明治
            if (salmonRecipeUI != null) salmonRecipeUI.SetActive(true);
        }

        Debug.Log($"[Customer] 顯示餐點食譜 → Index: {expectedMealIndex}");
    }


    // -------------------------------
    //          上菜判斷
    // -------------------------------
    public void OnFoodServed(bool isCorrect)
    {
        if (burgerRecipeUI != null) burgerRecipeUI.SetActive(false);
        if (salmonRecipeUI != null) salmonRecipeUI.SetActive(false);

        
        if (isCorrect)
        {
            Debug.Log("[Customer] 食物正確 → 開始吃...");
        }
        else
        {
            Debug.Log("[Customer] 食物錯誤 → 客人還是會離開");
        }

        


        // ⭐ 食完後等 5 秒離開
        StartCoroutine(LeaveAfterDelay());
    }

    // -------------------------------
    //    吃完飯 → 等 5 秒 → 離場
    // -------------------------------
    private IEnumerator LeaveAfterDelay()
    {
        yield return new WaitForSeconds(5f);

        Debug.Log("[Customer] 用餐完畢 → 客人準備離開");

        // ⭐ 食完後 → 清除桌上的食物
        if (myFoodArea != null)
        {
            myFoodArea.ClearFoodOnTable();
            myFoodArea.ClearCustomer();   // 讓 FoodArea 空出來
            myFoodArea = null;
        }


        // ⭐ 先釋放座位
        if (assignedSpot != null)
        {
            assignedSpot.ReleaseSpot();
            assignedSpot = null;

            // ⭐ 告訴 CustomerManager 座位空了，可以 spawn 新客人
            CustomerManager.Instance.NotifyCustomerLeft();
        }

        // ⭐ 前往離開點
        CustomerManager.Instance.MoveCustomerToLeavePoint(this);
    }

    // -------------------------------
    //         設定離開路徑
    // -------------------------------
    public void SetDestinationToLeavePoint(Vector3 leavePos)
    {
        agent.SetDestination(leavePos);
        isLeaving = true;
    }


    // -------------------------------
    // 保險：物件銷毀時釋放座位
    // -------------------------------
    void OnDestroy()
    {
        if (assignedSpot != null)
        {
            assignedSpot.ReleaseSpot();
            assignedSpot = null;
        }
    }
}