using UnityEngine;
 
public class SetRenderQueue : MonoBehaviour {

    public int queueValue = 3000;

	protected void Awake() {
        Renderer rend = GetComponent<MeshRenderer>();
        rend.material.renderQueue = queueValue;

	}
}
