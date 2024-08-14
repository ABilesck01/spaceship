using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerCustomization : MonoBehaviour
{
    [SerializeField] private PlayerShip[] ships;
    [Header("UI")]
    [SerializeField] private GameObject screen;
    [SerializeField] private TextMeshProUGUI txtShipName;
    [SerializeField] private Transform shipPreview;
    [Header("Buttons")]
    [SerializeField] private Button btnSelectLeft;
    [SerializeField] private Button btnSelectRight;
    [SerializeField] private Button btnStart;
    [Space]
    [SerializeField] private Slider redSlider;
    [SerializeField] private Slider greenSlider;
    [SerializeField] private Slider blueSlider;
    [SerializeField] private TextMeshProUGUI txtRed;
    [SerializeField] private TextMeshProUGUI txtGreen;
    [SerializeField] private TextMeshProUGUI txtBlue;
    [Space]
    [SerializeField] private GameObject firstSelected;
    [Space]
    public UnityEvent<Transform> OnShipSelected;

    public static event EventHandler OnPlayerReady;

    private int currentShip = 0;
    private bool hasMoved = false;
    private bool hasSelected = false;
    private MeshRenderer previewMesh;
    private int currentMesh = 0;
    private int red = 0;
    private int green = 0;
    private int blue = 0;
    private PlayerVisualData[] playerVisualDatas = new PlayerVisualData[4];

    public void HorizontalInput(InputAction.CallbackContext callbackContext)
    {
        float horizontal = callbackContext.ReadValue<float>();

        if(horizontal < 0 && !hasMoved)
        {
            Chose(-1);
            hasMoved = true;
        }
        else if(horizontal > 0 && !hasMoved) 
        {
            Chose(1);
            hasMoved = true;
        }
        else if (horizontal == 0 && hasMoved)
        {
            hasMoved = false;
        }
    }

    public void SelectInput(InputAction.CallbackContext callbackContext)
    {
        if (hasSelected) return;

        if(callbackContext.started)
        {
            SelectShip();
        }
    }

    private void SelectShip()
    {
        hasSelected = true;
        screen.SetActive(false);
        Transform s = Instantiate(ships[currentShip].shipPrefab, transform);
        s.GetComponentInChildren<PlayerVisual>().SetColors(playerVisualDatas);
        OnShipSelected?.Invoke(s);
        OnPlayerReady?.Invoke(this, EventArgs.Empty);
    }

    private void Start()
    {
        btnSelectLeft.onClick.AddListener(() => Chose(-1));
        btnSelectRight.onClick.AddListener(() => Chose(1));
        btnStart.onClick.AddListener(SelectShip);

        ShowShip();
    }

    private void Chose(int direction)
    {
        if(direction > 0)
        {
            currentShip++;
            if(currentShip >= ships.Length)
            {
                currentShip = 0;
            }
        }
        else if(direction < 0)
        {
            currentShip--;
            if (currentShip < 0)
            {
                currentShip = ships.Length - 1;
            }
        }

        ShowShip();
    }

    private void ShowShip()
    {
        foreach (Transform item in shipPreview)
        {
            if (item.GetComponent<Camera>() != null) continue;

            Destroy(item.gameObject);
        }

        var preview = Instantiate(ships[currentShip].ship, shipPreview);
        previewMesh = preview.GetComponent<MeshRenderer>();

        for (int i = 0; i < playerVisualDatas.Length; i++)
        {
            playerVisualDatas[i] = new PlayerVisualData(i);
            playerVisualDatas[i].VisualColor = previewMesh.materials[i].GetColor("_BaseColor");
        }

        //EventSystem.current.SetSelectedGameObject(firstSelected);

        txtShipName.text = ships[currentShip].shipName;
        Color32 current = previewMesh.materials[currentMesh].GetColor("_BaseColor");
        redSlider.value = current.r;
        greenSlider.value = current.g;
        blueSlider.value = current.b;
    }

    public void SelectMeshOne(bool value)
    {
        if (!value) return;
        currentMesh = 0;
        Color current = previewMesh.materials[currentMesh].GetColor("_BaseColor");
        redSlider.value = current.r;
        greenSlider.value = current.g;
        blueSlider.value = current.b;
    }

    public void SelectMeshTwo(bool value)
    {
        if (!value) return;
        currentMesh = 1;
        Color32 current = previewMesh.materials[currentMesh].GetColor("_BaseColor");
        redSlider.value = current.r;
        greenSlider.value = current.g;
        blueSlider.value = current.b;
    }

    public void SelectMeshTree(bool value)
    {
        if (!value) return;
        currentMesh = 2;
        Color32 current = previewMesh.materials[currentMesh].GetColor("_BaseColor");
        redSlider.value = current.r;
        greenSlider.value = current.g;
        blueSlider.value = current.b;
    }

    public void SelectMeshFour(bool value)
    {
        if (!value) return;
        currentMesh = 3;
        Color32 current = previewMesh.materials[currentMesh].GetColor("_BaseColor");
        redSlider.value = current.r;
        greenSlider.value = current.g;
        blueSlider.value = current.b;
    }

    public void SetRed(float red)
    {
        this.red = (int)red;
        txtRed.text = red.ToString();
        ChangeColor();
    }

    public void SetGreen(float green)
    {
        this.green = (int)green;
        txtGreen.text = green.ToString();
        ChangeColor();
    }

    public void SetBlue(float blue)
    {
        this.blue = (int)blue;
        txtBlue.text = blue.ToString();
        ChangeColor();
    }

    private void ChangeColor()
    {
        Color c = new Color32((byte)red, (byte)green, (byte)blue, 255);
        playerVisualDatas[currentMesh].VisualColor = c;
        previewMesh.materials[currentMesh].SetColor("_BaseColor", c);
    }
}
