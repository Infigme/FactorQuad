using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuadraticFactorizer : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField]private InputField inputA, inputB, inputC;
    [SerializeField]private Text responder;

    [Header("Input Coefficients")]
    [SerializeField]private float a, b, c;

    private void Start(){
        responder.color = Color.white;
        responder.text = "Waiting for Input...";
    }//Start()

    public void OnFactorize(){
        StartCoroutine(FactorizationSequence());
    }//OnFactorize()

    private IEnumerator FactorizationSequence(){
        responder.color = Color.white;
        responder.text = "Processing...";
        yield return new WaitForSeconds(1f);
        Factorize();
    }//factorizationSequence()

    private void Factorize(){
        // 1. Convert String Input to Floats
        if (!float.TryParse(inputA.text, out float a) ||
            !float.TryParse(inputB.text, out float b) || 
            !float.TryParse(inputC.text, out float c)){
                responder.color = Color.red;
                responder.text = "Error: Please enter valid numbers.";
                return;
            }

        // 2. Validate 'a'
        if (Mathf.Approximately(a, 0)){
            responder.color = Color.red;
            responder.text = "'a' cannot be zero.";
            return;
        }

        // 3. Solve
        float discriminant = (b * b) - (4 * a * c);

        if (discriminant > 0){
            float sqrtD = Mathf.Sqrt(discriminant);
            float x1 = (-b + sqrtD) / (2 * a);
            float x2 = (-b - sqrtD) / (2 * a);
            responder.text = $"The roots of f(x) = {a}x² + {b}x + {c} are \nx1 = {x1:F2} and x2 = {x2:F2}";
        }
        else if (Mathf.Approximately(discriminant, 0)){
            float x = -b / (2 * a);
            responder.text = $"The single root of f(x) = {a}x² + {b}x + {c} is \nx = {x:F2}";
        }
        else responder.text = "No real roots because the discriminant is negative (Complex)";
    }//Factorize()

}//class
