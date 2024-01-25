using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class DialogueControl : MonoBehaviour
{

    [System.Serializable]
    public enum idiom
    {
        pt,
        eng,
        spa,
    }

    public idiom language;

    [Header("Components")]
    public GameObject dialogueObj;  // janela do dialogo
    public Image profileSprite;   // sprite do perfil
    public Text speechText;   // texto da fala
    public Text actorNameText;   // nome do NPC


    [Header("Settings")]
    public float typingSpeed;  // velocidade da fala

    // variaveis de controle
    private bool isShowing; // saber se a janela está visivel
    private int index; //  saber a quantidade de textos dentro de uma fala / index das sentenças
    private string[] sentences;

    public static DialogueControl instance;

    public bool IsShowing { get => isShowing; set => isShowing = value; }

    // awake é chamado antes de todos os Start()  na hierarquia de execução de scripts
    private void Awake()
    {
        instance = this;
    }


    // É chamado ao inicializar
    void Start()
    {
        
    }


    void Update()
    {
        
    }

    // efeito letra por letra nos dialogos
    IEnumerator TypeSentence()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;

            yield return new WaitForSeconds(typingSpeed);
        }
    }

    // pular para próxima fala
    public void NextSentence()
    {
        if(speechText.text == sentences[index])
        {
            Debug.Log("clicou para proxima fala");
            Debug.Log("FALAS INDEX : " + sentences.Count());
            if(index < sentences.Length - 1)
            {
                index++;
                speechText.text = "";
                StartCoroutine(TypeSentence());
            }
            else // quando terminam os textos
            {
                speechText.text = "";
                index = 0;
                dialogueObj.SetActive(false);
                sentences = null;
                isShowing = false;
            }
        }
    }

    // chamar fala do NPC
    public void Speech(string[] txt)
    {
        if(!isShowing)
        {
            dialogueObj.SetActive(true);
            sentences = txt;
            StartCoroutine(TypeSentence());
            isShowing = true;
        }
    }
}
