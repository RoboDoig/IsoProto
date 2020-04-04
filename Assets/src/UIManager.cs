using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Basic Info")]
    public Text positionText;
    public Text floorTileText;
    public Text buildTileText;
    public Text traversableText;
    public Text openText;
    public Text floorNoiseText;
    public Text buildNoiseText;
    public Text timeAliveText;

    [Header("Action Description")]
    public Transform actionDescriptionContent;
    public GameObject actionDescriptionPrefab;

    public void UpdateTextInfo(WorldTileData tileData)
    {
        positionText.text = "Position: " + tileData.position.x.ToString() + ", " + tileData.position.y.ToString();

        floorTileText.text = "Floor Tile Type: " + tileData.floorType;
        buildTileText.text = "Build Tile Type: " + tileData.buildType;

        traversableText.text = "Traversable: " + tileData.traversable.ToString();
        openText.text = "Open: " + tileData.openForPlacement.ToString();

        floorNoiseText.text = "Noise: " + tileData.floorNoiseValue.ToString();
        buildNoiseText.text = "Build: " + tileData.buildNoiseValue.ToString();

        timeAliveText.text = "Time Alive: " + tileData.timeAlive.ToString();
    }

    public void UpdateActionInfo(List<GoapAction> actionList)
    {
        foreach (GoapAction action in actionList)
        {
            GameObject actionDescriptor = Instantiate(actionDescriptionPrefab, actionDescriptionContent);
            Text actionText = actionDescriptor.GetComponentInChildren<Text>();
            actionText.text = action.name;
        }
    }

    public void ClearActionInfo()
    {
        foreach (Transform child in actionDescriptionContent)
        {
            Destroy(child.gameObject);
        }
    }
}
