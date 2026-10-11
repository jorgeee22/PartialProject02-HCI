using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class KioskManager : MonoBehaviour {
    public enum KitStatus { Available, Reserved, Charging, Loaned }

    [System.Serializable]
    public class QuestKit {
        public int kitID;
        public KitStatus status;
        public int lockerNumber;
    }

    [System.Serializable]
    public class InventoryData {
        public QuestKit[] kits;
    }

    public QuestKit[] inventory = new QuestKit[6];
    private int currentlySelectedKitID = -1;
    private string saveFilePath;
    private UIDocument uiDocument;

    public VisualTreeAsset mainMenuAsset;
    public VisualTreeAsset mainMenuSelectionAsset;
    public VisualTreeAsset borrowPeriodAsset;
    public VisualTreeAsset borrowPeriodSelectedAsset;
    public VisualTreeAsset summaryAsset;
    public VisualTreeAsset endScreenAsset;

    public VisualTreeAsset unavailableMainMenuAsset;
    public VisualTreeAsset alternateSummaryAsset;
    public VisualTreeAsset confirmationScreenAsset;
    public VisualTreeAsset lastMainMenuAsset;

    void Start() {
        uiDocument = GetComponent<UIDocument>();
        LoadInventory();
        CheckAvailabilityAndLoadScreen();
    }

    private void CheckAvailabilityAndLoadScreen() {
        bool hasAvailableKits = false;
        foreach (var kit in inventory) {
            if (kit.status == KitStatus.Available) {
                hasAvailableKits = true;
                break;
            }
        }

        if (hasAvailableKits) {
            LoadMainMenu(); 
        } else {
            LoadUnavailableMainMenu(); 
        }
    }

    private void SaveInventory() {
        InventoryData data = new InventoryData { kits = inventory };
        string json = JsonUtility.ToJson(data);

        PlayerPrefs.SetString("KioskInventory", json);
        PlayerPrefs.Save(); 
    }

    private void LoadInventory() {
        if (PlayerPrefs.HasKey("KioskInventory")) {
            string json = PlayerPrefs.GetString("KioskInventory");
            InventoryData data = JsonUtility.FromJson<InventoryData>(json);
            inventory = data.kits;
        } else {
            for (int i = 0; i < 6; i++) {
                KitStatus startingStatus = (i == 1) ? KitStatus.Available : KitStatus.Reserved;

                inventory[i] = new QuestKit {
                    kitID = i + 1,
                    status = startingStatus,
                    lockerNumber = i + 1
                };
            }
        }
    }

    private void LoadMainMenu() {
        uiDocument.rootVisualElement.Clear();
        mainMenuAsset.CloneTree(uiDocument.rootVisualElement);

        Button btn2 = uiDocument.rootVisualElement.Q<Button>("Btn2");
        if (btn2 != null) {
            btn2.clicked += () => {
                currentlySelectedKitID = 2;
                LoadMainMenuSelection();
            };
        }

        Button resetBtn = uiDocument.rootVisualElement.Q<Button>("ResetButton");
        if (resetBtn != null) {
            resetBtn.clicked += ResetKiosk;
        }
    }

    private void LoadMainMenuSelection() {
        uiDocument.rootVisualElement.Clear();
        mainMenuSelectionAsset.CloneTree(uiDocument.rootVisualElement);

        StartCoroutine(WaitAndExecute(1.0f, LoadBorrowPeriod));
    }

    private void LoadBorrowPeriod() {
        uiDocument.rootVisualElement.Clear();
        borrowPeriodAsset.CloneTree(uiDocument.rootVisualElement);

        Button backBtn = uiDocument.rootVisualElement.Q<Button>("BackButton");
        if (backBtn != null) backBtn.clicked += LoadMainMenu; 

        Button period90Btn = uiDocument.rootVisualElement.Q<Button>("Period90");
        if (period90Btn != null) period90Btn.clicked += LoadBorrowPeriodSelected;
    }

    private void LoadBorrowPeriodSelected() {
        uiDocument.rootVisualElement.Clear();
        borrowPeriodSelectedAsset.CloneTree(uiDocument.rootVisualElement);

        Button backBtn = uiDocument.rootVisualElement.Q<Button>("BackButton");
        if (backBtn != null) backBtn.clicked += LoadBorrowPeriod;

        Button continueBtn = uiDocument.rootVisualElement.Q<Button>("ContinueButton");
        if (continueBtn != null) continueBtn.clicked += LoadSummary;
    }

    private void LoadSummary() {
        uiDocument.rootVisualElement.Clear();
        summaryAsset.CloneTree(uiDocument.rootVisualElement);

        Button backBtn = uiDocument.rootVisualElement.Q<Button>("BackButton");
        if (backBtn != null) backBtn.clicked += LoadBorrowPeriodSelected;

        Button completeBtn = uiDocument.rootVisualElement.Q<Button>("CompleteButton");
        if (completeBtn != null) completeBtn.clicked += LoadEndScreen;
    }

    private void LoadEndScreen() {
        uiDocument.rootVisualElement.Clear();
        endScreenAsset.CloneTree(uiDocument.rootVisualElement);

        Button doneBtn = uiDocument.rootVisualElement.Q<Button>("DoneButton");
        if (doneBtn != null) {
            doneBtn.clicked += () => {
                if (currentlySelectedKitID > 0) {
                    inventory[currentlySelectedKitID - 1].status = KitStatus.Loaned;
                }
                SaveInventory();
                CheckAvailabilityAndLoadScreen();
            };
        }
    }

    private void LoadUnavailableMainMenu() {
        uiDocument.rootVisualElement.Clear();
        unavailableMainMenuAsset.CloneTree(uiDocument.rootVisualElement);

        VisualElement overlay = uiDocument.rootVisualElement.Q<VisualElement>("UsefulStepOverlay");
        if (overlay != null) overlay.style.display = DisplayStyle.None; 

        for (int i = 1; i <= 6; i++) {
            Label kitLabel = uiDocument.rootVisualElement.Q<Label>("Btn" + i);
            if (kitLabel != null) {
                kitLabel.pickingMode = PickingMode.Position;
                kitLabel.RegisterCallback<ClickEvent>(evt => {
                    if (overlay != null) overlay.style.display = DisplayStyle.Flex;
                });
            }
        }

        Button noThanksBtn = uiDocument.rootVisualElement.Q<Button>("NoThanksButton");
        if (noThanksBtn != null) {
            noThanksBtn.clicked += () => {
                if (overlay != null) overlay.style.display = DisplayStyle.None;
            };
        }

        Button reserveNowBtn = uiDocument.rootVisualElement.Q<Button>("ReserveNowButton");
        if (reserveNowBtn != null) reserveNowBtn.clicked += LoadAlternateSummary;

        Button resetBtn = uiDocument.rootVisualElement.Q<Button>("ResetButton");
        if (resetBtn != null) {
            resetBtn.clicked += ResetKiosk;
        }
    }

    private void LoadAlternateSummary() {
        uiDocument.rootVisualElement.Clear();
        alternateSummaryAsset.CloneTree(uiDocument.rootVisualElement);

        Button backBtn = uiDocument.rootVisualElement.Q<Button>("BackButton");
        if (backBtn != null) backBtn.clicked += LoadUnavailableMainMenu;

        Button continueBtn = uiDocument.rootVisualElement.Q<Button>("ContinueButton");
        if (continueBtn != null) continueBtn.clicked += LoadConfirmationScreen;
    }

    private void LoadConfirmationScreen() {
        uiDocument.rootVisualElement.Clear();
        confirmationScreenAsset.CloneTree(uiDocument.rootVisualElement);

        Button doneBtn = uiDocument.rootVisualElement.Q<Button>("DoneButton");
        if (doneBtn != null) doneBtn.clicked += LoadLastMainMenu;
    }

    private void LoadLastMainMenu() {
        uiDocument.rootVisualElement.Clear();
        lastMainMenuAsset.CloneTree(uiDocument.rootVisualElement);
    }

    private IEnumerator WaitAndExecute(float delay, System.Action nextAction) {
        yield return new WaitForSeconds(delay);
        nextAction?.Invoke();
    }

    private void ResetKiosk() {
        PlayerPrefs.DeleteKey("KioskInventory");
        PlayerPrefs.Save();

        currentlySelectedKitID = -1;

        LoadInventory();
        LoadMainMenu();
    }
}