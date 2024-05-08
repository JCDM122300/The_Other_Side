using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesController : MonoBehaviour
{
    //Child page that will display the content
    private PageController Page;

    [Tooltip("Page container to activate/deactivate when needed")]
    //Page objjet to activate/deactivate
    [SerializeField] private GameObject PageContainer;

    //List of collected notes
    private List<Item> NotesList;

    private int ListIndex = 0;
    private int CurrentPage = 0;
    private int TotalPages = 0;

    private bool UI_Active;
    private void Awake()
    {
        NotesList = new List<Item>();

        Page = GetComponentInChildren<PageController>();
    }
    private void Start()
    {
        ToggleUI(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            UI_Active = !UI_Active;
            ToggleUI(UI_Active);
        }

        if (UI_Active)
        {
            HandleInput();
        }
    }
    private void HandleInput()
    {
        if (NotesList.Count > 0)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (CurrentPage >= 1)
                {
                    CurrentPage--;
                    Page.ShowPage(CurrentPage);
                }
                else
                {
                    if (ListIndex == 0)
                    {
                        ListIndex = NotesList.Count - 1;
                    }
                    else
                    {
                        ListIndex--;
                    }

                    ReadNote(NotesList[ListIndex]);
                }
                
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                //Increase Page | Increase Note Index if Last page
                if (CurrentPage < TotalPages)
                {
                    CurrentPage++;
                    Page.ShowPage(CurrentPage);
                }
                else
                {
                    if (ListIndex == NotesList.Count - 1)
                    {
                        ListIndex = 0;
                    }
                    else
                    {
                        ListIndex++;
                    }

                    ReadNote(NotesList[ListIndex]);
                    Page.ShowPage(CurrentPage);

                }
            }
        }
    }

    private void ToggleUI(bool toggle)
    {
        PageContainer.SetActive(toggle);

        if (toggle == true)
        {
            NotesList.Clear();

            Inventory inv = GameObject.Find("Player").GetComponent<Inventory>();

            foreach (var kvp in inv._inventory)
            {
                if (kvp.Key.Contains("Note"))
                {
                    NotesList.Add(kvp.Value);
                }
            }

            //Reads whatever current index is
            ReadNote(NotesList[ListIndex]);
        }
    }

    private void ReadNote(Item note)
    {
        Page.SetPageText(note.Description);

        TotalPages = Page.GetPageCount();
        
        if (TotalPages > 0)
        {
            CurrentPage = 1;
        }
    }
}
