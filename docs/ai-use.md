# AI Usage Declaration (`docs/ai-use.md`)

This log documents artificial intelligence collaboration utilized during the development, debugging, testing, and documentation of the Unity Quest Checkout Kiosk project, in compliance with course guidelines.

---

## AI Usage Log

| Tool | Date | Teammate | Purpose | Prompt | Response Used | Modifications Made | Verification Method | Related Work |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **Gemini / ChatGPT** | Oct 10, 2026 | Carlos Cruz | Feedback on UI Toolkit event listeners and overlay visibility | *"What is the best practice in Unity UI Toolkit for registering click callbacks on non-button elements like Labels, and how can we cleanly toggle modal visibility in KioskManager.cs?"* | Structural guidance on using `RegisterCallback<ClickEvent>` for Labels and manipulating `DisplayStyle.Flex` / `DisplayStyle.None` on container VisualElements. | Applied `RegisterCallback` to `"AvailableClickable"` and wired `"BestOptionButton"` to display the `UsefulStepOverlay`. | Verified in Unity Play Mode that label clicks and CTA buttons trigger expected visual overlays. | `KioskManager.cs`, `docs/testing.md` |
| **Gemini / ChatGPT** | Oct 11, 2026 | Carlos Cruz | Review of state persistence strategy across application restarts | *"We need to ensure Scenario B reloads its finished state upon app restart. Is storing a completion flag via PlayerPrefs recommended, and where should it be evaluated?"* | Architectural advice on storing a `ScenarioBCompleted` flag using `PlayerPrefs` and evaluating it early in the startup sequence. | Added flag checks inside `CheckAvailabilityAndLoadScreen()`, set the flag in `LoadConfirmationScreen()`, and added key clearing in `ResetKiosk()`. | Completed Scenario B, closed Play Mode, restarted, and confirmed the app launched directly into `LastMainMenu`. | `KioskManager.cs` |
| **Gemini / ChatGPT** | Oct 12, 2026 | Carlos Cruz | Feedback on Markdown formatting and test case organization | *"How can we structure our functional test cases and user trial observations in Markdown to clearly highlight pass/fail criteria, time-on-task, and evidence tables?"* | Formatting recommendations for Markdown hierarchy, left-aligned evidence blocks, and structured participant observation tables. | Customized template with real testing commit hash (`18b2794`), participant times, error counts, and local image paths. | Validated layout against project rubric requirements for Stage 2 deliverables. | `docs/testing.md` |
| **Gemini / ChatGPT** | Oct 12, 2026 | Carlos Cruz | Design review against HCI principles and Figma comparison | *"Can you review our design choices against Nielsen's Heuristics and Norman's principles, and help us categorize our UI Toolkit technical adaptations versus our Figma wireframes?"* | Analytical feedback mapping our UI decisions to *Visibility of System Status*, *User Freedom*, and *Signifiers*, alongside a comparative analysis table. | Populated table with specific project features like `UsefulStepOverlay` visibility toggles, expanded card hitboxes, and reset controls. | Cross-referenced against actual implemented UXML layouts and Figma prototype frames. | `docs/design.md` |

---

## Verification & Accountability Summary

All AI interactions served as technical feedback, code review, and structural guidance. All suggested code patterns, UI Toolkit event structures, and persistence strategies were independently integrated, reviewed, and verified in Unity Play Mode prior to submission.