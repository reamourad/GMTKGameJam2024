using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialNext : MonoBehaviour
{
    public int page = 0;
    [SerializeField] GameObject pages;

    void Start () {
        PageNext();
    }

    public void PageNext() {
        foreach (Transform pageobject in pages.transform) {
            pageobject.gameObject.SetActive(false);
        }
        pages.transform.GetChild(page).gameObject.SetActive(true);
        page += 1;
        if (page >= pages.transform.childCount) {
            page = 0;
        }
    }
}
