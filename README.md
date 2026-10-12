# Quest 3 Kiosk – Project 02 (HCI)

## Project Summary

An interactive prototype of a **Meta Quest 3 checkout kiosk**, built in Unity with **UI Toolkit** (UXML/USS) from an original Figma design. The kiosk greets the user, shows the status of six headset kits (Available, Reserved, Charging, Loaned), and lets them borrow an available kit by choosing a borrow period and confirming a summary. When no kits are available, the kiosk offers the best next option: reserving the next kit that becomes free.

Two scenarios are implemented:

- **Scenario A:** Borrow an available kit.
- **Scenario B:** Reserve a kit when none are available.

The kiosk state is stored with `PlayerPrefs`, so it persists between sessions until it is reset.

## Links

| Resource | Link |
|---|---|
| Original Figma design | [Project-01 on Figma](https://www.figma.com/design/5OQYsNzHx2vwk3AVBOPO9T/Project-01?node-id=23-2&t=ZsEe2tYVGqPVdT5R-1) |
| GitHub Release (Windows build) | [Quest Checkout v1.0.0](https://github.com/jorgeee22/PartialProject02-HCI/releases/tag/v1.0.0) |
| Demo video | [TODO: paste video link] |
| Design report | [design.md](design.md) |
| Testing report | [testing.md](testing.md) |
| AI usage log | [ai-use.md](ai-use.md) |

## Team Contributions

| Member | Contribution |
|---|---|
| **Jorge Ruiz** | Designed and built the full **Scenario A** screens. |
| **Carlos Béjar** | Designed and built the full **Scenario B** screens. |
| **Carlos Cruz** | Wrote the logic that makes both scenarios work: screen navigation, kit inventory, persistence, and reset controls. |
| **Whole team** | Wrote the documentation (design report, testing report, AI usage log, README) and recorded the demo video. |

## Unity Version

**Unity `6000.6.3f1`**

> Open the project only with this exact version to avoid compatibility issues. If you don't have it, install it from Unity Hub (*Installs → Install Editor → Archive*).

## Setup (Running from Unity)

1. Clone or download this repository.
2. In **Unity Hub**, click **Add → Add project from disk** and select the project folder.
3. Open it with **Unity 6000.6.3f1**.
4. In the **Project** window, open the scene **`MainScene`**.
5. Press **Play** ▶.

## Running the Build (Windows)

The build was made for **Windows**.

1. Go to the [GitHub Release](https://github.com/jorgeee22/PartialProject02-HCI/releases/tag/v1.0.0).
2. Download `QuestKiosk-v1.0.0.zip` and unzip it (keep all files and folders together).
3. Run the `.exe` inside the unzipped folder.

## Controls

The kiosk is operated with the **mouse** (or a touchscreen) by clicking buttons.

| Control | Action |
|---|---|
| **Available** label / **Select** button | Selects the available kit (Kit 2) |
| **90 min** option | Chooses the borrow period |
| **Continue** | Moves to the next screen |
| **Back** | Returns to the previous screen |
| **Complete** | Confirms the loan |
| **Done** | Finishes the flow and saves the state |
| **No Kits Available? Tap Here to See Best Next Option** | Opens the reservation offer (Scenario B) |
| **Reserve Now / No Thanks** | Accepts or dismisses the reservation offer |
| **Reset Scenarios** (blue, top right) | Restores the initial data (see below) |

## Resetting the Data

The blue **Reset Scenarios** button (top right) is available on the main menu, the "no kits available" menu, and the final screen. Pressing it:

- Deletes the saved inventory and the "Scenario B completed" flag.
- Restores the initial inventory: **Kit 2 = Available**, all other kits **Reserved**.
- Returns to the main menu, ready to test Scenario A.

Press **Reset Scenarios** before each testing round.

## How to Test the Scenarios

### Scenario A – Borrow an available kit

1. Press **Reset Scenarios** to start with clean data (one green, available kit is shown).
2. Click the green **Available / Select** kit.
3. After a brief selection screen (~1 second), choose **90 min** as the borrow period.
4. Click **Continue**.
5. On the summary screen, click **Complete**.
6. On the end screen, click **Done**. The kit is now marked **Loaned**.

### Scenario B – Reserve when no kits are available

Scenario B starts automatically after finishing Scenario A, because no kits remain available.

1. The main menu now shows no available kits.
2. Click the orange **"No Kits Available? Tap Here to See Best Next Option"** button.
3. Click **Reserve Now** in the overlay (**No Thanks** closes it so you can reopen it).
4. On the summary screen, click **Continue**.
5. On the confirmation screen, click **Done**. The final menu appears and Scenario B is marked as completed.

> If the app is closed and reopened, it remembers its state. Use **Reset Scenarios** to start over.

## Known Limitations

- The user ("Renata") and ID are hard-coded; there is no real authentication or locker hardware integration.
- The UI was designed for the Figma resolution (1920×1080); other resolutions or aspect ratios may look different.
- The build was only made and tested on **Windows**.
