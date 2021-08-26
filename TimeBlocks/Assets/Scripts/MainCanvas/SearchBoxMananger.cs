using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchBoxMananger : MonoBehaviour
{
    public InputField keywordInput;
    public GameObject searchBox;
    public ConfigManager configManager;
    public BlockChain blockChain;
    // Start is called before the first frame update
    void Start()
    {
        searchBox.SetActive(configManager.isSearchBoxAwake);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Search() {
        blockChain.Search(keywordInput.text);
    }
    public void OpenWindow() {
        configManager.isSearchBoxAwake = true;
        searchBox.SetActive(configManager.isSearchBoxAwake);
    }
    public void CloseWindow()
    {
        configManager.isSearchBoxAwake = false;
        searchBox.SetActive(configManager.isSearchBoxAwake);
    }
}
