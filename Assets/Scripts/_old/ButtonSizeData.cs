using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSizeData : MonoBehaviour
{
    public static ButtonSizeData Instance { get; private set; }

    [SerializeField][Range(1f, 25f)] private float separatorSizeUnits = 25f;
    [SerializeField][Range(0.001f, 1f)] private float widthPercent = 0.3f;
    [SerializeField][Range(0.001f, 1f)] private float heightPercent = 0.3f;

    /*v TODO - remove "[SerializeField]" - 4 testing and debugging only v*/
    [SerializeField] private Vector2 buttonSizes;
    /*^ TODO ^*/

    public Vector2 ButtonSizes { get { return buttonSizes; } }

    private void Awake() {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        separatorSizeUnits *= 4; // 4 spacers
        // set button size based o screen size
        buttonSizes = new Vector2(  (Screen.width * widthPercent) - separatorSizeUnits, 
                                    (Screen.height * heightPercent) - separatorSizeUnits);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
}
