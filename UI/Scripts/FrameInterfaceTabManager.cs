using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameInterfaceTabManager : MonoBehaviour
{
    public List<FrameInterfaceButton> tabToggleButtons;

    public int initalTab = 0;

    private void Start()
    {
        GotoInitialTab();
    }

    public void GotoInitialTab()
    {
        if(tabToggleButtons.Count > 0)
        {
            OpenTabById(initalTab);
        };
    }


    public void OpenTabById(int tabId)
    {
        tabToggleButtons[tabId].ButtonDown();
    }


    public void PreviousTab()
    {
        int currentTab = tabToggleButtons.FindIndex(tabButton => tabButton.state == ButtonState.On);
        currentTab--;

        if (currentTab < 0)
        {
            currentTab = tabToggleButtons.Count;
        };

        tabToggleButtons[currentTab].ButtonDown();
    }


    public void NextTab()
    {
        int currentTab = tabToggleButtons.FindIndex(tabButton => tabButton.state == ButtonState.On);
        currentTab++;
        
        if(currentTab >= tabToggleButtons.Count)
        {
            currentTab = 0;
        };

        tabToggleButtons[currentTab].ButtonDown();
    }


    public void OpenTab(FrameInterfaceButton thisTabButton)
    {
        tabToggleButtons.ForEach(tabButton => { 
            if (tabButton != thisTabButton)
            {
                tabButton.TriggerOff();
            };
        });
    }
}
