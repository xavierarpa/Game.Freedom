﻿#region Access
using System.Collections;
using UnityEngine;
using XavHelpTo.Look;
using XavHelpTo.Know;
#endregion
[DisallowMultipleComponent]
public class Reaction : MonoBehaviour
{
    #region Variables
        [HideInInspector]
        public Interactable interactable;
        protected bool skiping = false;
        [Header("⚡️Reaction Settings")]
        [HideInInspector]
        public string debug_information;
        [Space]
        [Range(0,20)]
        [Tooltip(
        "Nos permitirá modificar el evento para saber cuanto tiempo debe esperar hasta la siguiente,")]
        public float waiTime = 0;
    #endregion
    #region Event
    public virtual void Awake(){
        skiping = false;
    }
    public virtual void OnDrawGizmos()
    {
        name = $"Wait: ({waiTime} s) -> {debug_information}";
    }
    private void Update()
    {
        CheckForSkip();
    }
    #endregion
    #region Methods

    /// <summary>
    /// Revisa si se peude hacer skip
    /// </summary>
    private void CheckForSkip(){
        if (interactable
            && Interactable.Interacting
            && Input.GetKeyDown(KeyCode.Space)
            && interactable.reactions.Contains(this)
            ){
            
            interactable.NextReaction();
        }
    }

    /// <summary>
    /// Reaccionamos con la que estaba presente a la siguiente
    /// </summary>
    public virtual void ExecuteReaction()
    {
        React();

        StartCoroutine(WaitReact());

        //....
    }

    /// <summary>
    /// React with the interactable in case to be 
    /// </summary>
    protected virtual void React(){if (base.Equals(typeof(Reaction)))$"Reacción".Print("white");}

    /// <summary>
    /// Waits until an external thing advise to continue the reaction
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator WaitReact()
    {
        float _countTime = 0;
        while (!waiTime.TimerIn(ref _countTime)) yield return new WaitForEndOfFrame();
        interactable.NextReaction();
        //gameObject.SetActive(false);
    }



    #endregion
}

